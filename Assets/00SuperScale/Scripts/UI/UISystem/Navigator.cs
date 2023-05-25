using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using SuperScale.UI.Views;
using SuperScale.Utils;
using SuperScale.Services;

namespace SuperScale.UI
{
    public static class Navigator
    {
        private static UIService _uiService;
        private static UIDocument _uiDocument;
        private static VisualElement _root;

        private static List<IView> _activeViews = new List<IView>();

        private static IView _currentView;
        private static IView _destination;
        private static IView _viewToClose;

        private static bool Initialized => _uiDocument != null;

        private static bool IsViewActive(IView view)
        {
            return _activeViews.Any(x => x.ID == view.ID);
        }

        public static void Initialize(UIDocument uIDocument)
        {
            if (Initialized && ReferenceEquals(_uiDocument, uIDocument))
            {
                return;
            }

            _uiService = ServiceRegistry.Get<UIService>();
            _uiDocument = uIDocument;
            _root = _uiDocument?.rootVisualElement;
        }

        private static void AddView(IView view)
        {
            _root.Add(view.VisualElement);

            if (!IsViewActive(_currentView))
            {
                _activeViews.Add(view);
            }
        }

        private static void RemoveView(IView view)
        {
            view.Dispose();
            view.VisualElement.RemoveFromHierarchy();

            if (IsViewActive(_currentView))
            {
                _activeViews.Remove(view);
            }
        }

        public static void Navigate(IView destination)
        {
            if (!Initialized)
            {
                Debug.LogError("Navigator not initialized. Use Initialize(UIDocument uIDocument) to initialize the navigator before trying to navigate");
                return;
            }

            if (!_uiService.CanTranslate || destination == null || destination == _currentView)
            {
                return;
            }

            _destination = destination;

            if (_currentView != null)
            {
                _viewToClose = _currentView;
                _uiService.TransitionExit(_viewToClose, OnCurrentViewExit);
            }

            _currentView = _destination;

            if (!_currentView.IsReady())
            {
                _uiService.PrepareView(_currentView, OnCurrentViewReady);
            }
            else
            {
                OnCurrentViewReady();
            }
        }

        private static void OnCurrentViewExit()
        {
            if (!_destination.Overlay)
            {
                RemoveView(_viewToClose);
            }
        }

        private static void OnCurrentViewReady()
        {
            if (!IsViewActive(_currentView))
            {
                AddView(_currentView);
            }

            _uiService.TransitionEnter(_currentView);
        }
    } 
}
