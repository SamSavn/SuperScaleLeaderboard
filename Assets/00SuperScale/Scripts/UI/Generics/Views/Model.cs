namespace SuperScale.UI.Views
{
    public class Model : IModel
    {

    }

    public abstract class Model<TData> : IModel<TData>
    {
        protected TData Data;

        public TData GetData()
        {
            return Data;
        }

        public void SetData(TData data)
        {
            Data = data;
        }
    } 
}
