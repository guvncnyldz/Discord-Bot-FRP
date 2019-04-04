using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestBotDiscord
{
    public static class Card
    {
        public static string RandomCard()
        {
            Random random = new Random();

            int i = 0;
            int randomNumber = random.Next(0, 261);
            //Resimlerin bulunduğu klasörden random resim seçiyor
            foreach (String path in Directory.GetFiles(/*Klasör Yolu*/))
            {
                if(i==randomNumber)
                {
                    return path;
                }
                i++;
            }

            return "İşlem Başarısız";
        }
    }
}
