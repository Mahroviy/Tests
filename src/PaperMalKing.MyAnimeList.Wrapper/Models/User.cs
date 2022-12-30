﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2022 N0D4N
using System;
using PaperMalKing.MyAnimeList.Wrapper.Models.Favorites;

namespace PaperMalKing.MyAnimeList.Wrapper.Models;

internal sealed class User
{
	private string? _avatarUrl;
	private string? _profileUrl;
	internal required string Username { get; init; }

	internal string ProfileUrl => this._profileUrl ??= $"{Constants.PROFILE_URL}{this.Username}";

	internal string AvatarUrl =>
		this._avatarUrl ??=
			$"{Constants.USER_AVATAR}{this.Id}.jpg?t={DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

	internal uint Id { get; init; }

	internal bool HasPublicAnimeUpdates => this.LatestAnimeUpdateHash is not null;

	internal bool HasPublicMangaUpdates => this.LatestMangaUpdateHash is not null;

	internal string? LatestMangaUpdateHash { get; init; }

	internal string? LatestAnimeUpdateHash { get; init; }

	internal required UserFavorites Favorites { get; init; }
}