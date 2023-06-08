using UnityEngine;
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

        public InfoService(GameInfo gameInfo)
        {
            GameInfo = gameInfo;
        }

        /// <summary>
        /// Tries getting info
        /// </summary>
        /// <typeparam name="T">The type of the info</typeparam>
        /// <param name="info">The out value</param>
        /// <returns>True if there's any, False if there's none</returns>
        public bool TryGet<T>(out T info) where T : AbstractInfo
        {
            if(GameInfo.TryGetInfo(out info))
            {
                return true;
            }

            Debug.LogError($"Unable to retrieve info of type {typeof(T).Name}. Make sure it is added to the list in GameInfo ScriptableObject");
            
            info = null;
            return false;
        }

        /// <summary>
        /// Gets info
        /// </summary>
        /// <typeparam name="T">The type of the info</typeparam>
        /// <returns>The requested info if available, else returns null</returns>
        public T Get<T>() where T : AbstractInfo
        {
            if(TryGet(out T result))
            {
                return result;
            }

            return null;
        }

        public override void Dispose()
        {
            ServiceRegistry.Unregister<InfoService>();
        }
    }
}
