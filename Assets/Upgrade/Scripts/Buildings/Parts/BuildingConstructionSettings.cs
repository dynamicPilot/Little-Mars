using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Buildings.Parts
{

    [Serializable]
    public class BuildingConstructionSettings
    {
        public ResourceUnit<float>[] ResourcesForBuilding;
        public Resource[] ResourcesInMap;        
        public Path BuildingPath;
    }

}
