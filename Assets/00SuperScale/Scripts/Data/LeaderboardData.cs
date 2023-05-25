using System.Collections.Generic;

namespace SuperScale.Data
{
    public struct LeaderboardData
    {
        public List<LeaderboardEntryData> ranking { get; set; }
        public string playerUID { get; set; }
    }

    public struct LeaderboardEntryData
    {
        public PlayerData player { get; set; }
        public int ranking { get; set; }
        public int points { get; set; }
    }

    public struct PlayerData
    {
        public string uid { get; set; }
        public string username { get; set; }
        public bool isVip { get; set; }
        public string countryCode { get; set; }
        public string characterColor { get; set; }
        public int characterIndex { get; set; }
    }
}
