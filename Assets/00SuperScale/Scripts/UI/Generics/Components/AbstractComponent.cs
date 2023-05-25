using System;
using UnityEngine.UIElements;
using SuperScale.Utils;
using SuperScale.Data;
using UnityEngine;

namespace SuperScale.UI.Components
{
    public abstract class AbstractComponent : VisualElement
    {
        #region UXML Traits

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlFloatAttributeDescription _grow = new UxmlFloatAttributeDescription { name = "grow", defaultValue = 0 };
            private readonly UxmlFloatAttributeDescription _shrink = new UxmlFloatAttributeDescription { name = "shrink", defaultValue = 1 };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                AbstractComponent component = (AbstractComponent)ve;

                component.Grow = _grow.GetValueFromBag(bag, cc);
                component.Shrink = _shrink.GetValueFromBag(bag, cc);
            }
        }

        public float Grow
        {
            get => resolvedStyle.flexGrow;
            set => style.flexGrow = value;
        }

        public float Shrink
        {
            get => resolvedStyle.flexShrink;
            set => style.flexShrink = value;
        }

        #endregion

        public abstract string ID { get; }

        protected readonly ActionNotifier _dataLoadedNotifier;
        private readonly ComponentRepository _repo;
        protected VisualTreeAsset _asset;

        public AbstractComponent(VisualTreeAsset asset) : this()
        {
            _asset = asset;
        }

        public AbstractComponent()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            _dataLoadedNotifier = new ActionNotifier();
            _repo = new ComponentRepository();

            CreateVisualTree();
        }

        public abstract void GetElements();

        protected virtual void CreateVisualTree()
        {
            _repo.Initialize(ID, OnDataLoaded);

            void OnDataLoaded()
            {
                _asset = _repo.Data;
                _asset?.CloneTree(this);
                _dataLoadedNotifier.Notify();
            }
        }

        protected virtual void OnAssetLoaded()
        {
            GetElements();
        }

        protected void RegisterAssetLoadedListener(Action action)
        {            
            if (_asset != null)
            {
                action?.Invoke();
            }
            else
            {
                _dataLoadedNotifier.Subscribe(action);
            }
        }

        protected void RegisterAssetLoadedListener()
        {
            RegisterAssetLoadedListener(OnAssetLoaded);
        }
    } 
}
