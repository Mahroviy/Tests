﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2022 N0D4N

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DSharpPlus;
using DSharpPlus.Entities;
using Humanizer;
using PaperMalKing.Database.Models.MyAnimeList;
using PaperMalKing.MyAnimeList.Wrapper.Models;
using PaperMalKing.MyAnimeList.Wrapper.Models.Favorites;
using PaperMalKing.MyAnimeList.Wrapper.Models.List;
using PaperMalKing.MyAnimeList.Wrapper.Models.Progress;
using WConstants = PaperMalKing.MyAnimeList.Wrapper.Constants;


namespace PaperMalKing.UpdatesProviders.MyAnimeList;

internal static class Extensions
{
	private static readonly DiscordEmbedBuilder.EmbedFooter MalUpdateFooter = new()
	{
		IconUrl = WConstants.FAV_ICON,
		Text = Constants.Name
	};

	private static readonly Dictionary<GenericProgress, DiscordColor> Colors = new()
	{
		{GenericProgress.CurrentlyInProgress, Constants.MalGreen},
		{GenericProgress.Completed, Constants.MalBlue},
		{GenericProgress.OnHold, Constants.MalYellow},
		{GenericProgress.Dropped, Constants.MalRed},
		{GenericProgress.InPlans, Constants.MalGrey},
		{GenericProgress.Reprogressing, Constants.MalGreen},
		{GenericProgress.All, Constants.MalBlack},
		{GenericProgress.Unknown, Constants.MalBlack}
	};

	internal static ParserOptions ToParserOptions(this MalUserFeatures features)
	{
		var options = ParserOptions.None;
		if (features.HasFlag(MalUserFeatures.AnimeList)) options |= ParserOptions.AnimeList;
		if (features.HasFlag(MalUserFeatures.MangaList)) options |= ParserOptions.MangaList;
		if (features.HasFlag(MalUserFeatures.Favorites)) options |= ParserOptions.Favorites;

		return options;
	}

	internal static T ToDbFavorite<T>(this BaseFavorite baseFavorite, MalUser user) where T : class, IMalFavorite
	{
		return baseFavorite switch
		{
			FavoriteAnime favoriteAnime         => favoriteAnime.ToMalFavoriteAnime(user) as T,
			FavoriteCharacter favoriteCharacter => favoriteCharacter.ToMalFavoriteCharacter(user) as T,
			FavoriteManga favoriteManga         => favoriteManga.ToMalFavoriteManga(user) as T,
			FavoritePerson favoritePerson       => favoritePerson.ToMalFavoritePerson(user) as T,
			FavoriteCompany favoriteCompany     => favoriteCompany.ToMalFavoriteCompany(user) as T,
			_                                   => throw new InvalidOperationException()
		} ?? throw new InvalidOperationException();
	}

	internal static MalFavoriteAnime ToMalFavoriteAnime(this FavoriteAnime anime, MalUser user) => new()
	{
		Id = anime.Url.Id,
		Name = anime.Name,
		Type = anime.Type,
		ImageUrl = anime.ImageUrl,
		NameUrl = anime.Url.Url,
		StartYear = anime.StartYear,
		User = user,
		UserId = user.UserId
	};

	internal static MalFavoriteManga ToMalFavoriteManga(this FavoriteManga manga, MalUser user) => new()
	{
		Id = manga.Url.Id,
		Name = manga.Name,
		Type = manga.Type,
		ImageUrl = manga.ImageUrl,
		NameUrl = manga.Url.Url,
		StartYear = manga.StartYear,
		User = user,
		UserId = user.UserId
	};

	internal static MalFavoriteCompany ToMalFavoriteCompany(this FavoriteCompany company, MalUser user) => new()
	{
		Id = company.Url.Id,
		Name = company.Name,
		ImageUrl = company.ImageUrl,
		NameUrl = company.Url.Url,
		User = user,
		UserId = user.UserId
	};

	internal static MalFavoriteCharacter ToMalFavoriteCharacter(this FavoriteCharacter character, MalUser user) => new()
	{
		Id = character.Url.Id,
		Name = character.Name,
		ImageUrl = character.ImageUrl,
		NameUrl = character.Url.Url,
		FromTitleName = character.FromName,
		User = user,
		UserId = user.UserId
	};

	internal static MalFavoritePerson ToMalFavoritePerson(this FavoritePerson person, MalUser user) => new()
	{
		Id = person.Url.Id,
		Name = person.Name,
		ImageUrl = person.ImageUrl,
		NameUrl = person.Url.Url,
		User = user,
		UserId = user.UserId
	};

	internal static DiscordEmbedBuilder WithMalUpdateProviderFooter(this DiscordEmbedBuilder builder)
	{
		builder.Footer = MalUpdateFooter;
		return builder;
	}

	internal static string ToHashString(this (string, string) v) => $"{v.Item1} {v.Item2}";

