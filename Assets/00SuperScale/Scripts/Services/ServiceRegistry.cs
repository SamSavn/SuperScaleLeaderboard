using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperScale.Services
{
    /// <summary>
    /// Responsable for holding and retrieving Services
    /// </summary>
    public static class ServiceRegistry
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// Gets a Service
        /// </summary>
        /// <typeparam name="T">The type of the service</typeparam>
        /// <returns>Service</returns>
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

        /// <summary>
        /// Registers a Service in the registry
        /// </summary>
        /// <typeparam name="T">The type of the service</typeparam>
        /// <param name="service">The service to register</param>
        public static void Register<T>(T service) where T : Service
        {
            Type type = typeof(T);
            _services.AddOrReplace(type, service);
        }

        /// <summary>
        /// Unregisters a Service from the registry
        /// </summary>
        /// <typeparam name="T">The type of the service to unregister</typeparam>
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
