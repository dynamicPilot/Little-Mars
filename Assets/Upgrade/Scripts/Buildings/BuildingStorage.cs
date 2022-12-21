using LittleMars.Common;
using LittleMars.Common.Interfaces;
using System.Collections.Generic;

namespace LittleMars.Buildings
{
    public class BuildingStorage
    {
        Dictionary<BuildingType, Dictionary<Size, List<IBuildingFacade>>> _buildings = new();

        public void AddToFree(IBuildingFacade building)
        {
            var info = building.Info();
            if (!_buildings.ContainsKey(info.Type)) 
                _buildings[info.Type] = new Dictionary<Size, List<IBuildingFacade>>();

            if (!_buildings[info.Type].ContainsKey(info.Size))
                _buildings[info.Type][info.Size] = new List<IBuildingFacade>();

            _buildings[info.Type][info.Size].Add(building);
        }

        public bool GetFromFree(BuildingType type, Size size, out IBuildingFacade building)
        {
            building = null;

            if (!_buildings.ContainsKey(type)) return false;
            else if (!_buildings[type].ContainsKey(size)) return false;
            else if (_buildings[type][size].Count > 0) return false;

            building = _buildings[type][size][0];
            return true;
        }
    }
}
