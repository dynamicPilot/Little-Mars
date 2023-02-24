using System;

namespace LittleMars.Common.Levels
{
    /// <summary>
    /// Resources, building types settings for level.
    /// </summary>
    [Serializable]
    public class LevelConditions
    {
        public ResourceUnit<float>[] Resources;
        public BuildingUnit<int>[] BuildingTypes;
        public ResourceUnit<float>[] TradeUnits;
    }
}
