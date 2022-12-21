using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Map;
using LittleMars.Map.Routers;
using LittleMars.Slots;
using LittleMars.UI;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Path = LittleMars.Common.Path;

namespace LittleMars.Model
{
    public class PlacementManager : IPlacement
    {
        readonly MapManager _mapManager;
        readonly BuildingManager _buildingManager;
        readonly ViewSlotManager _viewSlotManager;
        readonly MapRouter _router;

        readonly MapRouterCheckForBuilding.Factory _checkFactory;
        readonly PlacingBuilding.Factory _factory;

        readonly SignalBus _signalBus;
        
        PlacingBuilding _placingBuilding;
        MapRouterCheckForBuilding _check;

        public PlacementManager(MapManager mapManager, BuildingManager buildingManager,
            ViewSlotManager viewSlotManager, MapRouter router, SignalBus signalBus,
            MapRouterCheckForBuilding.Factory checkFactory, PlacingBuilding.Factory factory)
        {
            _mapManager = mapManager;
            _buildingManager = buildingManager;
            _viewSlotManager = viewSlotManager;
            _router = router;
            _checkFactory = checkFactory;
            _factory = factory;
            _signalBus = signalBus;
        }

        public void StartPlacement(BuildingObject buildingObject, 
            Indexes indexes, Vector2 position)
        {
            NewCheck(buildingObject);
            var path = buildingObject.Construction.BuildingPath;
            var slot = _mapManager.Slots[indexes.Row][indexes.Column];
            var indexesList = new List<Indexes>();
            int rotationCount = 0;

            Debug.Log($"Start placement: {buildingObject.Info.Type}. Path type is {path.Type}.");
            if (_router.TryBuildRouteFrom(path, slot, _check,
                    out List<MapSlotExtended> route, ref rotationCount))
            {
                foreach (MapSlotExtended part in route) indexesList.Add(part.Indexes);

                _placingBuilding = _factory.Create(buildingObject.Info.Type,
                    buildingObject.Info.Size, path, position);

                _placingBuilding.RotationCount = rotationCount;
                _placingBuilding.SlotIndexes = indexesList;

                _viewSlotManager.PlacingBuildingInSlots(indexesList);
                _signalBus.Fire<StartBuildingPlacementSignal>();
            }
        }


        public void Rotate()
        {
            if (_placingBuilding == null) return;
            
            var path = _placingBuilding.Path;
            var startIndexes = _placingBuilding.StartSlotIndexes();           
            var slot = _mapManager.Slots[startIndexes.Row][startIndexes.Column];
            var rotationCount = _placingBuilding.RotationCount + 1;
            var indexesList = new List<Indexes>();

            if (_router.TryBuildRouteFrom(path, slot, _check,
                    out List<MapSlotExtended> route, ref rotationCount))
            {
                foreach (MapSlotExtended part in route) indexesList.Add(part.Indexes);

                _placingBuilding.RotationCount = rotationCount;
                
                _viewSlotManager.RemoveBuildingFromSlots(_placingBuilding.SlotIndexes.GetRange(1, _placingBuilding.SlotIndexes.Count - 1));

                _placingBuilding.SlotIndexes = indexesList;
                _viewSlotManager.PlacingBuildingInSlots(indexesList);
            }
        }

        public void Accept()
        {
            //_viewSlotManager.AddBuildingToSlots(_placingBuilding.SlotIndexes, _placingBuilding.Type);
            // map manager -> add building to slots
            //_mapManager.AddBuildingToSlots(_placingBuilding.SlotIndexes, _placingBuilding.Type);
            // building manager -> add building
            _buildingManager.AddBuilding(_placingBuilding);
            EndPlacement();
        }

        public void Remove()
        {
            _viewSlotManager.RemoveBuildingFromSlots(_placingBuilding.SlotIndexes);
            EndPlacement();
        }

        private void EndPlacement()
        {
            _placingBuilding = null;
            _check.Dispose();
        }

        private void NewCheck(BuildingObject buildingObject)
        {
            if (_check != null) _check.Dispose();
            _check = _checkFactory.Create(buildingObject);
        }
    }
}
