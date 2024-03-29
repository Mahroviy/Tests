﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2022 N0D4N

using System.Text.Json.Serialization;
using PaperMalKing.Common.Json;

namespace PaperMalKing.AniList.Wrapper.Abstractions.Models;

public sealed class MediaTag
{
	[JsonPropertyName("name")]
	[JsonConverter(typeof(StringPoolingJsonConverter))]
	public required string Name { get; init; }

	[JsonPropertyName("rank")]
	public byte Rank { get; init; }

	[JsonPropertyName("isMediaSpoiler")]
	public bool IsSpoiler { get; init; }
}