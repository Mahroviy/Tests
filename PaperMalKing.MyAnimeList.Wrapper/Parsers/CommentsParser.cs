﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2022 N0D4N
using HtmlAgilityPack;

namespace PaperMalKing.MyAnimeList.Wrapper.Parsers;

internal static class CommentsParser
{
	internal static string Parse(HtmlNode node)
	{
		var text = node.SelectSingleNode("//title").InnerText;
		return text.Substring(0, text.LastIndexOf('\''));
	}
}