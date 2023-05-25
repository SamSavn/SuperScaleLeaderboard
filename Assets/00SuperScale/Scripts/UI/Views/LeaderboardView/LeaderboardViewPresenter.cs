namespace SuperScale.UI.Views
{
    public class LeaderboardViewPresenter : Presenter<ILeaderboardView, ILeaderboardViewModel>
    {
        public LeaderboardViewPresenter(ILeaderboardView view, ILeaderboardViewModel model) : base(view, model)
        {
            Model.RequestData(View.SetEntries);                        
        }

        public override void OnViewEnter()
        {
            base.OnViewEnter();
            View.ShowEntries();
        }

        public void ExitButtonPressed()
        {
            Navigator.Navigate(new LoadingView());
        }
    }
}