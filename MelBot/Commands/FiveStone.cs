using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System;

namespace TestBotDiscord
{
    public class FiveStone : ModuleBase<SocketCommandContext>
    {
        [Command("Oyna")]
        public async Task StartGame()
        {
            if (StaticGame.game == null)
            {
                StaticGame.game = new Game();
            }

            if (!StaticGame.game.isStart)
            {
                await ReplyAsync(StaticGame.game.StartGame(Context.User.Id.ToString(), Context.Guild.Id.ToString()));

                if (StaticGame.game.isStart)
                {
                    await ReplyAsync(StaticGame.game.WhenStart(), false, Embeder().Build());
                }
            }
            else if (StaticGame.game.startTime.AddMinutes(1) < DateTime.UtcNow)
            {
                StaticGame.game = null;
                StaticGame.game = new Game();

                await ReplyAsync(StaticGame.game.StartGame(Context.User.Id.ToString(), Context.Guild.Id.ToString()));

                if (StaticGame.game.isStart)
                {
                    await ReplyAsync(StaticGame.game.WhenStart(), false, Embeder().Build());
                }
            }
            else
                await ReplyAsync("Şu anda bir oyun oynanıyor, 1 dakika içinde hamle yapılmazsa yeni oyun başlatabilirsin");
        }

        [Command("topla")]
        public async Task TakeStone()
        {
            if (StaticGame.game == null)
            {
                await ReplyAsync("Henüz başlamış oyun yok. Başlatmak için !oyun");
                return;
            }

            if ((Context.User.Id.ToString() == StaticGame.game.player1.UserId && StaticGame.game.isPlayer1Turn) || (Context.User.Id.ToString() == StaticGame.game.player2.UserId && !StaticGame.game.isPlayer1Turn))
            {
                string result = StaticGame.game.PlayGame();
                await ReplyAsync(result, false, Embeder().Build());
            }

            if (StaticGame.game.stoneP1 > 2)
            {
                Winner(true);
                EmbedBuilder embed = new EmbedBuilder();

                embed.WithTitle($"{StaticGame.game.player1.Nickname} oyunu kazandı!").
                    WithDescription($"Kazananın yeni zenith miktarı: {StaticGame.game.player1.zenith}\nYeni oyuna başlamak için !oyna").
                    WithColor(Color.Red);

                await ReplyAsync("", false, embed.Build());

                StaticGame.game = null;
            }
            else if (StaticGame.game.stoneP2 > 2)
            {
                Winner(false);
                EmbedBuilder embed = new EmbedBuilder();

                embed.WithTitle($"{StaticGame.game.player2.Nickname} oyunu kazandı!").
                    WithDescription($"Kazananın yeni zenith miktarı: {StaticGame.game.player2.zenith}\nYeni oyuna başlamak için !oyna").
                    WithColor(Color.Red);

                await ReplyAsync("", false, embed.Build());

                StaticGame.game = null;
            }
            else if (StaticGame.game.stoneP1 == 2 && StaticGame.game.stoneP2 == 2)
            {
                StaticGame.game.startLevel = false;
                await ReplyAsync("Yerde 1 taş kaldı, ilk alan kazanır");
            }
        }

        private void Winner(bool isPlayer1)
        {
            if (isPlayer1)
            {
                StaticGame.game.player1.zenith += 100;
                StaticGame.game.player2.lose++;
                StaticGame.game.player1.win++;
            }
            else
            {
                StaticGame.game.player2.zenith += 100;
                StaticGame.game.player1.lose++;
                StaticGame.game.player2.win++;
            }

            StaticGame.game.player1.UpdateProfile();
            StaticGame.game.player2.UpdateProfile();
        }
        private EmbedBuilder Embeder()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle($"**Yerde {StaticGame.game.stoneG} adet taş var**").
                WithColor(Color.LightOrange).
                AddField($"Oyuncu 1: {StaticGame.game.player1.Nickname}", $"Kalan zenith: {StaticGame.game.player1.zenith}\nElindeki taş: {StaticGame.game.stoneP1}", true).
                AddField($"Oyuncu 2: {StaticGame.game.player2.Nickname}", $"Kalan zenith: {StaticGame.game.player2.zenith}\nElindeki taş: {StaticGame.game.stoneP2}", true);

            return builder;
        }
    }
}
