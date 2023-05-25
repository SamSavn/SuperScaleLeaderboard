using System;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace SuperScale.UI.Transitions
{
    public abstract class Transition : ITransition
    {
        public float Duration { get; }

        protected readonly VisualElement Target;
        protected IValueAnimation Animation;
        private Action CompleteCallback;

        protected Transition(VisualElement target, float duration)
        {
            Target = target;
            Duration = duration;
        }

        public abstract ITransition Start();
        public abstract ITransition Stop();
        public abstract ITransition Reset();
        public abstract ITransition Reverse();

        public ITransition OnComplete(Action onComplete)
        {
            CompleteCallback = onComplete;
            return this;
        }

        protected void CompleteEventHandler()
        {
            Stop();
            CompleteCallback?.Invoke();
        }
    }
}
