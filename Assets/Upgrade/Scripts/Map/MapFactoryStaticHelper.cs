using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Map.Helpers
{
    public static class MapFactoryStaticHelper
    {
        public static List<BuildingType> GetBuildingTypesForSlot(BuildingType[] initialTypes)
        {
            List<BuildingType> types = new List<BuildingType>();

            if (initialTypes[0] == BuildingType.none) return types;
            else if (initialTypes[0] == BuildingType.all)
            {
                for (int i = 0; i < (int)BuildingType.all; i++) types.Add((BuildingType)i);
            }
            else
            {
                for (int i = 0; i < initialTypes.Length; i++) types.Add(initialTypes[i]);
            }

            return types;
        }

        public static List<Resource> GetResourcesForSlot(Resource[] initialResources)
        {
            List<Resource> resources = new List<Resource>();

            if (initialResources[0] == Resource.none) return resources;
            else if (initialResources[0] == Resource.all)
            {
                for (int i = 0; i < (int)Resource.all; i++) resources.Add((Resource)i);
            }
            else
            {
                for (int i = 0; i < initialResources.Length; i++) resources.Add(initialResources[i]);
            }

            return resources;
        }
    }
}
