using SuperScale.Data;

namespace SuperScale.UI.Views
{
    public interface ILeaderboardView : IView
    {
        void SetEntries(LeaderboardData data);
        void ShowEntries();
    }
}