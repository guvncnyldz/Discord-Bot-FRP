using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System;

namespace TestBotDiscord
{
    public class ProfileCommands : ModuleBase<SocketCommandContext>
    {
        [Command("kayıt")]
        public async Task RegisterCommand([Remainder] string nickname)
        {
            bool result = ProfileDB.Instance().Register(nickname, Context.User.Id.ToString(),Context.Guild.Id.ToString());
            
            if (result)
                await ReplyAsync($"{nickname} adlı profil oluşturuldu");
            else
                await ReplyAsync($"Mevcut bir profiliniz bulunmakta. !profil");
        }

        [Command("profil")]
        public async Task ProfileCommand()
        {
            Profile p = ProfileDB.Instance().FindProfile(Context.User.Id.ToString(),Context.Guild.Id.ToString());

            if (p == null)
            {
                await ReplyAsync("Hesabınıza ait profil bulunamadı");
            }
            else
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle($"**{p.Nickname}**").
                     WithColor(Color.DarkBlue).
                     WithDescription($"Zenith: {p.zenith}\n Zafer: {p.win}\n Kayıp: {p.lose}");

                await ReplyAsync("", false, builder.Build());
            }
        }

        [Command("günlük")]
        public async Task DailyCommand()
        {
            Profile p = ProfileDB.Instance().FindProfile(Context.User.Id.ToString(), Context.Guild.Id.ToString());

            if (p == null)
            {
                await ReplyAsync("Hesabınıza ait profil bulunamadı");
            }
            else
            {
                if(p.daily.AddDays(1) <= DateTime.Now)
                {
                    p.zenith += 25;
                    p.daily = DateTime.Now;
                    p.UpdateProfile();
                    await ReplyAsync("25 zenith kazandın, yarın da gel");
                }else
                    await ReplyAsync("Ödül için 1 gün beklemelisin\nSon ödül tarihi: '"+p.daily+"'");
            }
        }
    }
}
