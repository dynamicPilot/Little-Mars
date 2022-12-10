using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Map;
using LittleMars.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LittleMars.Models.Facades
{
    public class ModelFacade : IModelFacade
    {
        MapManager _mapManager;
        BuildingManager _buildingManager;
        PlacementManager _placementManager;

        public ModelFacade(MapManager mapManager, BuildingManager buildingManager,
            PlacementManager placementManager)
        {
            _mapManager = mapManager;
            _buildingManager = buildingManager;
            _placementManager = placementManager;
        }

        public void StartBuildingPlacement(BuildingObject buildingObject, Indexes indexes, Vector2 position)
        {
            _placementManager.StartPlacement(buildingObject, indexes, position);
        }

        public List<List<MapSlotExtended>> MapSlots()
        {
            return _mapManager.Slots;
        }

        public void TryChangeBuildingState(IBuildingFacade building, ProductionState state)
        {
            _buildingManager.TryChangeBuildingState(building, state);
        }
    }
}

