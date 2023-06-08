using UnityEngine;

namespace SuperScale.Services.Settings
{
    [CreateAssetMenu(fileName = "CacheServiceSettings", menuName = "SuperScale/ServiceSettings/Services/Cache Service")]
    public class CacheServiceSettings : AbstractServiceSettings<CacheService>
    {
        public override void Initialize()
        {
            SetService(new CacheService());
        }
    }
}
