﻿using LittleMars.Common.Signals;
using LittleMars.Model.Interfaces;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class GoalTracker : IGoalTracker, IOnGoalUpdated
    {
        protected readonly SignalBus _signalBus;

        protected bool _isDone = false;
        protected bool _isFirstDone = true;
        protected GoalUpdatedSignal _onUpdateSignal;
        protected GoalIsDoneSignal _isDoneSignal;
        public GoalTracker(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public bool Check()
        {
            return _isDone;
        }

        public virtual void OnGoalUpdated()
        {
            UpdateOnUpdatedSignal();
            _signalBus.Fire(_onUpdateSignal);
        }

        public virtual void OnGoalIsDone()
        {
            Debug.Log("Goal is done! Goal index " + _isDoneSignal.Index.ToString());
            _isDoneSignal.IsFirstDone = _isFirstDone;
            _signalBus.Fire(_isDoneSignal);
        }

        protected virtual void SetSignals(int index)
        { }

        protected virtual void UpdateOnUpdatedSignal()
        { }

        protected void CheckIsDone(bool isDone)
        {
            if (isDone == _isDone) return;

            _isDone = isDone;
            if (_isDone)
            {               
                //Debug.Log("Done!");
                OnGoalIsDone();
                if (_isFirstDone) _isFirstDone = false;
            }
            else
            {
                Debug.Log("Goal is lost!");
            }
        }

        public GoalUpdatedSignal GetInfo()
        {
            UpdateOnUpdatedSignal();
            return _onUpdateSignal;
        }
    }
}
