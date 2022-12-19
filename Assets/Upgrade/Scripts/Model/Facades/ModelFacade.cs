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
        ConstructionHelper _constructionHelper;

        public ModelFacade(MapManager mapManager, BuildingManager buildingManager,
            PlacementManager placementManager, ConstructionHelper constructionHelper)
        {
            _mapManager = mapManager;
            _buildingManager = buildingManager;
            _placementManager = placementManager;
            _constructionHelper = constructionHelper;
        }

        public void StartBuildingPlacement(BuildingObject buildingObject, Indexes indexes, Vector2 position)
        {
            if (_constructionHelper.CheckResources(buildingObject))
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

