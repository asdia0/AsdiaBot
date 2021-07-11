namespace AsdiaBot.Discord.Commands
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;
    using DSharpPlus.Entities;

    [Group("client")]
    public class Client : BaseCommandModule
    {
        [Command("ping")]
        [Aliases("p")]
        [Description("Checks the latency between discord and the bot.")]
        public async Task PingCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"{ctx.Client.Ping}ms").ConfigureAwait(false);
        }

        [Command("prefix")]
        [Description("Returns all accepted prefixes.")]
        public async Task PrefixCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Accepted prefixes: `a!`").ConfigureAwait(false);
        }

        [Command("id")]
        [Description("Returns the user's ID.")]
        public async Task IdCommand(CommandContext ctx, DiscordUser user)
        {
            await ctx.Channel.SendMessageAsync($"{user.Username}#{user.Discriminator}'s Id is `{user.Id}`.");
        }

        [Command("id")]
        [Description("Returns the author's ID.")]
        public async Task IdCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"{ctx.Message.Author.Username}#{ctx.Message.Author.Discriminator}'s Id is `{ctx.Message.Author.Id}`.");
        }

        [Command("serverid")]
        [Description("Returns the server's ID.")]
        public async Task ServerIdCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"{ctx.Guild.Name}'s Id is `{ctx.Guild.Id}`.");
        }
    }
}
