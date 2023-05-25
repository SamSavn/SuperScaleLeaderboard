using System;
using UnityEngine.UIElements;

namespace SuperScale.UI.Views
{
    public interface IView : IDisposable
    {
        string ID { get; }
        bool Overlay { get; }
        VisualElement VisualElement { get; }
        bool IsReady();

        void Enter(Action callback = null);
        void Exit(Action callback = null);
        void RegisterReadyListener(Action callback);
    } 
}
