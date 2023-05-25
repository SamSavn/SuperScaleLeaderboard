using UnityEngine.UIElements;

namespace SuperScale.UI.Views
{
    public abstract class OverlayView<TPresenter, TView> : View<TPresenter, TView>
        where TPresenter : IPresenter where TView : IView
    {
        public OverlayView(VisualTreeAsset asset) : base(asset)
        {
            AddToClassList("stack");
        }

        public OverlayView()
        {
            AddToClassList("stack");
        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }
    }
}
