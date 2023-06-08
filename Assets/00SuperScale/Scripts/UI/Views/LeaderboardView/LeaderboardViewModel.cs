using System;
using UnityEngine;
using SuperScale.Data;
using SuperScale.Services;

namespace SuperScale.UI.Views
{
    public class LeaderboardViewModel : Model<LeaderboardData>, ILeaderboardViewModel
    {
        private readonly LeaderboardRepository _repo;
        private readonly InfoService _infoService;

        private LeaderboardInfo _leaderboardInfo;
        private PlayerPrefsInfo _playerPrefsInfo;

        private int _entriesToRequest;

        public LeaderboardViewModel()
        {
            _repo = new LeaderboardRepository();
            _infoService = ServiceRegistry.Get<InfoService>();
        }

        private bool HasInfo<T>(ref T info) where T : AbstractInfo
        {
            return info != null || _infoService.TryGet(out info);
        }

        public void RequestData(Action<LeaderboardData> callback)
        {
            if(!HasInfo(ref _leaderboardInfo) || !HasInfo(ref _playerPrefsInfo))
            {
                Debug.LogError("Unable to retrieve needed info");
                return;
            }

            int openCount = PlayerPrefs.GetInt(_playerPrefsInfo?.LeaderboardOpenCountPPKey, 0);
            int maxCount = _leaderboardInfo.LeaderboardPossibleEntries.Length - 1;

            if(openCount > maxCount)
            {
                openCount = 0;
            }

            _entriesToRequest = _leaderboardInfo.LeaderboardPossibleEntries[openCount];
            PlayerPrefs.SetInt(_playerPrefsInfo?.LeaderboardOpenCountPPKey, ++openCount);

            _repo.Initialize(_entriesToRequest, OnDataRetrieved);

            void OnDataRetrieved()
            {
                SetData(_repo.Data);
                PlayerPrefs.SetString(_playerPrefsInfo?.PlayerUidPPKey, Data.playerUID);
                callback?.Invoke(Data);
            }
        }
    }
}