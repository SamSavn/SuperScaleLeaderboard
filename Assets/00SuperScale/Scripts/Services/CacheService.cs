using System;
using System.Collections.Generic;
using SuperScale.Data;

namespace SuperScale.Services
{
    public class CacheService : Service
    {
        private readonly Dictionary<Type, object> _caches;

        public CacheService()
        {
            _caches = new Dictionary<Type, object>();
        }

        public bool TryGet<T>(string key, out T obj)
        {
            Type type = typeof(T);

            if (_caches.TryGetValue(type, out object cache))
            {
                if (cache is Cache<T> typedCache)
                {
                    return typedCache.TryGetValue(key, out obj);
                }
            }

            obj = default;
            return false;
        }

        public void Save<T>(string key, T obj)
        {
            Type type = typeof(T);

            if (_caches.TryGetValue(type, out object cache))
            {
                if (cache is Cache<T> typedCache)
                {
                    typedCache.Save(key, obj);
                    return;
                }
            }

            Cache<T> newCache = new Cache<T>();
            newCache.Save(key, obj);
            _caches[type] = newCache;
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
