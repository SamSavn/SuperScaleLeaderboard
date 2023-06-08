using UnityEngine;
using SuperScale.Data;

namespace SuperScale.Services.Settings
{
    [CreateAssetMenu(fileName = "UIServiceSettings", menuName = "SuperScale/ServiceSettings/Services/UI Service")]
    public class UIServiceSettings : AbstractServiceSettings<UIService>
    {
        [SerializeField] UIInfo _uiInfo;

        public override void Initialize()
        {
            SetService(new UIService(_uiInfo));
        }
    }
}
