namespace AsdiaBot.Discord
{
    using AsdiaBot.Discord.Commands;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using DSharpPlus;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Entities;
    using DSharpPlus.EventArgs;

    public class Program
    {
        private const ulong amongusID = 845950499248537600;

        private const ulong yeahOkayID = 861949970886295552;

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = File.ReadAllText("AsdiaBot/token.txt"),
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) => await OnMessage(s, e);

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "a!", "!" }
            });

            commands.RegisterCommands<About>();
            commands.RegisterCommands<Misc>();
            commands.RegisterCommands<Moderation>();
            commands.RegisterCommands<Elo>();

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
                Description = $"```{e.GetType()}```",
                Timestamp = DateTime.UtcNow
            };
        }

        public static async Task OnMessage(BaseDiscordClient client, MessageCreateEventArgs e)
        {
            if (e.Author.IsBot)
            {
                return;
            }

            if (e.Author.Id == 849283709173366834)
            {
                await e.Message.CreateReactionAsync(DiscordEmoji.FromGuildEmote(client, yeahOkayID));
                return;
            }

            if (e.Message.Content.ToLower().Contains("sus"))
            {
                await e.Message.CreateReactionAsync(DiscordEmoji.FromGuildEmote(client, amongusID));
            }

            if (e.Message.Content.ToLower().Contains("dunkaccino"))
            {
                await e.Channel.SendMessageAsync("https://www.youtube.com/watch?v=AeLuQQH1OHA");
            }

            if (e.Message.Content.ToLower().Contains("en passant"))
            {
                await e.Channel.SendMessageAsync("Holy hell").ConfigureAwait(false);
            }

            if (e.Message.Content.ToLower().Contains("pipi"))
            {
                await e.Channel.SendMessageAsync("Are you kidding ??? What the *\\*** are you talking about man ? You are a biggest looser i ever seen in my life ! You was doing PIPI in your pampers when i was beating players much more stronger then you! You are not proffesional, because proffesionals knew how to lose and congratulate opponents, you are like a girl crying after i beat you! Be brave, be honest to yourself and stop this trush talkings!!! Everybody know that i am very good blitz player, i can win anyone in the world in single game! And \"w\"esley \"s\"o is nobody for me, just a player who are crying every single time when loosing, ( remember what you say about Firouzja ) !!! Stop playing with my name, i deserve to have a good name during whole my chess carrier, I am Officially inviting you to OTB blitz match with the Prize fund! Both of us will invest 5000$ and winner takes it all!").ConfigureAwait(false);
            }
        }
    }
}
