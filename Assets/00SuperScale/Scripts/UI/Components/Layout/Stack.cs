using UnityEngine.UIElements;

namespace SuperScale.UI.Components.Layout
{
    public class Stack : AbstractComponent
    {
        public new class UxmlFactory : UxmlFactory<Stack, UxmlTraits> { }
        public override string ID => "stack";

        public Stack()
        {
            AddToClassList(ID);
        }

        public override void GetElements()
        {
            
        }
    }
}