using System;

namespace LittleMars.Common
{
    public class LevelConditions
    {
        [Serializable]
        public class Settings
        {
            public ResourceUnit<float>[] Resources;
            public BuildingUnit<int>[] BuildingTypes;
            public ResourceUnit<float>[] TradeUnits;
        }
        
    }
}
