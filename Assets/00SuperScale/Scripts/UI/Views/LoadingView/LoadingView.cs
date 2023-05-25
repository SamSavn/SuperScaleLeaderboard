using SuperScale.UI.Transitions;
using UnityEngine;
using UnityEngine.UIElements;

namespace SuperScale.UI.Views
{
    public class LoadingView : View<LoadingViewPresenter, ILoadingView>, ILoadingView
    {
        public override string ID => "loading";

        private VisualElement _startButton;

        public LoadingView()
        {
            Presenter = new LoadingViewPresenter(this);
            RegisterAssetLoadedListener();
        }

        public override ITransition GetEnterTransition() => null;
        public override ITransition GetExitTransition() => null;

        public override void GetElements()
        {
            _startButton = this.Q<VisualElement>("start-button");
            _startButton?.RegisterCallback<ClickEvent>(StartButtonClickEventEventHandler);
            TriggerReadyState();
        }

        private void StartButtonClickEventEventHandler(ClickEvent evt)
        {
            Presenter.StartButtonPressed();
        }
    }
}