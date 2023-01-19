using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using LittleMars.Common;
using System;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class ResourceBalanceGoalTracker : GoalTracker, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly Goal<ResourceUnit<float>> _goal;

        GoalUpdatedSignal _onUpdateSignal;

        float _currentBalance = 0f;

        public ResourceBalanceGoalTracker(Goal<ResourceUnit<float>> goal, int index, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _goal = goal;

            _isDone = false;
            _currentBalance = 0f;
            _signalBus.Subscribe<ResourcesBalanceUpdatedSignal>(OnResourceBalanceUpdated);

            _onUpdateSignal = new GoalUpdatedSignal
            {
                Index = index,
                Values = new float[1] { 0 }
            };
        }

        private void OnResourceBalanceUpdated(ResourcesBalanceUpdatedSignal args)
        {
            var current = args.ResourcesBalance[_goal.Unit.Type];
            if (current == _currentBalance) return;

            _currentBalance = current;
            OnGoalUpdated();
            CheckIsDone(_currentBalance == _goal.Unit.Amount);
        }

        public override void OnGoalUpdated()
        {
            _onUpdateSignal.Values[0] = _currentBalance;
            _signalBus.Fire(_onUpdateSignal);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ResourcesBalanceUpdatedSignal>(OnResourceBalanceUpdated);
        }
    }
}
