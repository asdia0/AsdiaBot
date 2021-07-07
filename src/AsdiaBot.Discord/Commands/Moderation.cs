namespace AsdiaBot.Discord.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;
    using DSharpPlus.Entities;
    using Newtonsoft.Json;

    [Group("admin")]
    public class Moderation : BaseCommandModule
    {
        public Dictionary<ulong, Dictionary<string, string>> servers
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<ulong, Dictionary<string, string>>>(File.ReadAllText("AsdiaBot/servers.json"));
            }
        }


        public void UpdateServers(ulong guildID, string muteID = null, string prefix = null)
        {
            if (!this.servers.ContainsKey(guildID))
            {
                this.servers.Add(guildID, new()
                {
                    { "mute", "0" },
                    { "prefix", "" },
                });
            }

            if (muteID != null)
            {
                this.servers[guildID]["mute"] = muteID;
            }

            if (prefix != null)
            {
                this.servers[guildID]["prefix"] = prefix;
            }

            File.WriteAllText("AsdiaBot/servers.json", JsonConvert.SerializeObject(servers));
        }

        [Command("addMute")]
        [Description("Adds the server's muted role to muteIDs")]
        public async Task AddMuteRoleCommand(CommandContext ctx, [Description("The ID of the Muted role.")] string sRoleID)
        {
            this.UpdateServers(ctx.Guild.Id, sRoleID);

            await ctx.Channel.SendMessageAsync($"{ctx.Guild.GetRole(ulong.Parse(sRoleID)).Name} added as muted.");
        }

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

                await ctx.Channel.SendMessageAsync($"Successfully changed {user.DisplayName}#{user.Discriminator}'s nickname.").ConfigureAwait(false);
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
            reason ??= $"{user.DisplayName}#{user.Discriminator} was muted";

            try
            {
                await user.RemoveAsync(reason);

                await ctx.Channel.SendMessageAsync($"Successfully kicked {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
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
            reason ??= $"{user.DisplayName}#{user.Discriminator} was muted";

            try
            {
                await user.BanAsync(deleteMessageDays, reason);

                await ctx.Channel.SendMessageAsync($"Successfully banned {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("move")]
        [Description("Moves a user to a voice channel.")]
        [RequirePermissions(DSharpPlus.Permissions.MoveMembers)]
        public async Task MoveCommand(CommandContext ctx, [Description("The user to move.")] DiscordMember user, [Description("The channel to move to.")] DiscordChannel channel)
        {
            try
            {
                await user.PlaceInAsync(channel);

                await ctx.Channel.SendMessageAsync($"Successfully moved {user.DisplayName}#{user.Discriminator} to {channel.Mention}.").ConfigureAwait(false);
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

                await ctx.Channel.SendMessageAsync($"Successfully muted {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
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

                await ctx.Channel.SendMessageAsync($"Successfully deafened {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
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

                await ctx.Channel.SendMessageAsync($"Successfully unmuted {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
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

                await ctx.Channel.SendMessageAsync($"Successfully undeafened {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
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
            ulong roleID = ulong.Parse(servers[ctx.Guild.Id]["mute"]);

            if (roleID == 0)
            {
                await ctx.Channel.SendMessageAsync("Muted role must be set using `admin addMute` first.");
                return;
            }

            DiscordRole mute = ctx.Guild.GetRole(roleID);

            if (user.Roles.Contains(mute))
            {
                await ctx.Channel.SendMessageAsync($"{user.DisplayName}#{user.Discriminator} is already muted.");
                return;
            }

            reason ??= $"{user.DisplayName}#{user.Discriminator} was muted";

            try
            {
                await user.GrantRoleAsync(mute, reason);

                await ctx.Channel.SendMessageAsync($"Successfully muted {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("unmute")]
        [Description("Unmutes a user.")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task UnmuteCommand(CommandContext ctx, [Description("The user to unmute.")] DiscordMember user, [RemainingText][Description("The reason behind unmuting the user.")] string reason)
        {
            ulong roleID = ulong.Parse(servers[ctx.Guild.Id]["mute"]);

            if (roleID == 0)
            {
                await ctx.Channel.SendMessageAsync("Muted role must be set using `admin addMute` first.");
                return;
            }

            DiscordRole mute = ctx.Guild.GetRole(roleID);

            if (user.Roles.Contains(mute))
            {
                await ctx.Channel.SendMessageAsync($"{user.DisplayName}#{user.Discriminator} is already unmuted.");
                return;
            }

            reason ??= $"{user.DisplayName}#{user.Discriminator} was muted";

            try
            {
                await user.RevokeRoleAsync(mute, reason);

                await ctx.Channel.SendMessageAsync($"Successfully unmuted {user.DisplayName}#{user.Discriminator}.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("addrole")]
        [Description("Grants a user a role.")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task AddRoleCommand(CommandContext ctx, [Description("The user to grant the role to.")] DiscordMember user, [Description("The ID of the role.")] string roleID, [RemainingText][Description("The reason behind granting the role to the user.")] string reason)
        {
            reason ??= $"{user.DisplayName}#{user.Discriminator} was muted";

            try
            {
                DiscordRole role = ctx.Guild.GetRole(ulong.Parse(roleID));

                await user.GrantRoleAsync(role, reason);

                await ctx.Channel.SendMessageAsync($"Successfully granted {user.DisplayName}#{user.Discriminator} the role of {role.Name}.").ConfigureAwait(false);
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
            reason ??= $"{user.DisplayName}#{user.Discriminator} was muted";

            try
            {
                DiscordRole role = ctx.Guild.GetRole(ulong.Parse(roleID));

                await user.RevokeRoleAsync(role, reason);

                await ctx.Channel.SendMessageAsync($"Successfully removed {role.Name} from {user.DisplayName}#{user.Discriminator}").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync(Program.CreateErrorEmbed(e));
            }
        }

        [Command("purge")]
        [Description("Deletes multiple messages in the channel.")]
        [RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        public async Task BulkDeleteCommand(CommandContext ctx, [Description("Number of messages to delete")] int amount)
        {
            if (amount <= 0)
            {
                await ctx.Channel.SendMessageAsync("The amount of messages to remove must be positive.");
                return;
            }

            var messages = await ctx.Channel.GetMessagesAsync(amount + 1);

            var filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);

            var count = filteredMessages.Count();

            if (count == 0)
                await ctx.Channel.SendMessageAsync("Nothing to delete.");

            else
            {
                await ctx.Channel.DeleteMessagesAsync(filteredMessages);
                await ctx.Channel.SendMessageAsync($"Successfully removed {count} {(count > 1 ? "messages" : "message")}.");
            }
        }
    }
}
