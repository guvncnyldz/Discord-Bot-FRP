using System;

namespace TestBotDiscord
{
    public class Dice
    {
        public int Total;

        public string ReadDice(string message)
        {
            string diceCount = "";
            string diceType = "";
            //Zar sonuçlarını depolayacağımız string
            string result = "";

            bool beforeD = true;
            //Komutta hangi zarların atılmasını istediğimizi okuyor
            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == 'd')
                {
                    if (diceCount == "")
                        diceCount = "1";
                    beforeD = false;
                    continue;
                }
                else if (beforeD && Control.numberControl(message[i]))
                {
                    diceCount += message[i];
                }
                else if (!beforeD && Control.numberControl(message[i]))
                {
                    diceType += message[i];
                }
                else
                {
                    if (message[i] != ' ')
                    {
                        result = "Geçersiz Komut";
                        return result;
                    }
                }
                //Eğer okuma işlemi yapıldıysa ve zar atma işlemi yapılıyor
                if (!beforeD && (message[i] == ' ' || i == message.Length - 1))
                {
                    result += "[**" + diceCount + "d" + diceType + "** : ";
                    //Zar burada atılıyor
                    RollDice(ref result, diceCount, diceType);
                    result += "\n\n";
                    diceCount = "";
                    diceType = "";
                    beforeD = true;
                    continue;
                }

            }
            result += "";
            return result;
        }

        private void RollDice(ref string result, string diceCount, string diceType)
        {
            int critical = 0;
            int fail = 0;

            int diceCountInt = Convert.ToInt16(diceCount);
            int diceTypeInt = Convert.ToInt16(diceType);

            Random random = new Random();

            //Zar atma işlemi
            for (int i = 1; i <= diceCountInt; i++)
            {
                int diceResult = random.Next(1, diceTypeInt + 1);

                result += diceResult.ToString();
                if (i != diceCountInt)
                    result += ",";

                Total += diceResult;
                //Yüksek ve düşük kritikleri belirliyor
                if (diceResult == diceTypeInt)
                    critical++;
                if (diceResult == 1)
                    fail++;
            }
            result += "]" + Environment.NewLine + "**Toplam:** " + Total + Environment.NewLine + "**Yüksek Kritik:** " + critical + Environment.NewLine + "**Düşük Kritik:** " + fail;
        }
    }
}
