using UnityEngine.UIElements;

namespace SuperScale.UI.Components.Layout
{
    public class Column : Flex
    {
        public new class UxmlFactory : UxmlFactory<Column, UxmlTraits> { }
        public override string ID => "column";

        public Column()
        {
            AddToClassList(ID);
        }
    }
}