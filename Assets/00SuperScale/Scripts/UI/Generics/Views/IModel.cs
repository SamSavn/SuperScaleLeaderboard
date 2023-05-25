namespace SuperScale.UI.Views
{
    public interface IModel
    {

    }

    public interface IModel<TData> : IModel
    {
        TData GetData();
        void SetData(TData data);
    } 
}
