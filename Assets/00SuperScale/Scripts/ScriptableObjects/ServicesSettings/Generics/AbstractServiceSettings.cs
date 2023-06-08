using UnityEngine;

namespace SuperScale.Services.Settings
{
    public abstract class AbstractServiceSettings : ScriptableObject
    {
        public abstract void Initialize();
    }

    public abstract class AbstractServiceSettings<TService> : AbstractServiceSettings
        where TService : Service
    {
        protected TService _service;

        protected void Register()
        {
            ServiceRegistry.Register(_service);
        }

        protected void SetService(TService service)
        {
            _service = service;
            Register();
        }
    } 
}
