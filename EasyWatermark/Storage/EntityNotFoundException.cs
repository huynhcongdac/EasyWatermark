using System;

namespace EasyWatermark.Storage
{
    public class ObjectNotFoundException<T> : Exception
    {
        public ObjectNotFoundException(T item) : base($"{item} not found")
        {

        }
    }
}
