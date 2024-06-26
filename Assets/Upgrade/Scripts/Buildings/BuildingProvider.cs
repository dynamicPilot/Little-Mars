﻿using LittleMars.Common.Interfaces;
using LittleMars.Model;

namespace LittleMars.Buildings
{

    public class BuildingProvider
    {
        readonly BuildingFacade.Factory _factory;
        readonly BuildingStorage _storage;

        public BuildingProvider(BuildingFacade.Factory factory, BuildingStorage storage)
        {
            _factory = factory;
            _storage = storage;
        }

        public IBuildingFacade GetBuilding(PlacingBuilding placingBuilding)
        {
            var type = placingBuilding.Type;
            var size = placingBuilding.Size;

            if (!_storage.GetFromFree(type, size, out IBuildingFacade building))
            {
                building = _factory.Create(placingBuilding.Type, placingBuilding.Size);
            }

            var buildingTransform = ((BuildingFacade)building).gameObject.transform;
            buildingTransform.localPosition = placingBuilding.Position * 7f/2f;
            building.Rotate(90f * placingBuilding.RotationCount);
            building.OnStart();
            building.SetMapSlotIndexes(placingBuilding.SlotIndexes);

            return building;
        }

        public void FreeBuilding(IBuildingFacade building)
        {
            building.OnRemove();
            _storage.AddToFree(building);
        }
    }
}
