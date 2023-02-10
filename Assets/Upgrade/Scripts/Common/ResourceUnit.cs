using System;

namespace LittleMars.Common
{
    [Serializable]
    public class ResourceUnit<T> : Unit<Resource>
    {
        public T Amount;
    }
    [Serializable]
    public class BuildingUnit<T> : WithSizeUnit<BuildingType, Size>
    {
        public T Amount;
    }

    [Serializable]
    public class Unit<T>
    {
        public T Type;
    }

    [Serializable]
    public class WithSizeUnit<T1, T2> : Unit<T1>
    {
        public T2 Size;
    }

    [Serializable]
    public class TradeUnit<T> 
    {
        public ResourceUnit<T> Need;
        public ResourceUnit<T> Production;
        public int Hour;
    }
}

