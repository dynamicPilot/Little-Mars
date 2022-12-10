using System;
using Zenject;

namespace LittleMars.Common
{
    [Serializable]
    public class ResourceField<T>
    {
        public Resource Resource;
        public T[] Values;

        public class Factory: PlaceholderFactory<ResourceField<T>>
        {

        }
    }
}
