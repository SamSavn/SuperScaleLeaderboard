using UnityEngine;
using UnityEngine.UIElements;
using SuperScale.UI;
using SuperScale.UI.Views;
using SuperScale.Services;
using SuperScale.Services.Settings;
using SuperScale.Data;

namespace SuperScale.Managers
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private GameInfo _gameInfo;
        [SerializeField] private ServicesSettings _servicesSttings;
        [SerializeField] private UIDocument _rootUiDocument;

        private void Awake()
        {
            _servicesSttings.Initialize();
            Navigator.Initialize(_rootUiDocument);
        }

        private void Start()
        {
            Navigator.Navigate(new LoadingView());
        }

        private void OnApplicationQuit()
        {
            ServiceRegistry.Dispose();
        }
    }    
}
