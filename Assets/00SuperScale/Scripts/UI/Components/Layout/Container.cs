using UnityEngine.UIElements;

namespace SuperScale.UI.Components.Layout
{
    public class Container : AbstractComponent
    {
        public new class UxmlFactory : UxmlFactory<Container, UxmlTraits> { }
        public override string ID => "container";

        public Container()
        {
            AddToClassList(ID);
        }

        public override void GetElements()
        {
            
        }
    }
}