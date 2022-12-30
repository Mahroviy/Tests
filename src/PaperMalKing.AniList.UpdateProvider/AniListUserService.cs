﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2022 N0D4N
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaperMalKing.AniList.Wrapper;
using PaperMalKing.Database;
using PaperMalKing.Database.Models;
using PaperMalKing.Database.Models.AniList;
using PaperMalKing.UpdatesProviders.Base;
using PaperMalKing.UpdatesProviders.Base.Exceptions;

namespace PaperMalKing.AniList.UpdateProvider;

public sealed class AniListUserService : IUpdateProviderUserService
{
	private readonly AniListClient _client;
	private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;
	public static string Name => Constants.NAME;

	public AniListUserService(AniListClient client, IDbContextFactory<DatabaseContext> dbContextFactory)
	{
		this._client = client;
		this._dbContextFactory = dbContextFactory;
	}

	public async Task<BaseUser> AddUserAsync(ulong userId, ulong guildId, string? username = null)
	{
		using var db = this._dbContextFactory.CreateDbContext();
		var dbUser = db.AniListUsers.Include(su => su.DiscordUser).ThenInclude(du => du.Guilds).FirstOrDefault(su => su.DiscordUserId == userId);
		DiscordGuild? guild;
		if (dbUser is not null) // User already in db
		{
			if (dbUser.DiscordUser.Guilds.Any(g => g.DiscordGuildId == guildId))
			{
				throw new UserProcessingException(
					"You already have your account connected. If you want to switch to another account, remove current one, then add the new one.");

			}
			guild = db.DiscordGuilds.FirstOrDefault(g => g.DiscordGuildId == guildId);
			if (guild is null)
			{
				throw new UserProcessingException(BaseUser.FromUsername(username), 
					"Current server is not in database, ask server administrator to add this server to bot");
			}

			dbUser.DiscordUser.Guilds.Add(guild);
			db.AniListUsers.Update(dbUser);
			await db.SaveChangesAndThrowOnNoneAsync().ConfigureAwait(false);
			return BaseUser.FromUsername(username);
		}

		if (string.IsNullOrWhiteSpace(username))
		{
			throw new UserProcessingException(BaseUser.Empty, "You must provide username if you arent already tracked by this bot");
		}
		guild = db.DiscordGuilds.FirstOrDefault(g => g.DiscordGuildId == guildId);
		if (guild is null)
			throw new UserProcessingException(BaseUser.FromUsername(username), 
				"Current server is not in database, ask server administrator to add this server to bot");
		var dUser = db.DiscordUsers.Include(x => x.Guilds).FirstOrDefault(du => du.DiscordUserId == userId);
		var response = await this._client.GetCompleteUserInitialInfoAsync(username).ConfigureAwait(false);
		var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		if (dUser is null)
		{
			dUser = new()
			{
				Guilds = new DiscordGuild[1] { guild },
				DiscordUserId = userId,
				BotUser = new()
			};
		}
		else if (dUser.Guilds.All(x => x.DiscordGuildId != guildId))
		{
			dUser.Guilds.Add(guild);
		}
		dbUser = new()
		{
			Favourites = response.Favourites.Select(f => new AniListFavourite { Id = f.Id, FavouriteType = (FavouriteType)f.Type }).ToList(),
			Id = response.UserId!.Value,
			DiscordUser = dUser,
			LastActivityTimestamp = now,
			LastReviewTimestamp = now
		};
		dbUser.Favourites.ForEach(f =>
		{
			f.User = dbUser;
			f.UserId = dbUser.Id;
		});
		db.AniListUsers.Add(dbUser);
		await db.SaveChangesAndThrowOnNoneAsync().ConfigureAwait(false);
		return BaseUser.FromUsername(username);
	}

	public async Task<BaseUser> RemoveUserAsync(ulong userId)
	{
		using var db = this._dbContextFactory.CreateDbContext();
		var user = db.AniListUsers.Include(su => su.DiscordUser).ThenInclude(du => du.Guilds).FirstOrDefault(su => su.DiscordUserId == userId);
		if (user is null)
			throw new UserProcessingException($"You weren't tracked by {Name} update checker");

		db.AniListUsers.Remove(user);
		await db.SaveChangesAndThrowOnNoneAsync().ConfigureAwait(false);
		return BaseUser.Empty;
	}

	public async Task RemoveUserHereAsync(ulong userId, ulong guildId)
	{
		using var db = this._dbContextFactory.CreateDbContext();
		var user = db.DiscordUsers.Include(du => du.Guilds).FirstOrDefault(du => du.DiscordUserId == userId);
		if (user is null)
			throw new UserProcessingException("You weren't registered in bot");
		var guild = user.Guilds.FirstOrDefault(g => g.DiscordGuildId == guildId);
		if (guild is null)
			throw new UserProcessingException("You weren't registered in this server");

		user.Guilds.Remove(guild);
		db.DiscordUsers.Update(user);
		await db.SaveChangesAndThrowOnNoneAsync().ConfigureAwait(false);
	}

	public IReadOnlyList<BaseUser> ListUsers(ulong guildId)
	{
		using var db = this._dbContextFactory.CreateDbContext();
		return db.AniListUsers.Include(su => su.DiscordUser).ThenInclude(du => du.Guilds).OrderByDescending(u => u.LastActivityTimestamp).Select(su => new
				 {
					 su.DiscordUser
				 }).Where(u => u.DiscordUser.Guilds.Any(g => g.DiscordGuildId == guildId))
				 .Select(u => new BaseUser("", u.DiscordUser)).AsNoTracking().ToArray();
	}
}