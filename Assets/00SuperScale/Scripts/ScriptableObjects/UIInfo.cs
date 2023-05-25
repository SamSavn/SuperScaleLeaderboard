using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SuperScale.Data
{
    [CreateAssetMenu(fileName = "LoaderInfo", menuName = "SuperScale/Data/Loader Info")]
    public class UIInfo : ScriptableObject
    {
        [Header("Loader")]
        [SerializeField] private VisualTreeAsset _loaderAsset;
        [SerializeField] private float _timeToShowLoader;
        [SerializeField] private float _minTimeToShowLoader;

        [Header("Animations")]
        [SerializeField] private float _entriesInsertDelay;
        [SerializeField] private float _entriesFadeDelay;
        [SerializeField] private float _entriesFadeDuration;


        public VisualTreeAsset LoaderAsset => _loaderAsset;
        public float TimeToShowLoader => _timeToShowLoader;
        public float MinTimeToShowLoader => _minTimeToShowLoader;
        public float EntriesInsertDelay => _entriesInsertDelay;
        public float EntriesFadeDelay => _entriesFadeDelay;
        public float EntriesFadeDuration => _entriesFadeDuration;
    } 
}
