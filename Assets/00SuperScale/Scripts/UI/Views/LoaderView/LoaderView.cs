using UnityEngine.UIElements;
using SuperScale.UI.Transitions;

namespace SuperScale.UI.Views
{
    public class LoaderView : OverlayView<LoaderViewPresenter, ILoaderView>, ILoaderView
    {
        public override string ID => "loader";
        public override ITransition GetEnterTransition() => null;
        public override ITransition GetExitTransition() => null;

        public LoaderView(VisualTreeAsset asset) : base(asset)
        {
            BringToFront();
        }

        public LoaderView()
        {
            Presenter = new LoaderViewPresenter(this);
            RegisterAssetLoadedListener();
        }

        public override void GetElements()
        {
            TriggerReadyState();
        }
    }
}