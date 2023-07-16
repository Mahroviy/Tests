﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2023 N0D4N
// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaperMalKing.Database;

namespace PaperMalKing.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210114200529_AddAniListModels")]
    partial class AddAniListModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DiscordGuildDiscordUser", b =>
                {
                    b.Property<long>("GuildsDiscordGuildId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UsersDiscordUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GuildsDiscordGuildId", "UsersDiscordUserId");

                    b.HasIndex("UsersDiscordUserId");

                    b.ToTable("DiscordGuildDiscordUser");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.AniList.AniListFavourite", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("FavouriteType")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id", "FavouriteType", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AniListFavourites");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.AniList.AniListUser", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<long>("DiscordUserId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LastActivityTimestamp")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LastReviewTimestamp")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DiscordUserId");

                    b.ToTable("AniListUsers");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.BotUser", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("BotUsers");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.DiscordGuild", b =>
                {
                    b.Property<long>("DiscordGuildId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PostingChannelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscordGuildId");

                    b.ToTable("DiscordGuilds");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.DiscordUser", b =>
                {
                    b.Property<long>("DiscordUserId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("BotUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscordUserId");

                    b.HasIndex("BotUserId")
                        .IsUnique();

                    b.ToTable("DiscordUsers");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoriteAnime", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StartYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MalFavoriteAnimes");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoriteCharacter", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FromTitleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FromTitleUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MalFavoriteCharacters");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoriteManga", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StartYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MalFavoriteMangas");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoritePerson", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MalFavoritePersons");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("DiscordUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastAnimeUpdateHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastMangaUpdateHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("LastUpdatedAnimeListTimestamp")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LastUpdatedMangaListTimestamp")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("DiscordUserId");

                    b.ToTable("MalUsers");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.Shikimori.ShikiFavourite", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FavType")
                        .HasColumnType("TEXT");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "FavType", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ShikiFavourites");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.Shikimori.ShikiUser", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<long>("DiscordUserId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LastHistoryEntryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DiscordUserId");

                    b.ToTable("ShikiUsers");
                });

            modelBuilder.Entity("DiscordGuildDiscordUser", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.DiscordGuild", null)
                        .WithMany()
                        .HasForeignKey("GuildsDiscordGuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaperMalKing.Database.Models.DiscordUser", null)
                        .WithMany()
                        .HasForeignKey("UsersDiscordUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.AniList.AniListFavourite", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.AniList.AniListUser", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.AniList.AniListUser", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.DiscordUser", "DiscordUser")
                        .WithMany()
                        .HasForeignKey("DiscordUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscordUser");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.DiscordUser", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.BotUser", "BotUser")
                        .WithOne("DiscordUser")
                        .HasForeignKey("PaperMalKing.Database.Models.DiscordUser", "BotUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BotUser");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoriteAnime", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.MyAnimeList.MalUser", "User")
                        .WithMany("FavoriteAnimes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoriteCharacter", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.MyAnimeList.MalUser", "User")
                        .WithMany("FavoriteCharacters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoriteManga", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.MyAnimeList.MalUser", "User")
                        .WithMany("FavoriteMangas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalFavoritePerson", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.MyAnimeList.MalUser", "User")
                        .WithMany("FavoritePeople")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalUser", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.DiscordUser", "DiscordUser")
                        .WithMany()
                        .HasForeignKey("DiscordUserId");

                    b.Navigation("DiscordUser");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.Shikimori.ShikiFavourite", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.Shikimori.ShikiUser", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.Shikimori.ShikiUser", b =>
                {
                    b.HasOne("PaperMalKing.Database.Models.DiscordUser", "DiscordUser")
                        .WithMany()
                        .HasForeignKey("DiscordUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscordUser");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.AniList.AniListUser", b =>
                {
                    b.Navigation("Favourites");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.BotUser", b =>
                {
                    b.Navigation("DiscordUser")
                        .IsRequired();
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.MyAnimeList.MalUser", b =>
                {
                    b.Navigation("FavoriteAnimes");

                    b.Navigation("FavoriteCharacters");

                    b.Navigation("FavoriteMangas");

                    b.Navigation("FavoritePeople");
                });

            modelBuilder.Entity("PaperMalKing.Database.Models.Shikimori.ShikiUser", b =>
                {
                    b.Navigation("Favourites");
                });
#pragma warning restore 612, 618
        }
    }
}
