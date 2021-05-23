namespace Asdia.Discord.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    public class Misc : BaseCommandModule
    {
        List<string> eightBallResponses = new List<string>()
        {
            "It is certain",
            "It is decidedly so",
            "Without a doubt",
            "Yes, definitely",
            "You may rely on it",
            "As I see it, yes",
            "Most likely",
            "Outlook good",
            "Yes",
            "Signs point to yes",
            "Reply hazy try again",
            "Ask again later",
            "Better not tell you now",
            "Cannot predict now",
            "Concentrate and ask again",
            "Don't count on it",
            "My reply is no",
            "My sources say no",
            "Outlook not so good",
            "Very doubtful",
        };

        [Command("echo")]
        [Description("Echoes the message given.")]
        public async Task PingCommand(CommandContext ctx, [RemainingText] string message)
        {
            await ctx.Channel.SendMessageAsync($"{message}").ConfigureAwait(false);
        }

        [Command("8ball")]
        [Description("Rolls an 8-ball")]
        public async Task EightBallCommand(CommandContext ctx, [RemainingText] string question)
        {
            await ctx.Channel.SendMessageAsync(eightBallResponses[new Random().Next(0, eightBallResponses.Count - 1)]).ConfigureAwait(false);
        }
    }
}
