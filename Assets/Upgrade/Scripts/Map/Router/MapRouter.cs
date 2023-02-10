using LittleMars.Buildings.Parts;
using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace LittleMars.Map.Routers
{
    public class MapRouter
    {
        public bool TryBuildRouteFromRandom(Path path, List<List<MapSlotExtended>> slots, 
            IMapRouterCheck check,
            out List<MapSlotExtended> route, out int rotationCounter)
        {
            MapSlotExtended startSlot = null;
            rotationCounter = 0;

            while (startSlot == null || startSlot.IsBlocked || startSlot.HasBuildingOfType!= BuildingType.none)
            {
                startSlot = slots[Random.Range(0, slots.Count)][Random.Range(0, slots[0].Count)];
            }
            
            return TryBuildRouteFrom(path, startSlot, check, out route, ref rotationCounter);
        }

        public bool TryBuildRouteFrom(Path path, MapSlotExtended startSlot, 
            IMapRouterCheck check,
            out List<MapSlotExtended> route, ref int rotationCounter)
        {
            route = new List<MapSlotExtended>();
            rotationCounter %= 4;

            var initialCounter = rotationCounter;
            bool hasRoute = false;

            while (rotationCounter < 4 && !hasRoute)
            {
                hasRoute = TryBuildSingleRoute(path, startSlot, rotationCounter, out route);
                if (hasRoute) hasRoute = check.Check(route);
                if (!hasRoute)
                {
                    rotationCounter = (rotationCounter + 1) % 4;
                    if (rotationCounter == initialCounter) break;
                }
            }
            return hasRoute;
        }

        private bool TryBuildSingleRoute(Path path, MapSlotExtended start,
            int rotationCounter, out List<MapSlotExtended> route)
        {
            //Debug.Log("Try build signle route");
            route = new List<MapSlotExtended>();

            if (start.IsBlocked) return false;

            route.Add(start);

            // single slot building
            if (path.Type == PathType.single) return true;

            var current = start;
            var next = start;
            Direction direction;

            for (int i = 0; i < path.Units.Length; i++)
            {
                //Debug.Log($"Path unit {i}, rotation counter {rotationCounter}");
                current = next;
                direction = path.Units[i].RotateBySeveralTimes(rotationCounter);
                next = current.GetNeighbor(direction);

                //Debug.Log($"Current: row {current.Indexes.Row} col {current.Indexes.Column}.");
                //Debug.Log("Direction " + direction);
                if (next != null) Debug.Log($"Next: row {next.Indexes.Row} col {next.Indexes.Column}.");
                if (path.Units[i].IsBackStep)
                {
                    //Debug.Log("-- is back step");
                    continue;
                }

                if (next == null)
                {
                    //Debug.Log("-- next is null");
                    return false;
                }
                else if (next.HasBuildingOfType != BuildingType.none)
                {
                    //Debug.Log("-- slot is not empty");
                    return false;
                }
                else if (next.IsBlocked)
                {
                    //Debug.Log("-- slot is blocked");
                    return false;
                }
                else
                {
                    //Debug.Log("-- add to road");
                    route.Add(next);
                }
            }

            return true;

        }
    }
}
