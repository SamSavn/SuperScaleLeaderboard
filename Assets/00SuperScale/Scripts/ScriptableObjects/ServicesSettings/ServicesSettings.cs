using UnityEngine;

namespace SuperScale.Services.Settings
{
    [CreateAssetMenu(fileName = "ServicesSettings", menuName = "SuperScale/ServiceSettings/Services Settings")]
    public class ServicesSettings : ScriptableObject
    {
        [SerializeField] private AbstractServiceSettings[] _settings;

        public void Initialize()
        {
            for (int i = 0; i < _settings.Length; i++)
            {
                _settings[i]?.Initialize();
            }
        }
    } 
}
