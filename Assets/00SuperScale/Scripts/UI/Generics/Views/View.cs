using UnityEngine.UIElements;

namespace SuperScale.UI.Views
{
    public abstract class View<TPresenter, TView> : AbstractView
        where TPresenter : IPresenter where TView : IView
    {
        public override string ID { get; }
        protected TPresenter Presenter;

        public View(VisualTreeAsset asset) : base(asset)
        {

        }

        public View()
        {

        }

        protected override void OnEnter()
        {
            base.OnEnter();
            Presenter?.OnViewEnter();
        }

        protected override void OnExit()
        {
            base.OnExit();
            Presenter?.OnViewExit();
        }
    }
}
