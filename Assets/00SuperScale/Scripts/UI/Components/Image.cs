using SuperScale.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace SuperScale.UI.Components
{
    public class Image : AbstractComponent
    {
        public new class UxmlFactory : UxmlFactory<Image, UxmlTraits> { }

        public override string ID => "image";

        public Sprite Background
        {
            get => resolvedStyle.backgroundImage.sprite;
            set => style.backgroundImage = new StyleBackground(value);
        }

        public Color Color
        {
            get => resolvedStyle.unityBackgroundImageTintColor;
            set => style.unityBackgroundImageTintColor = value;
        }

        public Image()
        {
            AddToClassList(ID);
        }

        public void SetBackround(string spriteAddress)
        {
            ServiceRegistry.Get<AssetsService>().GetOrLoadAsset<Sprite>(spriteAddress, sprite =>
            {
                Background = sprite;
            });
        }

        public void SetColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color color))
            {
                Color = color;
            }
            else
            {
                Color = Color.white;
            }
        }

        public override void GetElements() { }
    }
}