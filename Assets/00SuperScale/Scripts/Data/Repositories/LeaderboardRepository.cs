using UnityEngine;
using System;
using Newtonsoft.Json;
using SuperScale.Services;

namespace SuperScale.Data
{
    public class LeaderboardRepository : Repository<LeaderboardData, TextAsset>
    {
        private AddressesInfo _adrsInfo;
        private int _entriesToRequest;

        public void Initialize(int entriesToRequest, Action onDataLoaded)
        {
            _adrsInfo = ServiceRegistry.Get<InfoService>().AddressesInfo;
            _entriesToRequest = entriesToRequest;

            base.Initialize($"{_adrsInfo.DataAdrsPrefix}{_entriesToRequest}", onDataLoaded);
        }

        protected override void OnDataLoaded(TextAsset data)
        {
            Data = JsonConvert.DeserializeObject<LeaderboardData>(data.text);
            base.OnDataLoaded(data);
        }
    }
}
