namespace AsdiaBot.Discord
{
    using System.IO;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;
    using DSharpPlus.Entities;

    [Group("Elo")]
    public class Elo : BaseCommandModule
    {
        [Command("version")]
        [Description("Gets the EloCalculator version running.")]
        public async Task Version(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("EloCalculator v1.0.0");
        }
    }
}
