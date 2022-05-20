using System.Collections.Generic;
using UnityEngine;

public class MapChecking : MonoBehaviour
{
    //public void CheckConnectionTypeForStartProduction(MapSlot startSlot)
    //{

    //}

    public KeyValuePair<List<MapSlot>, List<int>> CheckForBuildingWays(BuildingWay[] ways, MapSlot startSlot, Inventory.B_TYPE type, Inventory.R_TYPE resource, int rotationIndex = 0)
    {
        // return: map slot, image part index

        if (!startSlot.IsEmpty || startSlot.Blocked)
        {
            return new KeyValuePair<List<MapSlot>, List<int>>(null, null);
        }

        int wayIndex = 0;
        KeyValuePair<List<MapSlot>, List<int>> result = new KeyValuePair<List<MapSlot>, List<int>>(null, null);

        while (wayIndex < ways.Length)
        {
            result = CheckForBuildingWay(ways[wayIndex].Way, startSlot, type, resource, rotationIndex);

            if (result.Key == null || result.Value == null)
            {
                wayIndex++;
            }
            else
            {
                break;
            }
        }

        // add way index to the and of the indexes array
        if (result.Value != null) 
            result.Value.Add(wayIndex);

        return result;
    }

    public KeyValuePair<List<MapSlot>, List<int>> CheckForBuildingWay(BuildingWayStep[] way, MapSlot startSlot, Inventory.B_TYPE type, Inventory.R_TYPE resource, int rotationIndex = 0)
    {
        // return: map slot, image part index

        //if (!startSlot.IsEmpty || startSlot.Blocked)
        //{
        //    return new KeyValuePair<List<MapSlot>, List<int>>(null, null);
        //}

        
        List<MapSlot> slotsInWay = new List<MapSlot>();
        List<int> buildingImageIndexes = new List<int>();

        while (rotationIndex < 4)
        {
            KeyValuePair<List<MapSlot>, List<int>> result = CheckForOneWayForBuildings(way, startSlot, type, resource, rotationIndex);
            slotsInWay = result.Key;
            buildingImageIndexes = result.Value;

            if (slotsInWay == null || buildingImageIndexes == null)
            {
                rotationIndex++;
            }
            else
            {
                break;
            }
        }

        if (buildingImageIndexes != null)
            buildingImageIndexes.Add(rotationIndex);

        return new KeyValuePair<List<MapSlot>, List<int>>(slotsInWay, buildingImageIndexes);

    }

    public KeyValuePair<List<MapSlot>, int> CheckForFieldWay(BuildingWayStep[] way, MapSlot startSlot)
    {
        if (startSlot.Blocked)
        {
            return new KeyValuePair<List<MapSlot>, int>(null, -1);
        }
        // return: map slot, rotation index
        int rotationIndex = 0;

        List<MapSlot> slotsInWay = new List<MapSlot>();

        while (rotationIndex < 4)
        {
            slotsInWay = CheckForOneWay(way, startSlot, rotationIndex);

            if (slotsInWay == null)
            {
                //Debug.Log("MapChecking: change rotation");
                rotationIndex++;
            }
            else
            {
                break;
            }
        }

        if (slotsInWay == null)
        {
            return new KeyValuePair<List<MapSlot>, int>(null, -1);
        }

        return new KeyValuePair<List<MapSlot>, int>(slotsInWay, rotationIndex);

    }

    private KeyValuePair<List<MapSlot>, List<int>> CheckForOneWayForBuildings(BuildingWayStep[] way, MapSlot startSlot, Inventory.B_TYPE type, Inventory.R_TYPE resource, int rotationIndex)
    {
        List<MapSlot> slotsInWay = CheckForOneWay(way, startSlot, rotationIndex);
        List<int> buildingImageIndexes = new List<int>();
        int typeMatchCounter = 0;
        int resourceMatchCounter = 0;

        if (slotsInWay == null)
        {
            return new KeyValuePair<List<MapSlot>, List<int>>(null, null);
        }

        for (int i = 0; i < slotsInWay.Count; i++)
        {
            buildingImageIndexes.Add(i);

            if (slotsInWay[i].AvailableTypes.Contains(type))
            {
                typeMatchCounter++;
            }

            if (slotsInWay[i].AvailableResources.Contains(resource))
            {
                resourceMatchCounter++;
            }
        }

        if (typeMatchCounter < slotsInWay.Count)
        {
            // no suitable map slot according to the type of the building
            //Debug.Log("MapChecker: type mismatch " + typeMatchCounter);
            return new KeyValuePair<List<MapSlot>, List<int>>(null, null);
        }

        if (resourceMatchCounter == 0 && (resource != Inventory.R_TYPE.none && resource != Inventory.R_TYPE.all))
        {
            //Debug.Log("MapChecker: resources mismatch " + resourceMatchCounter + " " +resource);
            return new KeyValuePair<List<MapSlot>, List<int>>(null, null);
        }

        return new KeyValuePair<List<MapSlot>, List<int>>(slotsInWay, buildingImageIndexes);
    }

    private List<MapSlot> CheckForOneWay(BuildingWayStep[] way, MapSlot startSlot, int rotationIndex)
    {
        
        List<MapSlot> slotsInWay = new List<MapSlot>();
       
        // add start slot
        slotsInWay.Add(startSlot);

        if (way[0].IsSingleSlotBuilding)
        {
            return slotsInWay;
        }

        MapSlot prevSlot = slotsInWay[slotsInWay.Count - 1];
        // check way
        for (int i = 0; i < way.Length; i++)
        {
            MapSlot nextSlot = prevSlot.Neighbors[way[i].MakeRotation(rotationIndex)];
            bool isBack = way[i].IsBackStep;

            if (isBack)
            {
                // back step --- no checks and adds
                prevSlot = nextSlot;
                continue;
            }

            // check nextSlot before add it
            if (nextSlot == null)
            {
                return null;
            }
            else if (!nextSlot.IsEmpty)
            {
                return null;
            }
            else if (nextSlot.Blocked)
            {
                return null;
            }

            slotsInWay.Add(nextSlot);
            prevSlot = slotsInWay[slotsInWay.Count - 1];
        }

        return slotsInWay;
    }
}
