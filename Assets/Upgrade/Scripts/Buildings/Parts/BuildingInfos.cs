using LittleMars.Common;
using System;
using Zenject;

namespace LittleMars.Buildings.Parts
{
    /// <summary>
    /// Class for building info (type, size).
    /// </summary>    
    public class BuildingInfos
    {
        //public BuildingType Type { get; private set; }
        //public Size Size { get; private set; }
        Settings _settings;
        public BuildingInfos(Settings settings)
        {
            _settings = settings;
        }

        [Serializable]
        public class Settings
        {
            public BuildingType Type;
            public Size Size;
        }

        public class Factory: PlaceholderFactory<BuildingInfos>
        {

        }
    }

    /// <summary>
    /// Settings for BuildingInfos class.
    /// </summary>
    [Serializable]
    public class BuildingInfoSettings
    {
        public BuildingType Type;
        public Size Size;
    }
}
