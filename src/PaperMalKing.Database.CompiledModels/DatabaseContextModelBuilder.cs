﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace PaperMalKing.Database.CompiledModels
{
    public partial class DatabaseContextModel
    {
        partial void Initialize()
        {
            var discordGuildDiscordUser = DiscordGuildDiscordUserEntityType.Create(this);
            var aniListFavourite = AniListFavouriteEntityType.Create(this);
            var aniListUser = AniListUserEntityType.Create(this);
            var botUser = BotUserEntityType.Create(this);
            var discordGuild = DiscordGuildEntityType.Create(this);
            var discordUser = DiscordUserEntityType.Create(this);
            var baseMalFavorite = BaseMalFavoriteEntityType.Create(this);
            var malUser = MalUserEntityType.Create(this);
            var shikiFavourite = ShikiFavouriteEntityType.Create(this);
            var shikiUser = ShikiUserEntityType.Create(this);
            var malFavoriteAnime = MalFavoriteAnimeEntityType.Create(this, baseMalFavorite);
            var malFavoriteCharacter = MalFavoriteCharacterEntityType.Create(this, baseMalFavorite);
            var malFavoriteCompany = MalFavoriteCompanyEntityType.Create(this, baseMalFavorite);
            var malFavoriteManga = MalFavoriteMangaEntityType.Create(this, baseMalFavorite);
            var malFavoritePerson = MalFavoritePersonEntityType.Create(this, baseMalFavorite);

            DiscordGuildDiscordUserEntityType.CreateForeignKey1(discordGuildDiscordUser, discordGuild);
            DiscordGuildDiscordUserEntityType.CreateForeignKey2(discordGuildDiscordUser, discordUser);
            AniListFavouriteEntityType.CreateForeignKey1(aniListFavourite, aniListUser);
            AniListUserEntityType.CreateForeignKey1(aniListUser, discordUser);
            DiscordUserEntityType.CreateForeignKey1(discordUser, botUser);
            MalUserEntityType.CreateForeignKey1(malUser, discordUser);
            ShikiFavouriteEntityType.CreateForeignKey1(shikiFavourite, shikiUser);
            ShikiUserEntityType.CreateForeignKey1(shikiUser, discordUser);
            MalFavoriteAnimeEntityType.CreateForeignKey1(malFavoriteAnime, malUser);
            MalFavoriteCharacterEntityType.CreateForeignKey1(malFavoriteCharacter, malUser);
            MalFavoriteCompanyEntityType.CreateForeignKey1(malFavoriteCompany, malUser);
            MalFavoriteMangaEntityType.CreateForeignKey1(malFavoriteManga, malUser);
            MalFavoritePersonEntityType.CreateForeignKey1(malFavoritePerson, malUser);

            DiscordGuildEntityType.CreateSkipNavigation1(discordGuild, discordUser, discordGuildDiscordUser);
            DiscordUserEntityType.CreateSkipNavigation1(discordUser, discordGuild, discordGuildDiscordUser);

            DiscordGuildDiscordUserEntityType.CreateAnnotations(discordGuildDiscordUser);
            AniListFavouriteEntityType.CreateAnnotations(aniListFavourite);
            AniListUserEntityType.CreateAnnotations(aniListUser);
            BotUserEntityType.CreateAnnotations(botUser);
            DiscordGuildEntityType.CreateAnnotations(discordGuild);
            DiscordUserEntityType.CreateAnnotations(discordUser);
            BaseMalFavoriteEntityType.CreateAnnotations(baseMalFavorite);
            MalUserEntityType.CreateAnnotations(malUser);
            ShikiFavouriteEntityType.CreateAnnotations(shikiFavourite);
            ShikiUserEntityType.CreateAnnotations(shikiUser);
            MalFavoriteAnimeEntityType.CreateAnnotations(malFavoriteAnime);
            MalFavoriteCharacterEntityType.CreateAnnotations(malFavoriteCharacter);
            MalFavoriteCompanyEntityType.CreateAnnotations(malFavoriteCompany);
            MalFavoriteMangaEntityType.CreateAnnotations(malFavoriteManga);
            MalFavoritePersonEntityType.CreateAnnotations(malFavoritePerson);

            AddAnnotation("ProductVersion", "7.0.2");
        }
    }
}
