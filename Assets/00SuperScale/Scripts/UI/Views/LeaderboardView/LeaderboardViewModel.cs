using System;
using UnityEngine;
using SuperScale.Data;
using SuperScale.Services;

namespace SuperScale.UI.Views
{
    public class LeaderboardViewModel : Model<LeaderboardData>, ILeaderboardViewModel
    {
        private readonly LeaderboardRepository _repo;
        private readonly LeaderboardInfo _leaderboardInfo;
        private readonly PlayerPrefsInfo _playerPrefsInfo;

        private int _entriesToRequest;

        public LeaderboardViewModel()
        {
            _repo = new LeaderboardRepository();

            InfoService infoService = ServiceRegistry.Get<InfoService>();
            _leaderboardInfo = infoService.LeaderboardInfo;
            _playerPrefsInfo = infoService.PlayerPrefsInfo;
        }

        public void RequestData(Action<LeaderboardData> callback)
        {
            int openCount = PlayerPrefs.GetInt(_playerPrefsInfo.LeaderboardOpenCountPPKey, 0);
            int maxCount = _leaderboardInfo.LeaderboardPossibleEntries.Length - 1;

            if(openCount > maxCount)
            {
                openCount = 0;
            }

            _entriesToRequest = _leaderboardInfo.LeaderboardPossibleEntries[openCount];
            PlayerPrefs.SetInt(_playerPrefsInfo.LeaderboardOpenCountPPKey, ++openCount);

            _repo.Initialize(_entriesToRequest, OnDataRetrieved);

            void OnDataRetrieved()
            {
                SetData(_repo.Data);
                PlayerPrefs.SetString(_playerPrefsInfo.PlayerUidPPKey, Data.playerUID);
                callback?.Invoke(Data);
            }
        }
    }
}