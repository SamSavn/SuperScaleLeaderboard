using SuperScale.Data;
using UnityEngine;

namespace SuperScale.Services.Settings
{
    [CreateAssetMenu(fileName = "InfoServiceSettings", menuName = "SuperScale/ServiceSettings/Services/Info Service")]
    public class InfoServiceSettings : AbstractServiceSettings<InfoService>
    {
        [SerializeField] private GameInfo _gameInfo;

        public override void Initialize()
        {
            SetService(new InfoService(_gameInfo));
        }
    } 
}
