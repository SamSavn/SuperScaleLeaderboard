using UnityEngine;

namespace SuperScale.Data
{
    [CreateAssetMenu(fileName = "PlayerPrefsInfo", menuName = "SuperScale/Data/PlayerPrefs Info")]
    public class PlayerPrefsInfo : AbstractInfo
    {
        [SerializeField] private string _playerUidPPKey;
        [SerializeField] private string _leaderboardOpenCountPPKey;

        public string PlayerUidPPKey => _playerUidPPKey;
        public string LeaderboardOpenCountPPKey => _leaderboardOpenCountPPKey;
    }
}
