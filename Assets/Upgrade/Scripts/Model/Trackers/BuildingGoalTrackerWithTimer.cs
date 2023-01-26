using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class BuildingGoalTrackerWithTimer : GoalTracker, IDisposable
    {
        readonly GoalWithTime<BuildingUnit<int>> _goal;
        readonly SignalBus _signalBus;

        List<IBuildingFacade> _buildings;

        GoalUpdatedSignal _onUpdateSignal;
        GoalIsDoneSignal _isDoneSignal;

        float _timer = 0f;
        bool _hasEnoughBuildings = false;

        public BuildingGoalTrackerWithTimer(GoalWithTime<BuildingUnit<int>> goal, int index, SignalBus signalBus)
        {
            _goal = goal;
            _signalBus = signalBus;

            _buildings = new List<IBuildingFacade>();
            _isDone = false;
            _hasEnoughBuildings = false;

            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);

            _onUpdateSignal = new GoalUpdatedSignal
            {
                Index = index,
                Values = new float[2] { 0f, 0f }
            };

            _isDoneSignal = new GoalIsDoneSignal
            {
                ResultType = _goal.Type,
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
                _buildings.Add(building);
            }
            else if (building.State() == States.off && _buildings.Contains(building))
            {
                _buildings.Remove(building);
            }
            else return;

            OnGoalUpdated();
            CheckBuildingCount();
        }


        private void OnHourlySignal()
        {
            _timer += 1f;

            OnGoalUpdated();
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

        public override void OnGoalUpdated()
        {
            _onUpdateSignal.Values[0] = _buildings.Count;
            _onUpdateSignal.Values[1] = _timer;
            _signalBus.Fire(_onUpdateSignal);
        }

        public override void OnGoalIsDone()
        {
            _isDoneSignal.IsFirstDone = _isFirstDone;
            _signalBus.Fire(_isDoneSignal);
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

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }
    }
}
