using UnityEngine.UIElements;
using DG.Tweening;

namespace SuperScale.UI.Transitions
{
    public class OpacityTransition : Transition
    {
        private float _from;
        private float _to;
        private Tween _tween;

        public OpacityTransition(VisualElement target, float from, float to, float duration) : base(target, duration)
        {
            _from = from;
            _to = to;
        }

        public override ITransition Start()
        {
            Target.style.opacity = _from;
            _tween = DOTween.To(() => Target.style.opacity.value, x => Target.style.opacity = x, _to, Duration)
            .OnComplete(CompleteEventHandler);

            return this;
        }

        public override ITransition Stop()
        {
            _tween?.Kill();
            return this;
        }

        public override ITransition Reverse()
        {
            return new OpacityTransition(Target, _to, _from, Duration);
        }

        public override ITransition Reset()
        {
            Target.style.opacity = _from;
            return this;
        }
    }
}
