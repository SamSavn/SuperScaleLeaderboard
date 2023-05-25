using UnityEngine;
using UnityEngine.UIElements;
using SuperScale.UI;
using SuperScale.UI.Views;
using SuperScale.Services;
using SuperScale.Data;

public class Initializer : MonoBehaviour
{
    [SerializeField] private GameInfo _gameInfo;
    [SerializeField] private UIDocument _rootUiDocument;

    private void Awake()
    {
        InitializeServices();
        Navigator.Initialize(_rootUiDocument);
    }

    private void Start()
    {
        Navigator.Navigate(new LoadingView());
    }

    private void InitializeServices()
    {
        ServiceRegistry.Register(new InfoService(_gameInfo));
        ServiceRegistry.Register(new CoroutineService(this));
        ServiceRegistry.Register(new AssetsService());
        ServiceRegistry.Register(new CacheService());
        ServiceRegistry.Register(new UIService(_gameInfo.UIInfo));
    }

    private void OnApplicationQuit()
    {
        ServiceRegistry.Dispose();
    }
}
