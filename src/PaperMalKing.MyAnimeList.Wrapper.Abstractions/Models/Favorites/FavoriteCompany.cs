// Tantei.
// Copyright (C) 2021-2022 N0D4N
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

namespace PaperMalKing.MyAnimeList.Wrapper.Abstractions.Models.Favorites;

public sealed class FavoriteCompany : BaseFavorite
{
	public FavoriteCompany(MalUrl url, string name, string? imageUrl) : base(url, name, imageUrl)
	{ }

	public FavoriteCompany(BaseFavorite other) : base(other)
	{ }
}