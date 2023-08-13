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

namespace LittleMars.Model
{
    public class PlacementManager : IPlacement
    {
        readonly MapManager _mapManager;
        readonly BuildingManager _buildingManager;
        readonly ViewSlotManager _viewSlotManager;
        readonly MapRouter _router;
        readonly ConstructionHelper _constructionHelper;
        readonly MapRouterCheckForBuilding.Factory _checkFactory;
        readonly PlacingBuilding.Factory _factory;

        readonly SignalBus _signalBus;
        
        PlacingBuilding _placingBuilding;
        MapRouterCheckForBuilding _check;
        BuildingObject _buildingObject;

        public PlacementManager(MapManager mapManager, BuildingManager buildingManager,
            ViewSlotManager viewSlotManager, MapRouter router, SignalBus signalBus,
            MapRouterCheckForBuilding.Factory checkFactory, PlacingBuilding.Factory factory,
            ConstructionHelper constructionHelper)
        {
            _mapManager = mapManager;
            _buildingManager = buildingManager;
            _viewSlotManager = viewSlotManager;
            _router = router;
            _checkFactory = checkFactory;
            _factory = factory;
            _signalBus = signalBus;

            _placingBuilding = null;
            _constructionHelper = constructionHelper;
        }

        public void StartPlacement(BuildingObject buildingObject, 
            Indexes indexes, Vector2 position)
        {
            NewCheck(buildingObject);
            _buildingObject = buildingObject;

            var path = buildingObject.Construction.BuildingPath;
            var slot = _mapManager.Slots[indexes.Row][indexes.Column];
            var indexesList = new List<Indexes>();
            int rotationCount = 0;

            //Debug.Log($"Start placement: {buildingObject.Info.Type}. Path type is {path.Type}.");
            if (_router.TryBuildRouteFrom(path, slot, _check,
                    out List<MapSlotExtended> route, ref rotationCount))
            {
                //Debug.Log("Has Route");
                foreach (MapSlotExtended part in route) indexesList.Add(part.Indexes);

                _placingBuilding = _factory.Create(buildingObject.Info.Type,
                    buildingObject.Info.Size, path, position);

                _placingBuilding.RotationCount = rotationCount;
                _placingBuilding.SlotIndexes = indexesList;

                _viewSlotManager.PlacingBuildingInSlots(indexesList);
                _signalBus.Fire<StartBuildingPlacementSignal>();
            }
            else
            {
                _signalBus.Fire<NeedRouteErrorNotSignal>();
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
            _buildingManager.AddBuilding(_placingBuilding);
            //Debug.Log("Rotation index: " + _placingBuilding.RotationCount);
            _constructionHelper.Accept(_buildingObject);
            EndPlacement();
        }

        public void Remove()
        {
            _viewSlotManager.RemoveBuildingFromSlots(_placingBuilding.SlotIndexes);
            EndPlacement();
        }

        public void OnBeginDrag()
        {
            if (_placingBuilding == null) return;
            Remove();
        }

        void EndPlacement()
        {
            _placingBuilding = null;
            _buildingObject = null;
            _check.Dispose();
        }

        void NewCheck(BuildingObject buildingObject)
        {
            if (_check != null) _check.Dispose();
            _check = _checkFactory.Create(buildingObject);
        }
    }
}
