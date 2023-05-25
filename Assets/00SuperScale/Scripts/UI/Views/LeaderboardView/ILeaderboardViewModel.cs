using SuperScale.Data;
using System;

namespace SuperScale.UI.Views
{
    public interface ILeaderboardViewModel : IModel
    {
        void RequestData(Action<LeaderboardData> callback);
    }
}
