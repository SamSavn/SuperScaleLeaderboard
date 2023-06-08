using UnityEngine;

namespace SuperScale.Services.Settings
{
    [CreateAssetMenu(fileName = "CoroutineServiceSettings", menuName = "SuperScale/ServiceSettings/Services/Coroutine Service")]
    public class CoroutineServiceSettings : AbstractServiceSettings<CoroutineService>
    {
        [SerializeField] private GameObject _runnerPrefab;

        public override void Initialize()
        {
            CoroutineRunner _runner = Instantiate(_runnerPrefab).GetComponent<CoroutineRunner>();
            SetService(new CoroutineService(_runner));
        }
    }
}
