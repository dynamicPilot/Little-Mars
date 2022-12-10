using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Map;
using LittleMars.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using LittleMars.Model;

namespace LittleMars.Buildings
{
    public class BuildingManager: IInitializable
    {
        List<IBuildingFacade> _buildings;

        readonly BuildingFactory _factory;
        readonly OperationManager _operation;
        readonly SignalBus _signalBus;

        public BuildingManager(BuildingFactory factory, OperationManager operation, SignalBus signalBus)
        {
            _factory = factory;
            _operation = operation;
            _signalBus = signalBus;
            _buildings = new List<IBuildingFacade>();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }

        public void AddBuilding(PlacingBuilding placingBuilding)
        {
            var building = _factory.CreateBuilding(placingBuilding);
            _buildings.Add(building);
            _signalBus.TryFire(new AddBuildingSignal { BuildingFacade = building });

            Debug.Log($"Add building. Total building number is {_buildings.Count}.");
            _operation.TryChangeBuildingState(building, ProductionState.on, OperationMode.manual);            
        }

        public void RemoveBuilding(IBuildingFacade building)
        {

        }

        public void TryChangeBuildingState(IBuildingFacade building, ProductionState state)
        {
            _operation.TryChangeBuildingState(building, state, OperationMode.manual);
        }

        public void OnPeriodChanged(Period period)
        {
            _operation.OnPeriodChanged(period);
            DoBuildingsCheck(null);
        }

        private void OnBuildingStateChanged(BuildingStateChangedSignal args)
        {
            DoBuildingsCheck(args.BuildingFacade);
        }

        private void DoBuildingsCheck(IBuildingFacade building)
        {
            for (int i = 0; i < _buildings.Count; i++)
            {
                if (_buildings[i] == building) continue;

                _operation.TryChangeBuildingState(_buildings[i], ProductionState.on, OperationMode.auto);
            }
        }

        

    }
}
