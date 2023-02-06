using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.Trackers
{

    public class BuildingGoalTracker : GoalTracker, IDisposable
    {
        readonly Goal<BuildingUnit<int>> _goal;
        
        List<IBuildingFacade> _buildings;

        public BuildingGoalTracker(Goal<BuildingUnit<int>> goal, int index, SignalBus signalBus)
            : base (signalBus)
        {
            _goal = goal;
            _isDone = false;
            _buildings = new List<IBuildingFacade>();

            SetSignals(index);
            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        protected override void SetSignals(int index)
        {
            _onUpdateSignal = new GoalUpdatedSignal
            {
                Index = index,
                Values = new float[1] { 0 }
            };

            _isDoneSignal = new GoalIsDoneSignal
            {
                ResultType = _goal.Type,
                Index = index,
                IsFirstDone = _isFirstDone
            };
        }

        private void OnBuildingStateChanged(BuildingStateChangedSignal args)
        {
            var building = args.BuildingFacade;
            var info = building.Info();

            if (_goal.Unit.Type != info.Type || _goal.Unit.Size != info.Size) return;

            if (building.State() == States.on && !_buildings.Contains(building))
            {
                Debug.Log("Add building to goal" + building.Info().Type);
                _buildings.Add(building);                
            }
            else if (building.State() == States.off && _buildings.Contains(building)
                && info.Type == BuildingType.dome)
            {
                // remove only when dome is off
                Debug.Log("Remove building to goal" + building.Info().Type);
                _buildings.Remove(building);
            }
            else return;

            OnGoalUpdated();
            CheckIsDone(_buildings.Count >= _goal.Unit.Amount);
        }

        private void OnRemoveBuilding(RemoveBuildingSignal args)
        {
            var building = args.BuildingFacade;

            if (!_buildings.Contains(building)) return;

            _buildings.Remove(building);
            OnGoalUpdated();
            CheckIsDone(_buildings.Count >= _goal.Unit.Amount);
        }

        protected override void UpdateOnUpdatedSignal()
        {
            _onUpdateSignal.Values[0] = _buildings.Count;
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
            _signalBus.TryUnsubscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }
    }
}
