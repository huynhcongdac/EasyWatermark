using System;

namespace EasyWatermark.Storage
{
    public abstract class DataManager<TModel> : IDataManager<TModel> where TModel : class
    {
        private DataStorage _storage;
        private readonly string DataKey = "configs";
        public abstract TModel Default { get; }
        public DataManager(Type type, string fileName = null)
        {
            _storage = string.IsNullOrEmpty(fileName) ? new DataStorage(type) : new DataStorage(fileName, type);
        }

        public TModel Load()
        {
            return _storage.GetAs(DataKey, Default);
        }

        public void Update(TModel model)
        {
            _storage.AddOrUpdate(DataKey, model);
        }

        public void DynamicUpdate(Action<TModel> modelAction)
        {
            var model = _storage.GetAs(DataKey, Default);
            modelAction?.Invoke(model);
            _storage.AddOrUpdate(DataKey, model);
        }
    }
}
