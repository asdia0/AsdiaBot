namespace Asdia.Discord.Commands
{
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    public class Misc : BaseCommandModule
    {
        [Command("echo")]
        [Description("Echoes the message given.")]
        public async Task PingCommand(CommandContext ctx, [RemainingText] string message)
        {
            await ctx.Channel.SendMessageAsync($"{message}").ConfigureAwait(false);
        }
    }
}
