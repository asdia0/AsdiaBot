namespace AsdiaBot.Discord.Commands
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    [Group("client")]
    public class About : BaseCommandModule
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
    }
}
