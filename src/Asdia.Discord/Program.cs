namespace Asdia.Discord
{
    using Asdia.Discord.Commands;
    using System.IO;
    using System.Threading.Tasks;
    using DSharpPlus;
    using DSharpPlus.CommandsNext;

    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = File.ReadAllText("token.txt"),
                TokenType = TokenType.Bot
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "a!" }
            });

            commands.RegisterCommands<About>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

    }
}
