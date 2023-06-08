using UnityEngine;

namespace SuperScale.Services.Settings
{
    [CreateAssetMenu(fileName = "AssetsServiceSettings", menuName = "SuperScale/ServiceSettings/Services/Assets Service")]
    public class AssetsServiceSettings : AbstractServiceSettings<AssetsService>
    {
        public override void Initialize()
        {
            SetService(new AssetsService());
        }
    }
}
