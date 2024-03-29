// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2023 N0D4N

using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.HighPerformance.Buffers;

namespace PaperMalKing.Common.Json;

/// <summary>
/// This converter tries to pool string from incoming json if possible.
/// It is intended to work on small strings only (smaller or equal to <see cref="MaxLengthLimit"/> chars)
/// If input string is escaped, or length in bytes exceed 256, it isn't pooled, and is just allocated
/// </summary>
public sealed class StringPoolingJsonConverter : JsonConverter<string>
{
	private static readonly StringPool _stringPool = new StringPool();

	public const int MaxLengthLimit = 128;
	private const int ByteBufferLength = MaxLengthLimit * 4; // 4 is max count of bytes per character

	public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return ReadStringOrGetFromPool(ref reader, _stringPool);
	}

	public static string ReadStringOrGetFromPool(ref Utf8JsonReader reader) => ReadStringOrGetFromPool(ref reader, _stringPool);

	/// <remarks>
	/// Uses ref in order to not copy struct when invoking <see cref="Utf8JsonReader.GetString"/> in unhappy paths
	/// </remarks>
	public static string ReadStringOrGetFromPool(ref Utf8JsonReader reader, StringPool stringPool)
	{
		Debug.Assert(reader.TokenType == JsonTokenType.String);
		if (reader.ValueIsEscaped || (reader.HasValueSequence ? reader.ValueSequence.Length : reader.ValueSpan.Length) > 256)
		{
			return reader.GetString()!;
		}

		scoped Span<byte> bytes = stackalloc byte[ByteBufferLength];
		var bytesWritten = reader.CopyString(bytes);
		bytes = bytes.Slice(0, bytesWritten);
		scoped Span<char> chars = stackalloc char[MaxLengthLimit];
		var charsWritten = Encoding.UTF8.GetChars(bytes, chars);
		return stringPool.GetOrAdd(chars.Slice(0, charsWritten));
	}

	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
	{ }
}