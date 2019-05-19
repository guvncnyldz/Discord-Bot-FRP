using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace TestBotDiscord
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                //Logların ne kadar ayrıntılı olacağı
                LogLevel = LogSeverity.Info
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                //Büyük küçük harf duyarlığı
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Info
            });

            //Mesaj gelme durumuna event veriyoruz
            Client.MessageReceived += MessageReceived;
            //Logları yazıyoruz
            Client.Log += Log;

            Client.SetGameAsync("!yardım");
            //Bot Token
            await Client.LoginAsync(TokenType.Bot, "");
            await Client.StartAsync();

            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            await Task.Delay(-1);
        }

        private async Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
        }

        private async Task MessageReceived(SocketMessage socketMessage)
        {
            //SocketMessage'ı UserMessage'e çeviriyoruz
            var Message = socketMessage as SocketUserMessage;
            //Mesaj yoksa veya içeriği boşsa eventi bitir
            if (Message == null || Message.Content == "")
                return;

            //Komut prefixinin kaçıncı karakterden sonra biteceğini takip etmek için tanımladığımız değişken
            int argPos = 0;
            //Komut prefixi yazılmadıysa veya bot etiketlenmediyse veya komutu yazan bir botsa eventi bitir
            if (!(Message.HasCharPrefix('!', ref argPos) ||
                Message.HasMentionPrefix(Client.CurrentUser, ref argPos)) ||
                Message.Author.IsBot)
                return;
            //Client ve Message arasında bağlantı oluşturuyoruz. Bu bize yeni yerlere ulaşmamızı sağlıyor
            //Client, Channel, Guild, User, Message gibi
            var Context = new SocketCommandContext(Client, Message);
            //Komutu çalıştırıyoruz
            var Result = await Commands.ExecuteAsync(Context, argPos, null);
            //Hatalı komut girilmesi durumunda konsola mesajı tarihi ve yazan kullanıcıyı ekliyoruz
            if (!Result.IsSuccess)
            {
                var chnl = Message.Channel as SocketGuildChannel;
                Console.WriteLine($"{DateTime.Now} {Result} Mesaj = {Message}, Kullanıcı = {Message.Author.Username}, Kanal = {chnl.Guild.Name}");

            }
        }
    }
}
