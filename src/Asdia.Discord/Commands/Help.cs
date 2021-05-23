namespace Asdia.Discord.Commands
{
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Converters;
    using DSharpPlus.CommandsNext.Entities;
    using DSharpPlus.Entities;

    public class CustomHelpFormatter : DefaultHelpFormatter
    {
        public CustomHelpFormatter(CommandContext ctx) : base(ctx) { }

        public override CommandHelpMessage Build()
        {
            EmbedBuilder.Color = new DiscordColor("b0dab9");
            return base.Build();
        }
    }
}