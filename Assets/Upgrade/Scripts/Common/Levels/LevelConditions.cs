using System;

namespace LittleMars.Common.Levels
{
    [Serializable]
    public class LevelConditions
    {
        public ResourceUnit<float>[] Resources;
        public BuildingUnit<int>[] BuildingTypes;
        public ResourceUnit<float>[] TradeUnits;

        //[Serializable]
        //public class Settings
        //{
        //    public ResourceUnit<float>[] Resources;
        //    public BuildingUnit<int>[] BuildingTypes;
        //    public ResourceUnit<float>[] TradeUnits;
        //}

    }
}
