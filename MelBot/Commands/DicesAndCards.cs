using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace TestBotDiscord
{
    public class DicesAndCards : ModuleBase<SocketCommandContext>
    {
        [Command("z")]
        public async Task DiceCommand([Remainder] string dices)
        {
            dices = dices.ToLower();

            Dice dice = new Dice();

            EmbedBuilder builder = new EmbedBuilder();
            //Zar atma işlemini gerçekleştiriyor. bknz: Dice.cs
            string result = dice.ReadDice(dices);

            builder.WithTitle("**İşte Zarın!**").
                WithDescription(result).
                WithColor(Color.Gold);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("k")]
        public async Task CardCommand()
        {
            //Kart çekme işlemini gerçekleştiriyor. bknz: Card.cs
            await ReplyAsync("Üzgünüm, kartlarımı yanıma almayı unutmuşum");
        }

    }
}
