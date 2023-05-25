using System;
using UnityEngine.UIElements;

namespace SuperScale.Data
{
    public class ComponentRepository : Repository<VisualTreeAsset>
    {
        public new void Initialize(string id, Action onDataLoaded)
        {
            base.Initialize(id, onDataLoaded);
        }

        protected override void OnDataLoaded(VisualTreeAsset data)
        {
            Data = data;
            base.OnDataLoaded(data);
        }
    } 
}
