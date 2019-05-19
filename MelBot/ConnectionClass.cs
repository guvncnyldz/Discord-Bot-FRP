using System;
using System.Collections.Generic;
using System.Text;

namespace TestBotDiscord
{
    class ConnectionClass
    {
        //Build alındıktan sonra bu baglanti cümlesi kullanılmalı
        public static string BaglantiCumlesi = @"Data Source=|DataDirectory|MelBot.s3db";

        //Eğer derleyicideysek SiparisVeritabani.s3db yolu aşağıda @"Data Source=BURAYA" girilmeli
        //public static string BaglantiCumlesi = @"Data Source=";
    }
}
