namespace SuperScale.UI.Views
{
    public class LoadingViewPresenter : Presenter<ILoadingView>
    {
        public LoadingViewPresenter(ILoadingView view) : base(view)
        {
        
        }

        public void StartButtonPressed()
        {
            Navigator.Navigate(new LeaderboardView(new LeaderboardViewModel()));
        }
    }
}