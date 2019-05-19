using System;

namespace TestBotDiscord
{
    public class Profile
    {
        public string Nickname { get; private set; }

        public string UserId { get; private set; }
        public string GuildId { get; private set; }

        public int zenith;
        public int win;
        public int lose;

        public DateTime daily;

        public Profile(string username, string userId, string guildId, int zenith, int win, int lose, DateTime daily)
        {
            this.Nickname = username;
            this.UserId = userId;
            this.GuildId = guildId;
            this.zenith = zenith;
            this.win = win;
            this.lose = lose;
            this.daily = daily;
        }

        public void UpdateProfile()
        {
            ProfileDB.Instance().UpdateProfile(zenith, win, lose,daily,UserId,GuildId);
        }
    }
}
