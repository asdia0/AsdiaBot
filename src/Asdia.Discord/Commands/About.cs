namespace Asdia.Discord.Commands
{
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    public class About : BaseCommandModule
    {
        [Command("ping")]
        [Aliases("p")]
        public async Task PingCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"{ctx.Client.Ping}ms").ConfigureAwait(false);
        }
    }
}
