using UnityEngine;
using UnityEngine.UIElements;

namespace SuperScale
{
    public static class VisualElementsExtentions
    {
        public static void ToggleVisibility(this VisualElement visualElement, bool show)
        {
            visualElement.style.visibility = show ? Visibility.Visible : Visibility.Hidden;
        }

        public static void SetLayout(this VisualElement visualElement, Rect layout, Position position = Position.Absolute)
        {
            var style = visualElement.style;
            style.position = position;
            style.left = layout.x;
            style.top = layout.y;
            style.right = float.NaN;
            style.bottom = float.NaN;
            style.width = layout.width;
            style.height = layout.height;
        }
    }
}
