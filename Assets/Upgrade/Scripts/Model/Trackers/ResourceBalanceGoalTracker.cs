using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using LittleMars.Common;
using System;
using Zenject;
using UnityEngine;

namespace LittleMars.Model.Trackers
{
    public class ResourceBalanceGoalTracker : GoalTracker, IDisposable
    {
        readonly Goal<ResourceUnit<float>> _goal;

        float _currentBalance = 0f;

        public ResourceBalanceGoalTracker(Goal<ResourceUnit<float>> goal, int index, SignalBus signalBus)
            : base (signalBus)
        {
            _goal = goal;

            _isDone = false;

            _currentBalance = 0f; // add start check for goal before first update
            //Debug.Log("Create balance goal slot. Goal is "+ _goal.Unit.Amount);
            SetSignals(index);      
            _signalBus.Subscribe<ResourcesBalanceUpdatedSignal>(OnResourceBalanceUpdated);
        }

        protected override void SetSignals(int index)
        {
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

        private void OnResourceBalanceUpdated(ResourcesBalanceUpdatedSignal args)
        {
            var current = args.ResourcesBalance[_goal.Unit.Type];
            if (current == _currentBalance) return;

            //Debug.Log("OnResourceBalanceUpdate: " + current + " current balance " + _currentBalance);
            _currentBalance = current;
            OnGoalUpdated();
            CheckIsDone(_currentBalance >= _goal.Unit.Amount);
        }

        protected override void UpdateOnUpdatedSignal()
        {
            _onUpdateSignal.Values[0] = _currentBalance;
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ResourcesBalanceUpdatedSignal>(OnResourceBalanceUpdated);
        }
    }
}
