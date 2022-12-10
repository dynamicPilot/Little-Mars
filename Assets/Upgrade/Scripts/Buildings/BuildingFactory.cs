using LittleMars.Common;
using LittleMars.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using LittleMars.Buildings.Parts;
using LittleMars.Common.Interfaces;
using LittleMars.Model;

namespace LittleMars.Buildings
{

    public class BuildingFactory
    {
        BuildingFacade.Factory _factory;

        public BuildingFactory(BuildingFacade.Factory factory)
        {
            _factory = factory;
        }

        public IBuildingFacade CreateBuilding(PlacingBuilding placingBuilding)
        {
            var building = _factory.Create(placingBuilding.Type, placingBuilding.Size);
            building.gameObject.transform.localPosition = placingBuilding.Position;
            building.gameObject.transform.Rotate(0f, 90f * placingBuilding.RotationCount, 0f);

            return building;
        }
    }
}
