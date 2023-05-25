using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System;

namespace SuperScale.UI.Views
{
    public abstract class View<TPresenter, TView> : AbstractView
        where TPresenter : IPresenter where TView : IView
    {
        public override string ID { get; }
        protected TPresenter Presenter;

        protected override void OnEnter()
        {
            base.OnEnter();
            Presenter?.OnViewEnter();
        }

        protected override void OnExit()
        {
            base.OnExit();
            Presenter?.OnViewExit();
        }
    }
}
