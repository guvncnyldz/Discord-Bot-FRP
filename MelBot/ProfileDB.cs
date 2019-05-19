using System;
using System.Data.SQLite;

namespace TestBotDiscord
{
    public class ProfileDB
    {
        public static ProfileDB _instance;

        SQLiteConnection connection = new SQLiteConnection(ConnectionClass.BaglantiCumlesi);

        private ProfileDB()
        { }

        public bool Register(string nickname, string userId, string guildId)
        {
            Profile p = ProfileDB._instance.FindProfile(userId, guildId);

            if (p == null)
            {
                //Veritabanına kullanıcı kaydı yapıyor
                connection.Open();

                SQLiteCommand command = new SQLiteCommand("INSERT INTO Profiles(Nickname,UserId,GuildId,Zenith,Win,Lose,daily) Values('" + nickname + "','" + userId + "','" + guildId + "','" + 200 + "','" + 0 + "','" + 0 + "',datetime('now','-1 days'))", connection);
                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Profile FindProfile(string userId, string guildId)
        {
            connection.Open();
            //Veritabanından kullanıcı buluyor
            SQLiteCommand command = new SQLiteCommand("select * from Profiles where userId = '" + userId + "' and guildId = '" + guildId + "'", connection);
            SQLiteDataReader dr = command.ExecuteReader();

            Profile p = null;
            while (dr.Read())
            {
                p = new Profile(dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), Convert.ToInt16(dr[4]), Convert.ToInt16(dr[5]), Convert.ToInt16(dr[6]),(DateTime) dr[7]);
            }

            connection.Close();

            return p;
        }

        public void UpdateProfile(int zenith, int win, int lose, DateTime daily, string userId, string guildId)
        {
            connection.Open();
            //Kullanıcı verilerini güncelliyor
            SQLiteCommand command = new SQLiteCommand("Update Profiles set zenith=@z,win=@w,lose=@l,daily=@d where userId = '" + userId + "' and guildId = '" + guildId + "'", connection);

            command.Parameters.AddWithValue("@z", zenith);
            command.Parameters.AddWithValue("@w", win);
            command.Parameters.AddWithValue("@l", lose);
            command.Parameters.AddWithValue("@d", daily);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static ProfileDB Instance()
        {
            //Singleton
            if (_instance == null)
                _instance = new ProfileDB();

            return _instance;
        }
    }
}
