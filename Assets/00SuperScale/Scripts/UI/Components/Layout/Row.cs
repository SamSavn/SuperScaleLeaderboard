using UnityEngine.UIElements;

namespace SuperScale.UI.Components.Layout
{
    public class Row : Flex
    {
        public new class UxmlFactory : UxmlFactory<Row, UxmlTraits> { }
        public override string ID => "row";

        public Row()
        {
            AddToClassList(ID);
        }
    }
}