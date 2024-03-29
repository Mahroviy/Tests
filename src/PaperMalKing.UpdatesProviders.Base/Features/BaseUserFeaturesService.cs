// SPDX-License-Identifier: AGPL-3.0-or-later
// Copyright (C) 2021-2023 N0D4N

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaperMalKing.Database;
using PaperMalKing.Database.Models;
using PaperMalKing.UpdatesProviders.Base.Exceptions;

namespace PaperMalKing.UpdatesProviders.Base.Features;

public abstract class BaseUserFeaturesService<TUser, TFeature>
	where TFeature : unmanaged, Enum where TUser : class, IUpdateProviderUser<TFeature>
{
	protected  ILogger<BaseUserFeaturesService<TUser, TFeature>> Logger { get; }
	protected IDbContextFactory<DatabaseContext> DbContextFactory { get; }

	protected BaseUserFeaturesService(IDbContextFactory<DatabaseContext> dbContextFactory, ILogger<BaseUserFeaturesService<TUser, TFeature>> logger)
	{
		this.Logger = logger;
		this.DbContextFactory = dbContextFactory;
	}

	public abstract Task EnableFeaturesAsync(TFeature feature, ulong userId);

	public async Task DisableFeaturesAsync(TFeature feature, ulong userId)
	{
		using var db = this.DbContextFactory.CreateDbContext();
		var dbUser = db.Set<TUser>().FirstOrDefault(su => su.DiscordUserId == userId);

		if (dbUser is null)
		{
			throw new UserFeaturesException("You must register first before disabling features");
		}

		var features = dbUser.Features;
		var f = Unsafe.As<TFeature, ulong>(ref features);
		var featureValue = Unsafe.As<TFeature, ulong>(ref feature);
		if ((f & featureValue) != 0)
		{
			throw new UserFeaturesException("This feature wasnt enabled for you,so you cant enable it");
		}

		f &= ~featureValue;

		dbUser.Features = Unsafe.As<ulong, TFeature>(ref f);
		await DisableFeatureCleanupAsync(db, dbUser, feature).ConfigureAwait(false);
		await db.SaveChangesAndThrowOnNoneAsync(CancellationToken.None).ConfigureAwait(false);
	}

	protected abstract ValueTask DisableFeatureCleanupAsync(DatabaseContext db, TUser user, TFeature featureToDisable);

	public ValueTask<string> EnabledFeaturesAsync(ulong userId)
	{
		using var db = this.DbContextFactory.CreateDbContext();
		var features = db.Set<TUser>().AsNoTracking().Where(u => u.DiscordUser.DiscordUserId == userId).Select(x => new TFeature?(x.Features))
						 .FirstOrDefault();
		if (!features.HasValue)
		{
			throw new UserFeaturesException("You must register first before checking for enabled features");
		}

		return ValueTask.FromResult(features.Value.Humanize());

	}
}