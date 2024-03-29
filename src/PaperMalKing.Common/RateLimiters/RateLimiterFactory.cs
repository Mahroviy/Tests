﻿// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2022 N0D4N

using System;
using System.Threading.RateLimiting;

namespace PaperMalKing.Common.RateLimiters;

public static class RateLimiterFactory
{
	public static RateLimiter<T> Create<T>(RateLimit rateLimit)
	{
		if (rateLimit is null)
			throw new ArgumentNullException(nameof(rateLimit));
		if (rateLimit.AmountOfRequests == 0 || rateLimit.PeriodInMilliseconds == 0)
			return new RateLimiter<T>(NullRateLimiter.Instance);

		return new RateLimiter<T>(new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions()
		{
			Window = TimeSpan.FromMilliseconds(rateLimit.PeriodInMilliseconds),
			AutoReplenishment = true,
			PermitLimit = rateLimit.AmountOfRequests,
			QueueLimit = 100,
			QueueProcessingOrder = QueueProcessingOrder.OldestFirst
		}));
	}
}