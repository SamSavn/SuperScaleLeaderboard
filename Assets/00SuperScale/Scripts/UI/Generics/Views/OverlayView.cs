namespace SuperScale.UI.Views
{
    public abstract class OverlayView<TPresenter, TView> : View<TPresenter, TView>
        where TPresenter : IPresenter where TView : IView
    {
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
