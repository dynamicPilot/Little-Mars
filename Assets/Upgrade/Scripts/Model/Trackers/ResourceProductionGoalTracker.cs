using LittleMars.Common.LevelGoal;
using LittleMars.Common;
using Zenject;
using LittleMars.Common.Signals;
using System;

namespace LittleMars.Model.Trackers
{
    public class ResourceProductionGoalTracker : GoalTracker, IDisposable
    {
        readonly Goal<ResourceUnit<float>> _goal;

        float _currentProduction = 0f;

        public ResourceProductionGoalTracker(Goal<ResourceUnit<float>> goal, int index, SignalBus signalBus)
            : base (signalBus)
        {
            _goal = goal;

            _isDone = false;
            _currentProduction = 0f;
            _signalBus.Subscribe<TotalProductionChangedSignal>(OnTotalProductionChanged);

            _onUpdateSignal = new GoalUpdatedSignal
            {
                Index = index,
                Values = new float[1] { 0 },
            };

            _isDoneSignal = new GoalIsDoneSignal
            {
                ResultType = _goal.Type,
                Index = index,
                IsFirstDone = _isFirstDone
            };
        }

        private void OnTotalProductionChanged(TotalProductionChangedSignal args)
        {
            var current = args.TotalProdution[_goal.Unit.Type];
            if (current == _currentProduction) return;

            _currentProduction = current;
            OnGoalUpdated();
            CheckIsDone(_currentProduction == _goal.Unit.Amount);
        }

        protected override void UpdateOnUpdatedSignal()
        {
            _onUpdateSignal.Values[0] = _currentProduction;
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<TotalProductionChangedSignal>(OnTotalProductionChanged);
        }
    }
}
