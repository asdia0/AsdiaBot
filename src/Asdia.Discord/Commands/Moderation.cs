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
        public async Task NickCommand(CommandContext ctx, DiscordMember user, [RemainingText] string nickname)
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
        public async Task KickCommand(CommandContext ctx, DiscordMember user, [RemainingText] string reason)
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
        public async Task BanCommand(CommandContext ctx, DiscordMember user, int deleteMessageDays, [RemainingText] string reason)
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
        public async Task BanCommand(CommandContext ctx, DiscordMember user, DiscordChannel channel)
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
        public async Task MuteVoiceCommand(CommandContext ctx, DiscordMember user)
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
        public async Task DeafCommand(CommandContext ctx, DiscordMember user)
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
        public async Task UnmuteVoiceCommand(CommandContext ctx, DiscordMember user)
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
        public async Task UndeafCommand(CommandContext ctx, DiscordMember user)
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
        public async Task MuteCommand(CommandContext ctx, DiscordMember user, [RemainingText] string reason)
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
        public async Task AddRoleCommand(CommandContext ctx, DiscordMember user, string roleID, [RemainingText] string reason)
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
        public async Task RevokeRoleCommand(CommandContext ctx, DiscordMember user, string roleID, [RemainingText] string reason)
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