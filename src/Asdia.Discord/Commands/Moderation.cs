namespace Asdia.Discord.Commands
{
    using System;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;
    using DSharpPlus.Entities;

    [Group("admin")]
    public class Moderation : BaseCommandModule
    {
        public static ulong muteID = 845926252619104307;

        [Command("nick")]
        [Description("Changes the nickname of a user.")]
        [RequirePermissions(DSharpPlus.Permissions.ChangeNickname)]
        public async Task NickCommand(CommandContext ctx, [Description("The user with the changed nickname.")] DiscordMember user, [RemainingText][Description("The new nickname.")] string nickname)
        {
            try
            {
                await user.ModifyAsync(x =>
                {
                    x.Nickname = nickname;
                    x.AuditLogReason = $"Changed by {ctx.User.Username} ({ctx.User.Id}).";
                });

                await ctx.Channel.SendMessageAsync($"Successfully changed `{user.Username}`'s nickname.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("kick")]
        [Description("Kicks a user from the guild.")]
        [RequirePermissions(DSharpPlus.Permissions.KickMembers)]
        public async Task KickCommand(CommandContext ctx, [Description("The user to kick.")] DiscordMember user, [RemainingText][Description("The reason behind kicking the user.")] string reason)
        {
            try
            {
                await user.RemoveAsync(reason);

                await ctx.Channel.SendMessageAsync($"Successfully kicked `{user.Username}`.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("ban")]
        [Description("Bans a user from the guild.")]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        public async Task BanCommand(CommandContext ctx, [Description("The user to ban.")] DiscordMember user, [Description("The number of days of messages from the banned user to delete.")] int deleteMessageDays, [RemainingText][Description("The reason behind banning the user.")] string reason)
        {
            try
            {
                await user.BanAsync(deleteMessageDays, reason);

                await ctx.Channel.SendMessageAsync($"Successfully banned `{user.Username}`.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("move")]
        [Description("Moves a user to a voice channel.")]
        [RequirePermissions(DSharpPlus.Permissions.MoveMembers)]
        public async Task BanCommand(CommandContext ctx, [Description("The user to move.")] DiscordMember user, [Description("The channel to move to.")] DiscordChannel channel)
        {
            try
            {
                await user.PlaceInAsync(channel);

                await ctx.Channel.SendMessageAsync($"Successfully moved `{user.Username}` to {channel.Mention}.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("mutev")]
        [Description("Mutes a user in a voice channel.")]
        [RequirePermissions(DSharpPlus.Permissions.MuteMembers)]
        public async Task MuteVoiceCommand(CommandContext ctx, [Description("The user to mute.")] DiscordMember user)
        {
            try
            {
                await user.SetMuteAsync(true);

                await ctx.Channel.SendMessageAsync($"Successfully muted `{user.Username}`.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("deaf")]
        [Description("Deafens a user in a voice channel.")]
        [RequirePermissions(DSharpPlus.Permissions.DeafenMembers)]
        public async Task DeafCommand(CommandContext ctx, [Description("The user to deafen.")] DiscordMember user)
        {
            try
            {
                await user.SetDeafAsync(true);

                await ctx.Channel.SendMessageAsync($"Successfully deafened `{user.Username}`.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("unmutev")]
        [Description("Unmutes a user in a voice channel.")]
        [RequirePermissions(DSharpPlus.Permissions.MuteMembers)]
        public async Task UnmuteVoiceCommand(CommandContext ctx, [Description("The user to unmute.")] DiscordMember user)
        {
            try
            {
                await user.SetMuteAsync(true);

                await ctx.Channel.SendMessageAsync($"Successfully unmuted `{user.Username}`.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("undeaf")]
        [Description("Undeafens a user in a voice channel.")]
        [RequirePermissions(DSharpPlus.Permissions.DeafenMembers)]
        public async Task UndeafCommand(CommandContext ctx, [Description("The user to undeafen.")] DiscordMember user)
        {
            try
            {
                await user.SetDeafAsync(true);

                await ctx.Channel.SendMessageAsync($"Successfully undeafened `{user.Username}`.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("mute")]
        [Description("Mutes a user.")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task MuteCommand(CommandContext ctx, [Description("The user to mute.")] DiscordMember user, [RemainingText][Description("The reason behind muting the user.")] string reason)
        {
            try
            {
                await user.GrantRoleAsync(ctx.Guild.GetRole(muteID), reason);

                await ctx.Channel.SendMessageAsync($"Successfully muted `{user.Username}`.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("addrole")]
        [Description("Grants a user a role.")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task AddRoleCommand(CommandContext ctx, [Description("The use to grant the role to.")] DiscordMember user, [Description("The ID of the role.")] string roleID, [RemainingText][Description("The reason behind granting the role to the user.")] string reason)
        {
            try
            {
                DiscordRole role = ctx.Guild.GetRole(ulong.Parse(roleID));

                await user.GrantRoleAsync(role, reason);

                await ctx.Channel.SendMessageAsync($"Successfully granted `{user.Username}` the role of {role.Name}.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("removerole")]
        [Description("Revokes a user's role.")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task RevokeRoleCommand(CommandContext ctx, [Description("The user to remove the role from.")] DiscordMember user, [Description("The ID of the role.")] string roleID, [RemainingText][Description("The reason behind removing the role from the user.")] string reason)
        {
            try
            {
                DiscordRole role = ctx.Guild.GetRole(ulong.Parse(roleID));

                await user.RevokeRoleAsync(role, reason);

                await ctx.Channel.SendMessageAsync($"Successfully removed {role.Name} from `{user.Username}`").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }
    }
}