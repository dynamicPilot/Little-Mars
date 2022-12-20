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
        PlacementManager _placementManager;
        ConstructionHelper _constructionHelper;
        BuildingController _controller;

        public ModelFacade(MapManager mapManager, PlacementManager placementManager, 
            ConstructionHelper constructionHelper, BuildingController controller)
        {
            _mapManager = mapManager;
            _placementManager = placementManager;
            _constructionHelper = constructionHelper;
            _controller = controller;
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

        public void StartBuildingControl(IBuildingFacade building)
        {
            _controller.StartController(building);
        }
    }
}
