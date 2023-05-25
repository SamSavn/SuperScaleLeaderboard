using UnityEngine;

namespace SuperScale.Data
{
    [CreateAssetMenu(fileName = "LeaderboardInfo", menuName = "SuperScale/Data/Leaderboard Info")]
    public class LeaderboardInfo : ScriptableObject
    {
        [SerializeField] private int _leaderboardBadgesNumber;
        [SerializeField] private int[] _leaderboardPossibleEntries;

        public int[] LeaderboardPossibleEntries => _leaderboardPossibleEntries;
        public int LeaderboardBadgesNumber => _leaderboardBadgesNumber;
    }
}
