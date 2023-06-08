using UnityEngine;

namespace SuperScale.Data
{
    [CreateAssetMenu(fileName = "GameInfo", menuName = "SuperScale/Data/Game Info")]
    public class GameInfo : ScriptableObject
    {
        [SerializeField] AbstractInfo[] _info;

        public bool TryGetInfo<T>(out T info) where T : AbstractInfo
        {
            for (int i = 0; i < _info.Length; i++)
            {
                if(_info[i] is T)
                {
                    info = (T)_info[i];
                    return true;
                }
            }

            info = null;
            return false;
        }
    }
}
