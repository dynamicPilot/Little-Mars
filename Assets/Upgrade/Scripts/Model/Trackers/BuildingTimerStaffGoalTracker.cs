using LittleMars.Common;
using LittleMars.Common.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.Trackers
{
    /// <summary>
    /// Fake goal tracker for the map building timers.
    /// </summary>
    public class BuildingTimerStaffGoalTracker : GoalTracker, IDisposable
    {
        public BuildingTimerStaffGoalTracker(int index, SignalBus signalBus) : base(signalBus)
        {
            SetSignals(index);
            _signalBus.Subscribe<BuildingTimerIsOverSignal>(OnBuildingTimerIsOver);
        }

        protected override void SetSignals(int index)
        {
            _onUpdateSignal = new GoalUpdatedSignal
            {
                Index = index,
                Values = new float[2] { 0, 0 }
            };

            _isDoneSignal = new GoalIsDoneSignal
            {
                ResultType = ResultType.loseStaff,
                Index = index,
                IsFirstDone = _isFirstDone
            };
        }

        private void OnBuildingTimerIsOver(BuildingTimerIsOverSignal args)
        {
            _onUpdateSignal.Values[0] = (int)args.Type;
            _onUpdateSignal.Values[1] = (int)args.Size;
            Debug.Log($"Building timer is over tracker updated to {_onUpdateSignal.Values[0]} and {_onUpdateSignal.Values[1]}.");
            CheckIsDone(true);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<BuildingTimerIsOverSignal>(OnBuildingTimerIsOver);
        }
    }
}
