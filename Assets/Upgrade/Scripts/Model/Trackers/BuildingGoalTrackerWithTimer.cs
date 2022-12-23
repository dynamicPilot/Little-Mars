using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class BuildingGoalTrackerWithTimer : GoalTracker
    {
        readonly GoalWithTime<BuildingUnit<int>> _goal;
        readonly SignalBus _signalBus;

        List<IBuildingFacade> _buildings;

        float _timer = 0f;
        bool _hasEnoughBuildings = false;
        public BuildingGoalTrackerWithTimer(GoalWithTime<BuildingUnit<int>> goal, SignalBus signalBus)
        {
            _goal = goal;
            _signalBus = signalBus;

            _buildings = new List<IBuildingFacade>();
            _isDone = false;
            _hasEnoughBuildings = false;

            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
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
            else if (building.State() == ProductionState.off && _buildings.Contains(building))
            {
                _buildings.Remove(building);
            }
            else return;

            CheckBuildingCount();
        }


        private void OnHourlySignal()
        {
            _timer += 1f;

            CheckIsDone(_timer >= _goal.Time);
            if (_isDone) StopTimer();
        }

        private void StartTimer()
        {
            _timer = 0f;
            _signalBus.Subscribe<HourlySignal>(OnHourlySignal);
        }

        private void StopTimer()
        {
            _signalBus.Unsubscribe<HourlySignal>(OnHourlySignal);
        }

        private void CheckBuildingCount()
        {
            var hasEnoughBuildings = (_buildings.Count >= _goal.Unit.Amount);

            // no changes
            if (hasEnoughBuildings == _hasEnoughBuildings) return;

            _hasEnoughBuildings = hasEnoughBuildings;
            if (_hasEnoughBuildings)
            {
                StartTimer();
            }
            else
            {
                StopTimer();
            }
        }
    }
}
