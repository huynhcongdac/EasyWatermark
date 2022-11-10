using System;
using System.IO;
using System.Reflection;

namespace EasyWatermark.Storage
{
    public abstract class DataMemorizer<T>
    {
        public IDataManager<T> DataManager { get; }

        public DataMemorizer(string fileName)
        {
            var startUpPath = Assembly.GetExecutingAssembly().Location;
            var fileNameToSave = Path.Combine(startUpPath, fileName);
            DataManager = new JsonDataManager<T>(fileNameToSave);
        }

        public DataMemorizer(IDataManager<T> dataManager)
        {
            DataManager = dataManager;
        }

        public T Load()
        {
            return DataManager.Load();
        }

        public void Update(T item)
        {
            DataManager.Update(item);
            OnChanged(item);
        }

        public event EventHandler<T> Changed;
        protected virtual void OnChanged(T item)
        {
            Changed?.Invoke(this, item);
        }
    }
}
