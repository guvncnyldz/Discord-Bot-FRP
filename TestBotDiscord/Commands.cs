using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using System;

namespace TestBotDiscord
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("yardım")]
        public async Task Help()
        {
            string help = 
                @"**Zar Atmak İçin** 
                
                !z komutunu kullanabilirsin. 

                Örnek: !z 1d8
                Örnek: !z 2d12 6d10

                **Kart Çekmek İçin**
                
                !k komutunu kullanabilirsin.";

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle($"**Senin için zar atabilir veya kart çekebilirim { Context.User.Username} \n\n**").
                WithDescription(help).
                WithColor(Color.Red);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("z")]
        public async Task DiceCommand([Remainder] string dices)
        {

            dices = dices.ToLower();

            EmbedBuilder builder = new EmbedBuilder();
            //Zar atma işlemini gerçekleştiriyor. bknz: Dice.cs
            string result = Dice.ReadDice(dices);

            builder.WithTitle("**İşte Zarın!**").
                WithDescription(result).
                WithColor(Color.Gold);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("k")]
        public async Task CardCommand()
        {
            //Kart çekme işlemini gerçekleştiriyor. bknz: Card.cs
            string path = Card.RandomCard();

            await Context.Channel.SendFileAsync(path);
        }

    }
}
