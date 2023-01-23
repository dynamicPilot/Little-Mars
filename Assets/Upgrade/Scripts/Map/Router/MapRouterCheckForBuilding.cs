using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Map.Routers
{
    public class MapRouterCheckForBuilding : IMapRouterCheck
    {
        IEnumerable<Resource> _resources;
        BuildingType _type;

        public MapRouterCheckForBuilding(BuildingObject buildingObject)
        {
            _resources = buildingObject.Construction.ResourcesInMap;
            _type = buildingObject.Info.Type;
        }

        public bool Check(IEnumerable<MapSlotExtended> slots)
        {
            var resourcesMatch = new Dictionary<Resource, int>();

            foreach (Resource resource in _resources)
            {
                if (resource == Resource.none || resource == Resource.all) continue;
                resourcesMatch.Add(resource, 0);
            }
                

            foreach(MapSlotExtended slot in slots)
            {
                // check building type
                if (!slot.Buildings.Contains(_type)) return false;

                // check resources
                foreach (Resource resource in _resources)
                {
                    if (slot.Resources.Contains(resource))
                        resourcesMatch[resource] += 1;
                }
            }

            foreach (Resource resource in _resources)
                if (resourcesMatch[resource] == 0) return false;

            return true;
        }

        public void Dispose()
        {
            _resources = null;
            Debug.Log("Dispose MapRouterCheckForBuilding");
        }

        public class Factory : PlaceholderFactory<BuildingObject, MapRouterCheckForBuilding>
        {

        }
    }



}
