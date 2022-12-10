using LittleMars.Common;
using System;
using System.Collections.Generic;

namespace LittleMars.Map
{
    [Serializable]
    public class MapSlot
    {
        public bool IsBlocked;
        public List<Resource> Resources;
        public List<BuildingType> Buildings;
    }
}
