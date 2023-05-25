using UnityEngine;

namespace SuperScale.Data
{
    [CreateAssetMenu(fileName = "GameInfo", menuName = "SuperScale/Data/Game Info")]
    public class GameInfo : ScriptableObject
    {
        [SerializeField] PlayerPrefsInfo _playerPrefsInfo;
        [SerializeField] LeaderboardInfo _leaderboardInfo;
        [SerializeField] AddressesInfo _addressesInfo;
        [SerializeField] TransitionsInfo _transitionsInfo;

        public PlayerPrefsInfo PlayerPrefsInfo => _playerPrefsInfo;
        public LeaderboardInfo LeaderboardInfo => _leaderboardInfo;
        public AddressesInfo AddressesInfo => _addressesInfo;
        public TransitionsInfo TransitionsInfo => _transitionsInfo;
    }
}
