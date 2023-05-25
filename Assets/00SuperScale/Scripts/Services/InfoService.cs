using SuperScale.Data;

namespace SuperScale.Services
{
    /// <summary>
    /// Responsable for returning infos.
    /// Infos are used to set game parameters through the editor.
    /// </summary>
    public class InfoService : Service
    {
        public readonly GameInfo GameInfo;

        public UIInfo UIInfo => GameInfo.UIInfo;
        public PlayerPrefsInfo PlayerPrefsInfo => GameInfo.PlayerPrefsInfo;
        public LeaderboardInfo LeaderboardInfo => GameInfo.LeaderboardInfo;
        public AddressesInfo AddressesInfo => GameInfo.AddressesInfo;

        public InfoService(GameInfo gameInfo)
        {
            GameInfo = gameInfo;
        }

        public override void Dispose()
        {
            ServiceRegistry.Unregister<InfoService>();
        }
    }
}
