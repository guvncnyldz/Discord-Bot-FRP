using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBotDiscord
{
    public class Game
    {
        public Profile player1, player2;
        public int diceP1, diceP2;

        public int stoneP1 = 0, stoneP2 = 0, stoneG = 5;

        public bool isPlayer1Turn = true;
        public bool isStart = false;

        public bool startLevel = true;

        public DateTime startTime;

        private Dice gameDice;

        public string StartGame(string userId, string guildId)
        {
            if (player1 == null)
            {
                player1 = ProfileDB.Instance().FindProfile(userId, guildId);

                if (player1 == null)
                    return "Lütfen oyuna başlamadan önce profil oluşturun";
                else if (player1.zenith < 50)
                {
                    player1 = null;
                    return "Yeterli zenithe sahip değilsin";
                }

                return "İkinci oyuncu bekleniyor";
            }
            else if (player2 == null)
            {
                player2 = ProfileDB.Instance().FindProfile(userId, guildId);

                if (player2 == null)
                    return "Lütfen oyuna başlamadan önce profil oluşturun";
                else if (player2.zenith < 50)
                {
                    player2 = null;
                    return "Yeterli zenithe sahip değilsin";
                }

                isStart = true;

                player1.zenith -= 50;
                player2.zenith -= 50;

                startTime = DateTime.UtcNow;
                return "50'şer zenith alındı, 2 taştan fazlasını ilk toplayan kazanır. Oyun başlıyor.";
            }

            return "";
        }

        public string PlayGame()
        {
            string result = "```";

            if (startLevel)
            {
                result += "([5d10] : ";
                for (int i = 0; i < 5; i++)
                {
                    gameDice.ReadDice("d10");
                    result += gameDice.Total.ToString();

                    if (gameDice.Total > 5)
                    {
                        if (isPlayer1Turn)
                            stoneP1++;
                        else
                            stoneP2++;

                        stoneG--;
                    }

                    if (stoneG == 0)
                        break;

                    gameDice.Total = 0;

                    if (i != 4)
                        result += ",";
                }
                isPlayer1Turn = !isPlayer1Turn;
                result += ")";
            }
            else
            {
                gameDice.ReadDice("3d10");

                if (isPlayer1Turn)
                {
                    diceP1 = gameDice.Total;
                    result += $"{player1.Nickname} attığı zar: {diceP1}.";
                    gameDice.Total = 0;
                }
                else
                {
                    diceP2 = gameDice.Total;
                    result += $"{player2.Nickname} attığı zar: {diceP2}.";
                    gameDice.Total = 0;
                }

                if (diceP1 != 0 && diceP2 != 0)
                {
                    if (diceP1 > diceP2)
                    {
                        stoneP1++;
                        stoneG--;
                    }
                    else if (diceP1 < diceP2)
                    {
                        stoneP2++;
                        stoneG--;
                    }
                    else
                        result += "Birbirinizin eline çarptınız. Tekrar zar atın";
                }

                isPlayer1Turn = !isPlayer1Turn;
                result += ")";
            }

            result += "```";
            return result;
        }
        public string WhenStart()
        {
            if (gameDice == null)
            {
                gameDice = new Dice();
            }

            do
            {
                gameDice.ReadDice("d10");
                diceP1 = gameDice.Total;
                gameDice.Total = 0;
                gameDice.ReadDice("d10");
                diceP2 = gameDice.Total;
                gameDice.Total = 0;

            } while (diceP1 == diceP2);

            string result = $"Başlangıç zarlarını atıyorum... \n{player1.Nickname} için {diceP1} ve {player2.Nickname} için {diceP2}\n";
            if (diceP1 > diceP2)
            {
                isPlayer1Turn = true;
                result += $"{player1.Nickname} oyuna başlıyor";
            }
            else
            {
                isPlayer1Turn = false;
                result += $"{player2.Nickname} oyuna başlıyor";
            }

            diceP1 = diceP2 = 0;
            return result;
        }
    }
}
