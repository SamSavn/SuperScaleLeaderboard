using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperScale.Services
{
    public static class ServiceRegistry
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static T Get<T>() where T : Service
        {
            Type type = typeof(T);
            if (_services.ContainsKey(type))
            {
                return (T)_services[type];
            }
            else
            {
                throw new Exception($"Service of type {type} not found in the registry.");
            }
        }

        public static void Register<T>(T service) where T : Service
        {
            Type type = typeof(T);
            _services.AddOrReplace(type, service);
        }

        public static void Unregister<T>() where T : Service
        {
            Type type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }

        public static void Dispose()
        {
            Type[] key = _services.Keys.ToArray();

            for (int i = 0; i < key.Length; i++)
            {
                var pair = new KeyValuePair<Type, object>(key[i], _services[key[i]]);
                if (pair.Value is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    } 
}
