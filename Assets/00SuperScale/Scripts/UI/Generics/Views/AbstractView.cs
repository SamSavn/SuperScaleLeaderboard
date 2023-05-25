using System;
using SuperScale.UI.Components;
using SuperScale.UI.Transitions;
using SuperScale.Utils;
using UnityEngine.UIElements;

namespace SuperScale.UI.Views
{
    public abstract class AbstractView : AbstractComponent, IView
    {
        public VisualElement VisualElement => this;
        public bool Overlay => ClassListContains("stack");
        public bool Ready { get; protected set; }

        private readonly ActionNotifier _readyNotifier;

        public AbstractView(VisualTreeAsset asset) : base(asset)
        {
            AddToClassList("view");
        }

        public AbstractView()
        {
            AddToClassList("view");
            _readyNotifier = new ActionNotifier();

            Ready = false;
        }

        public abstract ITransition GetEnterTransition();
        public abstract ITransition GetExitTransition();

        public bool IsReady() => Ready;

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }

        public virtual void Enter(Action callback = null)
        {
            if (GetEnterTransition() == null)
            {
                OnComplete();
                return;
            }

            GetEnterTransition().Start().OnComplete(OnComplete);

            void OnComplete()
            {
                callback?.Invoke();
                OnEnter();
            }
        }

        public virtual void Exit(Action callback = null)
        {
            if (GetExitTransition() == null)
            {
                OnComplete();
                return;
            }

            GetExitTransition().Start().OnComplete(OnComplete);

            void OnComplete()
            {
                callback?.Invoke();
                OnExit();
            }
        }

        public void RegisterReadyListener(Action action)
        {
            if (Ready)
            {
                action?.Invoke();
                return;
            }

            _readyNotifier.Subscribe(action);
        }

        protected void TriggerReadyState()
        {
            Ready = true;
            _readyNotifier.Notify();
        }

        public virtual void Dispose()
        {
            _readyNotifier.Clear();
        }
    }
}
