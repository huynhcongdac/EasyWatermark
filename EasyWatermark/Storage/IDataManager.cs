namespace EasyWatermark.Storage
{
    public interface IDataManager<T>
    {
        T Load();
        void Update(T item);
    }
}
