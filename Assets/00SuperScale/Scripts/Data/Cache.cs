using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperScale.Data
{
    public class Cache<T> : IDisposable
    {
        private Dictionary<string, T> _cache;

        public Cache()
        {
            _cache = new Dictionary<string, T>();
        }

        public bool TryGetValue(string key, out T obj)
        {
            return _cache.TryGetValue(key, out obj);
        }

        public void Save(string key, T obj)
        {
            _cache.AddOrReplace(key, obj);
        }

        public void Dispose()
        {
            Type type = typeof(T);
            Type disposableType = typeof(IDisposable);
            bool disposeItems = type.GetInterfaces().Contains(disposableType);

            if (disposeItems)
            {
                _cache.Foreach(pair =>
                {
                    IDisposable asset = (IDisposable)pair.Value;
                    asset.Dispose();
                });
            }

            _cache.Clear();
        }
    } 
}
