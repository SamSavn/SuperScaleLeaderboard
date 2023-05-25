using UnityEngine.UIElements;
using SuperScale.UI.Transitions;

namespace SuperScale.UI.Components
{
    public abstract class Component : AbstractComponent
    {
        public override string ID { get; }
        public abstract ITransition GetEnterTransition();

        public Component()
        {
            RegisterCallback<AttachToPanelEvent>(AttachToPanelEventHandler);
        }

        public override void GetElements() { }

        public virtual void OnShow()
        {
            GetEnterTransition()?.Start();
        }

        private void AttachToPanelEventHandler(AttachToPanelEvent evt)
        {
            RegisterAssetLoadedListener(OnShow);
        }
    }
}
