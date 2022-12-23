using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class BuildingGoalTracker : GoalTracker
    {
        readonly SignalBus _signalBus;
        readonly Goal<BuildingUnit<int>> _goal;
        
        List<IBuildingFacade> _buildings;

        public BuildingGoalTracker(Goal<BuildingUnit<int>> goal, SignalBus signalBus)
        {
            _goal = goal;
            _signalBus = signalBus;

            _isDone = false;
            _buildings = new List<IBuildingFacade>();
            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        private void OnBuildingStateChanged(BuildingStateChangedSignal args)
        {
            var building = args.BuildingFacade;
            var info = building.Info();

            if (_goal.Unit.Type != info.Type || _goal.Unit.Size != info.Size) return;

            if (building.State() == ProductionState.on && !_buildings.Contains(building))
            {
                _buildings.Add(building);
            }
            else if (building.State() == ProductionState.off && _buildings.Contains(building)
                && info.Type == BuildingType.dome)
            {
                // remove only when dome is off
                _buildings.Remove(building);
            }
            else return;

            CheckIsDone(_buildings.Count >= _goal.Unit.Amount);
        }

        private void OnRemoveBuilding(RemoveBuildingSignal args)
        {
            var building = args.BuildingFacade;

            if (!_buildings.Contains(building)) return;

            _buildings.Remove(building);
            CheckIsDone(_buildings.Count >= _goal.Unit.Amount);
        }
    }
}
