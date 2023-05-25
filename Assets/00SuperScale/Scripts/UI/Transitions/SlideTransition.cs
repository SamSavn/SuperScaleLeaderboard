using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace SuperScale.UI.Transitions
{
    public enum SlideMode
    {
        In,
        Out
    }

    public class SlideTransition : Transition
    {
        private SlideMode _mode;
        private Vector2 _offset;
        private Tween _tween;

        public SlideTransition(VisualElement target, Vector2 offset, SlideMode mode, float duration) : base(target, duration)
        {
            _offset = offset;
            _mode = mode;
        }

        public override ITransition Start()
        {
            if (Target?.parent == null)
            {
                CompleteEventHandler();
                return this;
            }

            Vector2 from = GetStartValue(_mode, _offset);
            Vector2 to = GetEndValue(_mode, _offset);

            Target.transform.position = from;
            _tween = DOTween.To(() => (Vector2)Target.transform.position, x => Target.transform.position = x, to, Duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(CompleteEventHandler);

            return this;
        }

        public override ITransition Stop()
        {
            _tween?.Kill();
            return this;
        }

        public override ITransition Reset()
        {
            Target.SetLayout(new Rect(0, 0, Target.layout.width, Target.layout.height));
            return this;
        }

        public override ITransition Reverse()
        {
            return new SlideTransition(Target, _offset, GetReversedSlideMode(_mode), Duration);
        }

        private Vector2 GetStartValue(SlideMode mode, Vector2 offset) => mode switch
        {
            SlideMode.In => new Vector2(Target.parent.resolvedStyle.width * offset.x, Target.parent.resolvedStyle.height * offset.y),
            SlideMode.Out => new Vector2(0, 0),
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

        private Vector2 GetEndValue(SlideMode mode, Vector2 offset) => GetStartValue(GetReversedSlideMode(mode), offset);

        private SlideMode GetReversedSlideMode(SlideMode mode) => mode switch
        {
            SlideMode.In => SlideMode.Out,
            _ => SlideMode.In
        };
    }
}
