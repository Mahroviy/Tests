﻿#region LICENSE
// PaperMalKing.
// Copyright (C) 2021 N0D4N
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
#endregion

// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaperMalKing.Database;

namespace PaperMalKing.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20201225224614_InitialMigration")]
    partial class InitialMigration
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
#pragma warning restore 612, 618
        }
    }
}