	internal static DiscordEmbedBuilder ToDiscordEmbedBuilder(this IMalFavorite favorite, bool added)
	{
		var eb = new DiscordEmbedBuilder
		{
			Url = favorite.NameUrl
		}.WithThumbnail(favorite.ImageUrl).WithDescription($"{(added ? "Added" : "Removed")} favorite");

		eb.WithColor(added ? Constants.MalGreen : Constants.MalRed);

		var title = favorite switch
		{
			IMalListFavorite baseListFavorite => $"{baseListFavorite.Name} ({baseListFavorite.Type}) [{baseListFavorite.StartYear}]",
			_                                 => favorite.Name
		};
		eb.WithTitle(title);

		if (favorite is MalFavoriteCharacter favoriteCharacter)
			eb.AddField("From", favoriteCharacter.FromTitleName, true);

		return eb;
	}

	internal static DiscordEmbedBuilder ToDiscordEmbedBuilder(this IListEntry listEntry, User user, DateTimeOffset timestamp,
															  MalUserFeatures features)
	{
		[return: NotNull]
		static string SubEntriesProgress(int progressedValue, int totalValue, bool isInPlans, string ending) =>
			progressedValue switch
			{
				0 when totalValue == 0                                                      => string.Empty,
				_ when progressedValue == totalValue || (isInPlans && progressedValue == 0) => $"{totalValue} {ending}",
				_ when totalValue == 0                                                      => $"{progressedValue}/? {ending}",
				_                                                                           => $"{progressedValue}/{totalValue} {ending}"
			};

		static string TitleMediaTypeString(string title, string mediaType, MalUserFeatures features) =>
			title.EndsWith(mediaType, StringComparison.OrdinalIgnoreCase) || title.EndsWith($"({mediaType})", StringComparison.OrdinalIgnoreCase) || !features.HasFlag(MalUserFeatures.MediaFormat)
				? title
				: $"{title} ({mediaType})";

		var eb = new DiscordEmbedBuilder().WithThumbnail(listEntry.ImageUrl).WithAuthor(user.Username, user.ProfileUrl, user.AvatarUrl)
										  .WithTimestamp(timestamp);


		if (listEntry.Score != 0)
			eb.AddField("Score", listEntry.Score.ToString(), true);

		string userProgressText;
		switch (listEntry)
		{
			case AnimeListEntry ale:
			{
				var progress = ale.UserAnimeProgress.Humanize(LetterCasing.Sentence);
				string episodeProgress = SubEntriesProgress(ale.WatchedEpisodes, ale.TotalEpisodes,
					ale.UserAnimeProgress == AnimeProgress.PlanToWatch, "ep.");
				userProgressText = episodeProgress.Length != 0 ? $"{progress} - {episodeProgress}" : progress;
				break;
			}
			case MangaListEntry mle:
			{
				string progress = mle.UserMangaProgress.Humanize(LetterCasing.Sentence)!;
				string chapterProgress = SubEntriesProgress(mle.ReadChapters, mle.TotalChapters,
					mle.UserMangaProgress == MangaProgress.PlanToRead, "ch. ");
				string volumeProgress =
					SubEntriesProgress(mle.ReadVolumes, mle.TotalVolumes, mle.UserMangaProgress == MangaProgress.PlanToRead, "v.");
				userProgressText = chapterProgress.Length != 0 || 
								   #pragma warning disable CA1508
								   volumeProgress.Length != 0 
					#pragma warning restore CA1508
					? $"{progress} - {chapterProgress}{volumeProgress}" : progress;
				break;
			}
			default:
			{
				var progress = listEntry.UserProgress.Humanize(LetterCasing.Sentence);
				var sep = SubEntriesProgress(listEntry.ProgressedSubEntries, listEntry.TotalSubEntries,
					listEntry.UserProgress == GenericProgress.InPlans, "");
				userProgressText = sep.Length != 0 ? $"{progress} - {sep}" : progress;
				break;
			}
		}

		eb.AddField("Progress", userProgressText, true);

		var shortTitle = TitleMediaTypeString(listEntry.Title, listEntry.MediaType, features);
		string title;
		if (features.HasFlag(MalUserFeatures.MediaStatus))
		{
			var entryStatus = listEntry switch
			{
				AnimeListEntry animeListEntry => animeListEntry.AnimeAiringStatus.Humanize(LetterCasing.Sentence),
				MangaListEntry mangaListEntry => mangaListEntry.MangaPublishingStatus.Humanize(LetterCasing.Sentence),
				_                             => listEntry.Status.Humanize(LetterCasing.Sentence),
			};
			title = $"{shortTitle} [{entryStatus}]";
		}
		else
			title = shortTitle;

		if (title.Length <= 256)
		{
			eb.Url = listEntry.Url;
			eb.Title = title;
		}
		else
			eb.Description = Formatter.MaskedUrl(title, new Uri(listEntry.Url));

		if (listEntry.Tags.Length != 0)
		{
			if (listEntry.Tags.Length <= 1024)
				eb.AddField("Tags", listEntry.Tags, true);
			else
			{
				var l = eb.Description?.Length ?? 0;
				var descToAdd = $"Tags\n{listEntry.Tags}".Truncate(2048 - l - 1, Truncator.FixedNumberOfCharacters);
				if (string.IsNullOrEmpty(eb.Description))
					eb.WithDescription(descToAdd);
				else
					eb.Description += descToAdd;
			}
		}


		eb.WithTitle(title);
		eb.WithColor(Colors[listEntry.UserProgress]);
		return eb;
	}
}