using LittleMars.Buildings.Parts;
using System;
using UnityEngine;

namespace LittleMars.Common
{
    [CreateAssetMenu(menuName = "LittleMars/BuildingObject")]
    public class BuildingObject : ScriptableObject
    {
        public InfoSettings Info;
        public ConstructionSettings Construction;
        public OperationSettings Operation;
        public ViewSettings View;

        [Serializable]
        public class InfoSettings
        {
            public BuildingType Type;
            public Size Size;
        }

        [Serializable]
        public class ConstructionSettings
        {
            public ResourceUnit<float>[] ResourcesForBuilding;
            public Resource[] ResourcesInMap;
            public Path BuildingPath;
        }

        [Serializable]
        public class OperationSettings
        {
            public Priority Priority;
            public ResourceUnit<float>[] DayProduction;
            public ResourceUnit<float>[] NightProduction;
            public BuildingType[] Connections;
            public ResourceUnit<float>[] Needs;
        }

        [Serializable]
        public class ViewSettings
        {
            public Sprite Sprite;
            public Sprite Shape;
        } 
    }
}
