using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#if UNITY_EDITOR
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets; 
#endif

namespace SuperScale.Services
{
    /// <summary>
    /// Responsable for loading and saving assets
    /// </summary>
    public class AssetsService : Service
    {
        private static object loadLock = new object();

        private async Task LoadAsset<T>(string address, Action<T> onAssetLoaded)
        {
            AsyncOperationHandle<T> handle;

            lock (loadLock)
            {
                handle = Addressables.LoadAssetAsync<T>(address);
            }

            await handle.Task;

            lock (loadLock)
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    T asset = handle.Result;
                    onAssetLoaded?.Invoke(asset);
                }
                else
                {
                    Debug.LogWarning($"Unable to load asset: {address}");
                }
            }

            Addressables.Release(handle);
        }

        public void GetOrLoadAsset<T>(string address, Action<T> onComplete)
        {
            if(ServiceRegistry.Get<CacheService>().TryGet(address, out T asset))
            {
                onComplete?.Invoke(asset);
            }
            else
            {
                LoadAndSaveAsset(address, onComplete);
            }
        }

        public async void LoadAndSaveAsset<T>(string address, Action<T> onComplete)
        {
            await LoadAsset<T>(address, SaveAsset);

            void SaveAsset(T asset)
            {
                onComplete?.Invoke(asset);
                ServiceRegistry.Get<CacheService>().Save(address, asset);
            }
        }

#if UNITY_EDITOR
        public void AddAssetToGroup(string path, string address, string group)
        {
            AddressableAssetSettings assetSettings = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetGroup assetGroup = assetSettings.FindGroup(group);

            if (assetGroup == null)
            {
                assetGroup = assetSettings.CreateGroup(group, false, false, true, new List<AddressableAssetGroupSchema>());
            }

            string assetGUID = UnityEditor.AssetDatabase.AssetPathToGUID(path);
            AddressableAssetEntry assetEntry = assetSettings.CreateOrMoveEntry(assetGUID, assetGroup);

            if (assetEntry == null)
            {
                Debug.LogError("Unable to create addressable asset");
                return;
            }

            assetEntry.SetLabel("Default", true, true);
            assetEntry.address = address;

            assetSettings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, assetEntry, true, true);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

            AddressableAssetSettings.BuildPlayerContent();
        }

        public void CreateFile(string path, string content)
        {
            try
            {
                using FileStream fs = File.Create(path);
                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
#endif

        public override void Dispose()
        {
            ServiceRegistry.Unregister<AssetsService>();
        }
    } 
}
