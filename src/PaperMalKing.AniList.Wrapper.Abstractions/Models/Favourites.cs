﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2022 N0D4N

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using PaperMalKing.AniList.Wrapper.Abstractions.Models.Enums;

namespace PaperMalKing.AniList.Wrapper.Abstractions.Models;

[SuppressMessage("Design", "CA1041:Provide ObsoleteAttribute message")]
public sealed class Favourites
{
	public bool HasNextPage { get; init; } = false;

	private readonly List<IdentifiableFavourite> _allFavourites = new();

	public IReadOnlyList<IdentifiableFavourite> AllFavourites => this._allFavourites;

	[JsonPropertyName("anime")]
	public Connection<IdentifiableFavourite> Anime
	{
		[Obsolete("",true), EditorBrowsable(EditorBrowsableState.Never)]get => ThrowNotSupportedException();
		init
		{
			if (value.PageInfo!.HasNextPage) this.HasNextPage = value.PageInfo.HasNextPage;
			Array.ForEach(value.Nodes, fav => fav.Type = FavouriteType.Anime);
			this._allFavourites.AddRange(value.Nodes);
		}
	}

	[JsonPropertyName("manga")]
	public Connection<IdentifiableFavourite> Manga
	{
		[Obsolete("",true), EditorBrowsable(EditorBrowsableState.Never)]get => ThrowNotSupportedException();
		init
		{
			if (value.PageInfo!.HasNextPage) this.HasNextPage = value.PageInfo.HasNextPage;

			Array.ForEach(value.Nodes, fav => fav.Type = FavouriteType.Manga);
			this._allFavourites.AddRange(value.Nodes);
		}
	}

	[JsonPropertyName("characters")]
	public Connection<IdentifiableFavourite> Characters
	{
		[Obsolete("",true), EditorBrowsable(EditorBrowsableState.Never)]get => ThrowNotSupportedException();
		init
		{
			if (value.PageInfo!.HasNextPage) this.HasNextPage = value.PageInfo.HasNextPage;

			Array.ForEach(value.Nodes, fav => fav.Type = FavouriteType.Characters);
			this._allFavourites.AddRange(value.Nodes);
		}
	}

	[JsonPropertyName("staff")]
	public Connection<IdentifiableFavourite> Staff
	{
		[Obsolete("",true), EditorBrowsable(EditorBrowsableState.Never)]get => ThrowNotSupportedException();
		init
		{
			if (value.PageInfo!.HasNextPage) this.HasNextPage = value.PageInfo.HasNextPage;

			Array.ForEach(value.Nodes, fav => fav.Type = FavouriteType.Staff);
			this._allFavourites.AddRange(value.Nodes);
		}
	}

	[JsonPropertyName("studios")]
	public Connection<IdentifiableFavourite> Studios
	{
		[Obsolete("",true), EditorBrowsable(EditorBrowsableState.Never)]get => ThrowNotSupportedException();
		init
		{
			if (value.PageInfo!.HasNextPage) this.HasNextPage = value.PageInfo.HasNextPage;

			Array.ForEach(value.Nodes, fav => fav.Type = FavouriteType.Studios);
			this._allFavourites.AddRange(value.Nodes);
		}
	}

	public static readonly Favourites Empty = new() {HasNextPage = false};

	private static Connection<IdentifiableFavourite> ThrowNotSupportedException()
	{
		throw new NotSupportedException("You shouldn't access this property");
	}
}