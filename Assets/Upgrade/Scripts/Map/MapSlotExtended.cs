﻿using LittleMars.Common;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace LittleMars.Map
{
    public class MapSlotExtended  : MapSlot
    {
        IDictionary<Direction, MapSlotExtended> _neighbors = 
            new Dictionary<Direction, MapSlotExtended>();
        public Indexes Indexes { get; private set; } = null;
        public BuildingType HasBuildingOfType { get; private set; } = BuildingType.none;

        public MapSlotExtended(bool isBlocked, List<BuildingType> buildings)
        {
            IsBlocked = isBlocked;
            Buildings = buildings;
            Resources = new List<Resource>();
        }

        public void AddNeighbor(Direction direction, MapSlotExtended neighbor)
        {
            _neighbors.Add(direction, neighbor);
        }

        public MapSlotExtended GetNeighbor(Direction direction)
        {
            MapSlotExtended neighbor = null;
            _neighbors.TryGetValue(direction, out neighbor);
            return neighbor;
        }

        public void AddResource(Resource resource)
        {
            if (resource == Resource.none || resource == Resource.all) return;
            if (!Resources.Contains(resource)) Resources.Add(resource);
        }

        public void AddBuilding(BuildingType building)
        {
            if (building == BuildingType.none || building == BuildingType.all) return;
            if (!Buildings.Contains(building)) Buildings.Add(building);
        }

        public void ChangeBuilding(BuildingType newBuilding)
        {
            if (newBuilding == BuildingType.none || newBuilding == BuildingType.all) return;
            HasBuildingOfType = newBuilding;
        }

        public void SetIndexes(int row, int col)
        {
            Indexes = new Indexes(row, col);
        }

        public class Factory: PlaceholderFactory<MapSlotExtended>
        {

        }

    }
}