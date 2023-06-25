using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Map;
using LittleMars.Models;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using LittleMars.Model;

namespace LittleMars.Buildings
{
    public class BuildingManager: IInitializable
    {
        List<IBuildingFacade> _buildings;

        readonly BuildingProvider _provider;
        readonly OperationManager _operation;
        readonly SignalBus _signalBus;

        public BuildingManager(BuildingProvider provider, OperationManager operation, SignalBus signalBus)
        {
            _provider = provider;
            _operation = operation;
            _signalBus = signalBus;
            _buildings = new List<IBuildingFacade>();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanged);
        }

        public void AddBuilding(PlacingBuilding placingBuilding)
        {
            var building = _provider.GetBuilding(placingBuilding);
            _buildings.Add(building);
            _signalBus?.TryFire(new AddBuildingSignal { BuildingFacade = building });

            //Debug.Log($"Add building. Total building number is {_buildings.Count}.");
            _operation.TryChangeBuildingState(building, Common.States.on, OperationMode.manual);            
        }

        public void RemoveBuilding(IBuildingFacade building)
        {
            //Debug.Log($"Remove building with {building.Info().Type}");
            _operation.TryChangeBuildingState(building, Common.States.off, OperationMode.manual);
            _provider.FreeBuilding(building);
            _buildings.Remove(building);
            _signalBus?.TryFire(new RemoveBuildingSignal { BuildingFacade = building });
        }

        public void TryChangeBuildingState(IBuildingFacade building, Common.States state, OperationMode mode)
        {
            _operation.TryChangeBuildingState(building, state, mode);
        }

        public void OnPeriodChanged(PeriodChangeSignal arg)
        {
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

                _operation.TryChangeBuildingState(_buildings[i], Common.States.on, OperationMode.auto);
            }
        }

        

    }
}
