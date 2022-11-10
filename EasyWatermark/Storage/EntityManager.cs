using System.Collections.Generic;

namespace EasyWatermark.Storage
{
    public abstract class EntityManager<T> : IObjectManager<T>
    {
        public static readonly object SynchronizedObject = new object();

        private DataStorage _storage;
        public EntityManager(DataStorage storage)
        {
            _storage = storage;
        }

        protected abstract string DataKeyName { get; }

        protected virtual List<T> GetItems()
        {
            return _storage.GetAs(DataKeyName, new List<T>());
        }

        protected virtual int FindIndex(List<T> list, T item)
        {
            return list.FindIndex(t => t.Equals(item));
        }

        public virtual void Add(T item)
        {
            lock (SynchronizedObject)
            {
                var items = GetItems();
                items.Add(item);
                _storage.AddOrUpdate(DataKeyName, items);
            }
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            lock (SynchronizedObject)
            {
                var oldItems = GetItems();
                foreach (var item in items)
                {
                    oldItems.Add(item);
                }
                _storage.AddOrUpdate(DataKeyName, oldItems);
            }
        }

        public virtual IEnumerable<T> Load()
        {
            return GetItems();
        }

        public virtual void Remove(IEnumerable<T> items)
        {
            lock (SynchronizedObject)
            {
                var oldItems = GetItems();
                var indexToRemove = new List<int>();
                foreach (var item in items)
                {
                    var index = FindIndex(oldItems, item);
                    if (index == -1)
                    {
                        throw new ObjectNotFoundException<T>(item);
                    }
                    indexToRemove.Add(index);
                }
                var itemToRemove = new List<T>();
                foreach (var index in indexToRemove)
                {
                    itemToRemove.Add(oldItems[index]);
                }
                foreach (var item in itemToRemove)
                {
                    oldItems.Remove(item);
                }
                _storage.AddOrUpdate(DataKeyName, oldItems);
            }
        }

        public virtual T Update(T item)
        {
            lock (SynchronizedObject)
            {
                var oldItems = GetItems();
                var index = FindIndex(oldItems, item);
                if (index == -1)
                {
                    throw new ObjectNotFoundException<T>(item);
                }
                oldItems[index] = item;
                _storage.AddOrUpdate(DataKeyName, oldItems);
                return item;
            }
        }

        public virtual void BulkUpdate(IEnumerable<T> items)
        {
            lock (SynchronizedObject)
            {
                var oldItems = GetItems();
                var dataToUpdate = new Dictionary<int, T>();
                foreach (var item in items)
                {
                    var index = FindIndex(oldItems, item);
                    if (index == -1)
                    {
                        throw new ObjectNotFoundException<T>(item);
                    }
                    dataToUpdate.Add(index, item);
                }
                foreach (var item in dataToUpdate)
                {
                    oldItems[item.Key] = item.Value;
                }
                _storage.AddOrUpdate(DataKeyName, oldItems);
            }
        }

        public void Clear()
        {
            lock (SynchronizedObject)
            {
                _storage.AddOrUpdate(DataKeyName, new List<T>());
            }
        }
    }
}
