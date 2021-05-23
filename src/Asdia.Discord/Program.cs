namespace Asdia.Discord
{
    using Asdia.Discord.Commands;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using DSharpPlus;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Entities;

    public class Program
    {
        public static string[] prefixes = new[] { "a!" };

        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = File.ReadAllText("token.txt"),
                TokenType = TokenType.Bot
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = prefixes
            });

            commands.RegisterCommands<About>();
            commands.RegisterCommands<Misc>();
            commands.RegisterCommands<Moderation>();

            var act = new DiscordActivity("with your mom", ActivityType.Playing);
            await discord.ConnectAsync(act, UserStatus.DoNotDisturb).ConfigureAwait(false);

            await Task.Delay(-1);
        }

        public static DiscordEmbedBuilder CreateErrorEmbed(Exception e)
        {
            return new DiscordEmbedBuilder
            {
                Color = new DiscordColor("#FF0000"),
                Title = "An exception occurred when executing a command.",
                Description = $"```{e.Message}```",
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
