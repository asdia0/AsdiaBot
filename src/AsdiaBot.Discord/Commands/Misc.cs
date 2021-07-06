namespace AsdiaBot.Discord.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;
    using Owoify;

    public class Misc : BaseCommandModule
    {
        public readonly List<string> eightBallResponses = new()
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
        public async Task EchoCommand(CommandContext ctx, [RemainingText][Description("The message to echo.")] string message)
        {
            await ctx.Channel.SendMessageAsync(message).ConfigureAwait(false);
        }

        [Command("8ball")]
        [Description("Rolls an 8-ball.")]
        public async Task EightBallCommand(CommandContext ctx, [RemainingText][Description("The question to ask the 8-ball.")] string question)
        {
            await ctx.Channel.SendMessageAsync(eightBallResponses[new Random().Next(0, eightBallResponses.Count - 1)]).ConfigureAwait(false);
        }

        [Command("owo")]
        [Description("Owoifies the message given.")]
        public async Task OwoCommand(CommandContext ctx, [RemainingText][Description("The message to owoify.")] string message)
        {
            await ctx.Channel.SendMessageAsync(this.WeebReplace(Owoifier.Owoify(message, Owoifier.OwoifyLevel.Owo))).ConfigureAwait(false);
        }

        [Command("uvu")]
        [Description("Uvuifies the message given.")]
        public async Task UvuCommand(CommandContext ctx, [RemainingText][Description("The message to uvuify.")] string message)
        {
            await ctx.Channel.SendMessageAsync(this.WeebReplace(Owoifier.Owoify(message, Owoifier.OwoifyLevel.Uvu))).ConfigureAwait(false);
        }

        [Command("uwu")]
        [Description("Uwuifies the message given.")]
        public async Task UwuCommand(CommandContext ctx, [RemainingText][Description("The message to uwuify.")] string message)
        {
            await ctx.Channel.SendMessageAsync(this.WeebReplace(Owoifier.Owoify(message, Owoifier.OwoifyLevel.Uwu))).ConfigureAwait(false);
        }

        public string WeebReplace(string input)
        {
            return input.Replace("`", "\\`");
        }
    }
}
