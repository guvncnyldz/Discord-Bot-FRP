using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace TestBotDiscord
{
    public class GeneralCommands : ModuleBase<SocketCommandContext>
    {
        [Command("yardım")]
        public async Task Help()
        {
            string help = "Genel bilgiler için **!yardım genel**\nProfil bilgileri için **!yardım profil**\n5 taş oyunu için **!yardım 5tas**\nMelbot hakkında bilgiler için **!yardım melbot**\n";

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Ne hakkında yardım istiyorsun '"+Context.User.Username+"' \n\n").
                WithDescription(help).
                WithColor(Color.Green);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("yardım")]
        public async Task Help2([Remainder]string helpAbout)
        {
            string help = "Geçersiz komut girdin";

            if (helpAbout.ToLower() == "5tas")
            {
                help = "5 taş oynamak için oyunu oynayacak iki oyuncunun da **!oyna** komutunu kullanması gerekli.\nZarlar atılır ve oyun büyük zar gelen oyuncu ile başlar.\n **!topla** komutu ile yerdeki taşları toplayabilirsin.\n2 taştan fazlasını ilk toplayan kazanır.\nYerde 1 taş kalması durumunda iki oyuncu da **!topla** komutunu kullanır ve büyük zar atan oyuncu kazanır.\nHer seferinde tek bir oyun oynanabilir. \nİki oyuncu da 1 dakika içinde hamle yapmazsa yeni oyun başlatılabilir\n";

                helpAbout = "5 taş oyunu hakkında bilgiler";
            }
            else if (helpAbout.ToLower() == "profil")
            {
                help = "Profil oluşturmak için **!kayıt 'kullanıcı adı'.**\nProfil bilgilerini görmek için **!profil** komutlarını kullanabilirsin.\n**!günlük** komutu ile her gün 25 zenith kazabilirsin\n\nBir profil oluşturduktan sonra, profilini silemezsin!";

                helpAbout = "Profil hakkında bilgiler";
            }
            else if (helpAbout.ToLower() == "genel")
            {
                help ="Zar Atmak İçin\n\n!z komutunu kullanabilirsin.\n\nÖrnek: !z 1d8\nÖrnek: !z 2d12 6d10\n\nKart Çekmek İçin\n\nHenüz kart sistemi aktif değildir";

                helpAbout = "Kullanabileceğin genel komutlar";
            }
            else if(helpAbout.ToLower() == "melbot")
            {
                help ="Melbot frp oynamaya yardımcı açık kaynak kodlu bir discord botudur.\n\nGeliştirmek veya incelemek için: https://github.com/guvncnyldz/Discord-Bot-FRP";

                helpAbout = "Melbot 2019";
            }

            if (help == "Geçersiz komut girdin")
                helpAbout = "Başka bir şeyler sormayı dene";

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("'"+helpAbout+"'\n\n").
                WithDescription(help).
                WithColor(Color.LightOrange);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
