using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Model;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Models
{
    public class OperationManager : IDisposable, IInitializable
    {
        Period _period;
        ProductionManager _production;
        SignalBus _signalBus;
        OperationHelper _helper;

        public OperationManager(ProductionManager production, SignalBus signalBus, 
            OperationHelper helper)
        {
            _production = production;
            _signalBus = signalBus;
            _helper = helper;
            _period = Period.day;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanged);
            _signalBus.Subscribe<TryChangeBuildingStateSignal>(OnTryChangeState);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PeriodChangeSignal>(OnPeriodChanged);
            _signalBus.TryUnsubscribe<TryChangeBuildingStateSignal>(OnTryChangeState);
        }

        private void OnPeriodChanged(PeriodChangeSignal arg)
        {
            var period = arg.Period;
            if (_period == period) return;
            _period = period;
        }

        public void OnBuildingTimetableChanged(IBuildingFacade building)
        {
            Debug.Log("OnBuildingTimetableChanged. Need change state? Period " + _period + " building state for period " + building.StateForPeriod(_period));
            if (building.StateForPeriod(_period) != building.State())
                TryChangeBuildingState(building, building.StateForPeriod(_period), OperationMode.auto);
        }

        private void OnTryChangeState(TryChangeBuildingStateSignal arg)
        {
            TryChangeBuildingState(arg.BuildingFacade, arg.State, arg.Mode);
        }

        public bool TryChangeBuildingState(IBuildingFacade building, States state, OperationMode mode)
        {
            Debug.Log($"Try change state for building {building.Info().Type}. To: {state}. Mode: {mode}.");

            if (state == States.on && CheckForTurnOn(building, mode))
            {
                ChangeBuildingStateTo(building, state, mode);
                return true;
            }                
            else if (state == States.off && CheckForTurnOff(building, mode))
            {
                ChangeBuildingStateTo(building, state, mode);
                return true;
            }

            return false;
        }

        private bool CheckForTurnOn(IBuildingFacade building, OperationMode mode)
        {
            Debug.Log($"Check for TURN ON.");

            // if the building was turned off by user -> not turn is on by auto
            if (building.OperationMode() == OperationMode.manual
                && mode != OperationMode.manual)
            {
                Debug.Log($"The building was turned off by user -> not turn is on by auto");
                return false;
            }

            // if building has all connections to other buildings to be on
            if (!_helper.HasAllConnections(building))
            {
                Debug.Log($"The building does not have all connections.");
                return false;
            }

            // if building sould be turn off for this period by timetable
            if (building.StateForPeriod(_period) == States.off)
            {
                if (building.State() == States.on) TryChangeBuildingState(building, States.off, OperationMode.auto);
                Debug.Log($"The building sould be turn off for this period by timetable.");
                return false;
            }

            if (building.State() == States.on)
            {
                Debug.Log($"The building is already turn on.");
                return false;
            }

            return _production.HasResourcesForNeeds(building.Needs());
        }

        private bool CheckForTurnOff(IBuildingFacade building, OperationMode mode)
        {
            Debug.Log($"Check for TURN OFF.");
            if (building.State() == States.off) return false;

            return true;
        }

        private void ChangeBuildingStateTo(IBuildingFacade building, States state,
            OperationMode mode)
        {
            _production.UpdateProduction(building.Production(), state);
            _production.UpdateNeeds(building.Needs(), state);

            // set new operation mode -> turn on always auto
            if (state == States.on) mode = OperationMode.auto;            
            
            building.ChangeState(state, mode);

            // raise event is needed
            Debug.Log($"Change state for building {building.Info().Type} to {state}.");
            _signalBus.TryFire(new BuildingStateChangedSignal { BuildingFacade = building });
        }

    }
}
