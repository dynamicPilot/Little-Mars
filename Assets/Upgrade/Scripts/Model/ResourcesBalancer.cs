using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LittleMars.Models
{
    public class ResourcesBalancer : IInitializable, IDisposable
    {
        Dictionary<Priority, List<IBuildingFacade>> _buildingsByPriority = 
            new Dictionary<Priority, List<IBuildingFacade>>();

        SignalBus _signalBus;

        public ResourcesBalancer(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.TryUnsubscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        private void OnAddBuilding(AddBuildingSignal arg)
        {
            var building = arg.BuildingFacade;
            var priority = building.Priority();

            if (!_buildingsByPriority.ContainsKey(priority))
                _buildingsByPriority[priority] = new List<IBuildingFacade>();

            _buildingsByPriority[priority].Add(building);
            Debug.Log("Add building " + building.Info().Type + " for priority " + building.Priority());
        }

        private void OnRemoveBuilding(RemoveBuildingSignal arg)
        {
            var building = arg.BuildingFacade;
            var priority = building.Priority();
            _buildingsByPriority[priority].Remove(building);
        }

        public void HelpBalanceResource(Resource resource)
        {
            foreach (Priority key in _buildingsByPriority.Keys)
            {
                foreach(IBuildingFacade building in _buildingsByPriority[key])
                {
                    if (building.State() == States.off) continue;

                    if (building.HasNeedForThisResource(resource))
                    {
                        Debug.Log("Try change state for " + building.Info().Type);
                        _signalBus.Fire(new TryChangeBuildingStateSignal
                        {
                            BuildingFacade = building,
                            State = States.off,
                            Mode = OperationMode.forcedAuto
                        });
                        //_operation.TryChangeBuildingState(building, ProductionState.off, OperationMode.forcedAuto);
                        return;
                    }                       
                }
            }
        }
    }
}
