using System;
using UnityEngine;
using SuperScale.Services;
using SuperScale.Utils;

namespace SuperScale.Data
{
    public abstract class Repository<TData> : Repository<TData, TData>
    {

    }

    public abstract class Repository<TData, TAsset>
    {
        private ActionNotifier _notifier = new ActionNotifier();
        public TData Data { get; protected set; }
        private string _assetKey;

        protected virtual void Initialize(string key, Action onDataLoaded)
        {
            _assetKey = key;
            _notifier.Subscribe(onDataLoaded);
            LoadData();
        }

        protected void LoadData()
        {
            if (string.IsNullOrEmpty(_assetKey))
            {
                Debug.LogWarning("Unable to load asset: AssetKey not set");
                _notifier.Clear();
                return;
            }

            ServiceRegistry.Get<AssetsService>().GetOrLoadAsset<TAsset>(_assetKey, OnDataLoaded);
        }

        protected virtual void OnDataLoaded(TAsset data)
        {
            _notifier.Notify();
        }
    }
}
