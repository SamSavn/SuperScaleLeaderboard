using System;
using UnityEngine;
using SuperScale.Services;
using SuperScale.Utils;

namespace SuperScale.Data
{
    /// <summary>
    /// Repositories are responsable for loading, parsing and holding data
    /// </summary>
    /// <typeparam name="TData">Type of data the repository will hold</typeparam>
    public abstract class Repository<TData> : Repository<TData, TData>
    {

    }

    /// <summary>
    /// Repositories are responsable for loading, parsing and holding data
    /// </summary>
    /// <typeparam name="TData">Type of data the repository will hold</typeparam>
    /// <typeparam name="TAsset">Type of asset to load</typeparam>
    public abstract class Repository<TData, TAsset> : IDisposable
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

        public virtual void Dispose()
        {
            Data = default(TData);
            _notifier.Clear();
        }
    }
}
