using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using SuperScale.UI.Views;
using SuperScale.Services;
using System.Collections;
using System;

namespace SuperScale.UI
{
    /// <summary>
    /// Responsable for changing views
    /// </summary>
    public static class Navigator
    {
        private static UIService _uiService;
        private static CoroutineService _coroutineService;

        private static UIDocument _uiDocument;
        private static VisualElement _root;
        private static LoaderView _loader;

        private static Coroutine _showLoaderCoroutine;
        private static WaitForSeconds _waitForShowLoader;
        private static WaitForSeconds _waitForHideLoader;

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
            _coroutineService = ServiceRegistry.Get<CoroutineService>();

            _waitForShowLoader = new WaitForSeconds(_uiService.UIInfo.TimeToShowLoader);
            _waitForHideLoader = new WaitForSeconds(_uiService.UIInfo.MinTimeToShowLoader);

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


        /// <summary>
        /// Navigates to destination
        /// </summary>
        /// <param name="destination">The destination view</param>
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
                if (!IsViewActive(_currentView))
                {
                    TryShowLoader(); 
                }

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
            TryHideLoader(() => 
            {
                if (!IsViewActive(_currentView))
                {
                    AddView(_currentView);
                }

                _uiService.TransitionEnter(_currentView);
            });
        }

        public static void TryShowLoader()
        {
            if (_showLoaderCoroutine == null)
            {
                _showLoaderCoroutine = _coroutineService.StartCoroutine(ShowLoader()); 
            }
        }

        private static IEnumerator ShowLoader()
        {
            yield return _waitForShowLoader;
            _loader = new LoaderView(_uiService.UIInfo.LoaderAsset);
            _root.Add(_loader);
        }

        private static void TryHideLoader(Action callback)
        {
            if (_loader != null)
            {
                _coroutineService.StartCoroutine(HideLoader(callback));
            }
            else
            {
                _coroutineService.StopCoroutine(_showLoaderCoroutine);
                callback?.Invoke();
            }
        }

        private static IEnumerator HideLoader(Action callback)
        {
            yield return _waitForHideLoader;
            _loader.RemoveFromHierarchy();
            callback?.Invoke();
        }
    } 
}
