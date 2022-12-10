using LittleMars.Buildings.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
//using BuildingInfo = LittleMars.Buildings.Parts.BuildingInfo;

namespace LittleMars.Common
{
    [CreateAssetMenu(menuName = "LittleMars/BuildingObject")]
    public class BuildingObject : ScriptableObject
    {
        public InfoSettings Info;
        public ConstructionSettings Construction;
        public OperationSettings Operation;

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

        
    }
}
