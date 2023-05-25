using UnityEngine;

namespace SuperScale.UI.Views
{
    public abstract class Presenter<TView> : IPresenter where TView : IView
    {
        public readonly TView View;

        public Presenter(TView view)
        {
            View = view;
        }

        public virtual void OnViewEnter()
        {

        }

        public virtual void OnViewExit()
        {

        }
    }


    public abstract class Presenter<TView, TModel> : Presenter<TView>
        where TView : IView where TModel : IModel
    {
        public readonly TModel Model;

        public Presenter(TView view, TModel model) : base(view)
        {
            Model = model;
        }
    } 
}
