using UnityEngine.UIElements;

namespace SuperScale.UI.Components
{
    public abstract class Flex : AbstractComponent
    {
        public new class UxmlTraits : AbstractComponent.UxmlTraits
        {
            private readonly UxmlEnumAttributeDescription<Justify> _mainAxisAlignmentAttribute = new UxmlEnumAttributeDescription<Justify> { name = "main-axis-alignment", defaultValue = Justify.FlexStart };
            private readonly UxmlEnumAttributeDescription<Align> _crossAxisAlignmentAttribute = new UxmlEnumAttributeDescription<Align> { name = "cross-axis-alignment", defaultValue = Align.Stretch };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var flex = (Flex)ve;
                flex.MainAxisAlignment = _mainAxisAlignmentAttribute.GetValueFromBag(bag, cc);
                flex.CrossAxisAlignment = _crossAxisAlignmentAttribute.GetValueFromBag(bag, cc);
            }
        }

        public Justify MainAxisAlignment
        {
            get => resolvedStyle.justifyContent;
            set => style.justifyContent = value;
        }

        public Align CrossAxisAlignment
        {
            get => resolvedStyle.alignItems;
            set => style.alignItems = value;
        }

        public override string ID => null;
        public override void GetElements() { }
    }
}
