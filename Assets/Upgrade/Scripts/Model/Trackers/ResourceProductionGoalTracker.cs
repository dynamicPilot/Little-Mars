using LittleMars.Common.LevelGoal;
using LittleMars.Common;
using Zenject;
using LittleMars.Common.Signals;
using System;

namespace LittleMars.Model.Trackers
{
    public class ResourceProductionGoalTracker : GoalTracker, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly Goal<ResourceUnit<float>> _goal;

        GoalUpdatedSignal _onUpdateSignal;

        float _currentProduction = 0f;

        public ResourceProductionGoalTracker(Goal<ResourceUnit<float>> goal, int index, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _goal = goal;

            _isDone = false;
            _currentProduction = 0f;
            _signalBus.Subscribe<TotalProductionChangedSignal>(OnTotalProductionChanged);

            _onUpdateSignal = new GoalUpdatedSignal
            {
                Index = index,
                Values = new float[1] { 0 }
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

        public override void OnGoalUpdated()
        {
            _onUpdateSignal.Values[0] = _currentProduction;
            _signalBus.Fire(_onUpdateSignal);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<TotalProductionChangedSignal>(OnTotalProductionChanged);
        }
    }


    
}
