﻿using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Map;
using LittleMars.Model;
using LittleMars.Model.Interfaces;
using System;
using System.Collections;
using System.Drawing;
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

        public OperationManager(ProductionManager production, SignalBus signalBus, OperationHelper helper)
        {
            _production = production;
            _signalBus = signalBus;
            _helper = helper;
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

        public void OnBuildingTimetableChange(IBuildingFacade building)
        {
            if (building.StateForPeriod(_period) != building.State())
                TryChangeBuildingState(building, building.StateForPeriod(_period), OperationMode.auto);
        }

        private void OnTryChangeState(TryChangeBuildingStateSignal arg)
        {
            TryChangeBuildingState(arg.BuildingFacade, arg.State, arg.Mode);
        }

        public bool TryChangeBuildingState(IBuildingFacade building, ProductionState state, OperationMode mode)
        {
            Debug.Log($"Try change state for building. To: {state}. Mode: {mode}.");

            if (state == ProductionState.on && CheckForTurnOn(building, mode))
            {
                ChangeBuildingStateTo(building, state, mode);
                return true;
            }                
            else if (state == ProductionState.off && CheckForTurnOff(building, mode))
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
            if (building.StateForPeriod(_period) == ProductionState.off 
                && building.State() == ProductionState.on)
            {
                // turn off
                Debug.Log($"The building sould be turn off for this period by timetable.");
                return false;
            }

            if (building.State() == ProductionState.on)
            {
                Debug.Log($"The building is already turn on.");
                return false;
            }

            return _production.HasResources(building.Needs());
        }

        private bool CheckForTurnOff(IBuildingFacade building, OperationMode mode)
        {
            Debug.Log($"Check for TURN OFF.");
            if (building.State() == ProductionState.off) return false;

            return true;
        }

        private void ChangeBuildingStateTo(IBuildingFacade building, ProductionState state,
            OperationMode mode)
        {
            _production.UpdateProduction(building.Production(), state);
            _production.UpdateNeeds(building.Needs(), state);

            // set new operation mode -> turn on always auto
            if (state == ProductionState.on) mode = OperationMode.auto;            
            
            building.ChangeState(state, mode);

            // raise event is needed
            Debug.Log($"Change state for building to {state}.");
            _signalBus.TryFire(new BuildingStateChangedSignal { BuildingFacade = building });
        }

    }
}
