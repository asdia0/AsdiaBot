namespace AsdiaBot.Discord
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;
    using DSharpPlus.Entities;
    using EloCalculator;

    [Group("Elo Game")]
    public class Game : Elo
    {
        [Command("add")]
        [Description("Adds a game.")]
        public async Task AddGameCommand(CommandContext ctx, string whiteName, string blackName, string gameResult, string isRated)
        {
            DiscordRole chessAdmin = ctx.Guild.GetRole(ulong.Parse(Program.Servers[long.Parse(ctx.Guild.Id.ToString())]["chess"]));

            if (chessAdmin == null)
            {
                await ctx.Channel.SendMessageAsync("Chess admin role must be set using `admin addChess` first.");
                return;
            }

            if (!ctx.Member.Roles.Contains(chessAdmin))
            {
                await ctx.Channel.SendMessageAsync("Only chess admins can add games.");
                return;
            }

            Player white = PlayerDatabase.Players.Where(i => i.Name == whiteName).FirstOrDefault();
            Player black = PlayerDatabase.Players.Where(i => i.Name == blackName).FirstOrDefault();
            Result? result = null;
            bool? rated = null;

            if (white == null)
            {
                await ctx.Channel.SendMessageAsync($"Create player {whiteName} with `elo player add` first.");
                return;
            }

            if (black == null)
            {
                await ctx.Channel.SendMessageAsync($"Create player {blackName} with `elo player add` first.");
                return;
            }

            switch (gameResult)
            {
                case "white":
                    result = Result.White;
                    break;
                case "black":
                    result = Result.Black;
                    break;
                case "draw":
                    result = Result.Draw;
                    break;
            }

            if (result == null)
            {
                await ctx.Channel.SendMessageAsync($"Result must be  \"white\", \"black\" or \"draw\".");
                return;
            }

            switch (isRated)
            {
                case "true":
                    rated = true;
                    break;
                case "false":
                    rated = false;
                    break;
            }

            if (rated == null)
            {
                await ctx.Channel.SendMessageAsync($"Result must be either \"true\" or \"false\".");
                return;
            }

            EloCalculator.Game game = new EloCalculator.Game(white, black, (Result)result, DateTime.Now, (bool)rated);

            await ctx.Channel.SendMessageAsync($"Createed game {game.ID}.");
        }
    }
}
