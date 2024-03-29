﻿//

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Microsoft.Extensions.Logging;
using PaperMalKing.Common;
using PaperMalKing.Common.Attributes;
using PaperMalKing.Startup.Exceptions;
using PaperMalKing.Startup.Services;
using PaperMalKing.UpdatesProviders.Base;

namespace PaperMalKing.Startup.Commands;

[SlashCommandGroup("sm", "Commands for managing server", true)]
[SlashModuleLifespan(SlashModuleLifespan.Singleton)]
[OwnerOrPermissions(Permissions.ManageGuild)]
[SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods")]
[GuildOnly, SlashRequireGuild]
internal sealed class GuildManagementCommands : BotCommandsModule
{
	private readonly ILogger<GuildManagementCommands> _logger;

	private readonly GuildManagementService _managementService;
	private readonly GeneralUserService _userService;

	public GuildManagementCommands(ILogger<GuildManagementCommands> logger, GuildManagementService managementService, GeneralUserService userService)
	{
		this._logger = logger;
		this._managementService = managementService;
		this._userService = userService;
	}

	[SlashCommand("set", "Sets channel to post updates to", true)]
	public async Task SetChannelCommand(InteractionContext context,
										[Option(nameof(channel), "Channel updates should be posted", autocomplete: false)] DiscordChannel? channel = null)
	{
		if (channel is null)
			channel = context.Channel;
		if (channel.IsCategory || channel.IsThread)
		{
			await context.EditResponseAsync(embed: EmbedTemplate.ErrorEmbed("You cant set posting channel to category or to a thread"))
										.ConfigureAwait(false);
			return;
		}

		try
		{
			var perms = channel.PermissionsFor(context.Guild.CurrentMember);
			if (!perms.HasPermission(Permissions.SendMessages))
			{
				await context.EditResponseAsync(embed: EmbedTemplate.ErrorEmbed(
								 $"Bot wouldn't be able to send updates to channel {channel} because it lacks permission to send messages",
								 "Permissions error"))
							 .ConfigureAwait(false);
			}

			await this._managementService.SetChannelAsync(channel.GuildId!.Value, channel.Id).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			var embed = ex is GuildManagementException ? EmbedTemplate.ErrorEmbed(ex.Message) : EmbedTemplate.UnknownErrorEmbed;
			await context.EditResponseAsync(embed: embed).ConfigureAwait(false);
			throw;
		}

		await context.EditResponseAsync(embed: EmbedTemplate.SuccessEmbed($"Successfully set {channel}")).ConfigureAwait(false);
	}

	[SlashCommand("update", "Updates channel where updates are posted", true)]
	public async Task UpdateChannelCommand(InteractionContext context,
										   [Option(nameof(channel), "New channel where updates should be posted")] DiscordChannel? channel = null)
	{
		if (channel is null)
			channel = context.Channel;
		if (channel.IsCategory || channel.IsThread)
		{
			await context.EditResponseAsync(EmbedTemplate.ErrorEmbed("You cant set posting channel to category or to a thread"))
										.ConfigureAwait(false);
			return;
		}

		try
		{
			var perms = channel.PermissionsFor(context.Guild.CurrentMember);
			if (!perms.HasPermission(Permissions.SendMessages))
			{
				await context.EditResponseAsync(embed: EmbedTemplate.ErrorEmbed(
								 $"Bot wouldn't be able to send updates to channel {channel} because it lacks permission to send messages",
								 "Permissions error"))
							 .ConfigureAwait(false);
			}

			await this._managementService.UpdateChannelAsync(channel.GuildId!.Value, channel.Id).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			var embed = ex is GuildManagementException ? EmbedTemplate.ErrorEmbed(ex.Message) : EmbedTemplate.UnknownErrorEmbed;
			await context.EditResponseAsync(embed: embed).ConfigureAwait(false);
			throw;
		}

		await context.EditResponseAsync(embed: EmbedTemplate.SuccessEmbed($"Successfully updated to {channel}")).ConfigureAwait(false);
	}

	[SlashCommand("removeserver", "Remove this server from being tracked", true)]
	public async Task RemoveGuildCommand(InteractionContext context)
	{
		try
		{
			await this._managementService.RemoveGuildAsync(context.Guild.Id).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			var embed = ex is GuildManagementException ? EmbedTemplate.ErrorEmbed(ex.Message) : EmbedTemplate.UnknownErrorEmbed;
			await context.EditResponseAsync(embed: embed).ConfigureAwait(false);
			throw;
		}

		await context.EditResponseAsync(embed: EmbedTemplate.SuccessEmbed("Successfully removed this server from being tracked"))
					 .ConfigureAwait(false);
	}

	[SlashCommand("forceremoveuserById", "Remove this user from being tracked in this server", true)]
	public async Task ForceRemoveUserCommand(InteractionContext context,
											 [Option(nameof(userId), "Discord user's id which should be to removed from being tracked")] long userId)
	{
		try
		{
			await this._userService.RemoveUserInGuildAsync(context.Guild.Id, (ulong)userId).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			var embed = ex is GuildManagementException ? EmbedTemplate.ErrorEmbed(ex.Message) : EmbedTemplate.UnknownErrorEmbed;
			await context.EditResponseAsync(embed: embed).ConfigureAwait(false);
			throw;
		}

		await context.EditResponseAsync(embed: EmbedTemplate.SuccessEmbed($"Successfully removed {userId} this server from being tracked"))
					 .ConfigureAwait(false);
	}

	[SlashCommand("forceremoveuser", "Remove this user from being tracked in this server")]
	public Task ForceRemoveUserCommand(InteractionContext context, [Option(nameof(user), "Discord user to remove from being tracked")] DiscordUser user) =>
		this.ForceRemoveUserCommand(context, (long)user.Id);
}