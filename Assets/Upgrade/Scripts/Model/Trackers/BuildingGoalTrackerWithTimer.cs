using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using LittleMars.Model.Interfaces;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class BuildingGoalTrackerWithTimer : IGoalTracker
    {
        readonly GoalWithTime<BuildingUnit<int>> _goal;
        readonly SignalBus _signalBus;

        Dictionary<IBuildingFacade, float> _buildingTimers;
        bool _isDone = false;

        public BuildingGoalTrackerWithTimer(GoalWithTime<BuildingUnit<int>> goal, SignalBus signalBus)
        {
            _goal = goal;
            _signalBus = signalBus;
        }

        public bool Check()
        {
            return _isDone;
        }

        private void OnBuildingStateChanged(BuildingStateChangedSignal args)
        {
            var info = args.BuildingFacade.Info();

            if (_goal.Unit.Type != info.Type || _goal.Unit.Size != info.Size) return;
            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
            _signalBus.Subscribe<HourlySignal>(OnHourlySignal);
        }

        private void OnHourlySignal()
        {

        }

        private void CheckIsDone()
        {

        }
    }
}
