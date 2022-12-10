using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
