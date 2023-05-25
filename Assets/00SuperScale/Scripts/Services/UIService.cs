using System;
using System.Linq;
using System.Collections.Generic;
using SuperScale.UI.Views;
using SuperScale.Utils;
using SuperScale.Data;

namespace SuperScale.Services
{
    /// <summary>
    /// Responsable for managing UI events
    /// </summary>
    public class UIService : Service
    {
        public readonly UIInfo UIInfo;

        private readonly ActionNotifier _viewExitNotifier;
        private readonly ActionNotifier _viewEnterNotifier;

        private readonly List<IView> _preparingViews;
        private readonly List<IView> _transitioningViews;

        public bool CanTranslate => _transitioningViews.Count == 0 && _preparingViews.Count == 0;

        public UIService(UIInfo info)
        {
            UIInfo = info;

            _viewExitNotifier = new ActionNotifier();
            _viewEnterNotifier = new ActionNotifier();

            _transitioningViews = new List<IView>();
            _preparingViews = new List<IView>();
        }

        private bool IsPreparing(IView view) => _preparingViews.Any(x => x.ID == view.ID);
        private bool IsTransitioning(IView view) => _transitioningViews.Any(x => x.ID == view.ID);

        public void PrepareView(IView view, Action callback = null)
        {
            if (view == null)
            {
                return;
            }

            if (!IsPreparing(view))
            {
                _preparingViews.Add(view);                 
            }

            view.RegisterReadyListener(() => OnViewReady(view, callback));
        }

        public void TransitionEnter(IView view, Action callback = null)
        {
            SetTransition(_viewEnterNotifier, view, callback);
            view.Enter(() => _viewEnterNotifier.Notify());
        }

        public void TransitionExit(IView view, Action callback = null)
        {
            SetTransition(_viewExitNotifier, view, callback);
            view.Exit(() => _viewExitNotifier.Notify());
        }

        private void SetTransition(ActionNotifier notifier, IView view, Action callback)
        {
            if (view == null)
            {
                return;
            }

            if (!IsTransitioning(view))
            {
                _transitioningViews.Add(view); 
            }

            notifier.Subscribe(() => OnTransitionComplete(view, callback));
        }

        private void OnViewReady(IView view, Action callback)
        {
            if (IsPreparing(view))
            {
                _preparingViews.Remove(view); 
            }

            callback?.Invoke();
        }

        private void OnTransitionComplete(IView view, Action callback)
        {
            if (IsTransitioning(view))
            {
                _transitioningViews.Remove(view); 
            }

            callback?.Invoke();
        }

        public override void Dispose()
        {
            _viewEnterNotifier.Clear();
            _viewExitNotifier.Clear();

            _transitioningViews.Clear();
            _preparingViews.Clear();

            ServiceRegistry.Unregister<UIService>();
        }
    }
}
