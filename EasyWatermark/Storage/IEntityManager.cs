using System.Collections.Generic;

namespace EasyWatermark.Storage
{
    public interface IObjectManager<T>
    {
        IEnumerable<T> Load();

        void Add(T item);

        void AddRange(IEnumerable<T> items);

        T Update(T item);

        void BulkUpdate(IEnumerable<T> items);

        void Remove(IEnumerable<T> items);

        void Clear();
    }
}
