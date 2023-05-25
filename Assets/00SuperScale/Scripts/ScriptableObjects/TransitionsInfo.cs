using UnityEngine;

namespace SuperScale.Data
{
    [CreateAssetMenu(fileName = "TransitionsInfo", menuName = "SuperScale/Data/Transitions Info")]
    public class TransitionsInfo : ScriptableObject
    {
        [SerializeField] private float _entriesInsertDelay;
        [SerializeField] private float _entriesFadeDelay;
        [SerializeField] private int _entriesFadeDuration;

        public float EntriesInsertDelay => _entriesInsertDelay;
        public float EntriesFadeDelay => _entriesFadeDelay;
        public int EntriesFadeDuration => _entriesFadeDuration;
    }
}
