using System;
using System.Collections.Generic;
using SuperScale.Data;

namespace SuperScale.Services
{
    /// <summary>
    /// Responsable for caching the assets once their loaded.
    /// The assets are stored in different caches by type.
    /// </summary>
    public class CacheService : Service
    {
        private readonly Dictionary<Type, object> _caches;

        public CacheService()
        {
            _caches = new Dictionary<Type, object>();
        }

        private bool TryGetCache<T>(out Cache<T> cache)
        {
            Type type = typeof(T);
            cache = null;

            if (_caches.TryGetValue(type, out object result))
            {
                if (result is Cache<T> typedCache)
                {
                    cache = typedCache;
                }
            }

            return cache != null;
        }

        public bool TryGet<T>(string key, out T obj)
        {
            if(TryGetCache(out Cache<T> cache))
            {
                return cache.TryGetValue(key, out obj);
            }

            obj = default;
            return false;
        }

        /// <summary>
        /// Saves an asset in the corresponding cache
        /// </summary>
        /// <typeparam name="T">Type of the asset</typeparam>
        /// <param name="key">Address of the asset</param>
        /// <param name="obj">Asset</param>
        public void Save<T>(string key, T obj)
        {
            if(TryGetCache(out Cache<T> cache))
            {
                cache.Save(key, obj);
                return;
            }

            Type type = typeof(T);
            Cache<T> newCache = new Cache<T>();
            newCache.Save(key, obj);
            _caches[type] = newCache;
        }

        /// <summary>
        /// Disposes a cache of a type if any
        /// </summary>
        /// <typeparam name="T">Type of cache</typeparam>
        public void DisposeCache<T>()
        {
            if(TryGetCache(out Cache<T> cache))
            {
                cache.Dispose();
            }
        }

        public override void Dispose()
        {
            _caches.Foreach(pair =>
            {
                if(pair.Value is IDisposable disposableCache)
                {
                    disposableCache.Dispose();
                }
            });

            _caches.Clear();
            ServiceRegistry.Unregister<CacheService>();
        }
    }    
}
