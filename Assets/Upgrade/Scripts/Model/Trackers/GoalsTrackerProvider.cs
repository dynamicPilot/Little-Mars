using LittleMars.Common.LevelGoal;
using LittleMars.Model.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.Trackers
{
    /// <summary>
    /// Provider class for the trackers creation.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class GoalsTrackerProvider<T1, T2> : IDisposable where T2 : GoalTracker
    {
        readonly TrackerFactory<T1> _trackerFactory;
        readonly TrackerFactoryWithTimer<T1> _trackerFactoryWithTimer;
        public GoalsTrackerProvider(TrackerFactory<T1> trackerFactory,
            TrackerFactoryWithTimer<T1> trackerFactoryWithTimer = null)
        { 
            _trackerFactory = trackerFactory;
            _trackerFactoryWithTimer = trackerFactoryWithTimer;
        }

        public List<IGoalTracker> CreateTrackers(Goals<T1> goals, ref int currentIndex)
        {
            if (goals.GoalsArray == null || goals.GoalsArray.Length == 0) return null;

            var trackers = new List<IGoalTracker>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                trackers.Add(_trackerFactory.Create(goals.GoalsArray[i], currentIndex));
                currentIndex++;
                Debug.Log("Create tracker for goal");
            }

            return trackers;
        }

        public List<IGoalTracker> CreateTrackersWithTimer(GoalsWithTimer<T1> goals, ref int currentIndex)
        {
            if (goals.GoalsArray == null || goals.GoalsArray.Length == 0 ||
                _trackerFactoryWithTimer == null) return null;

            var trackers = new List<IGoalTracker>();
            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                trackers.Add(_trackerFactoryWithTimer.Create(goals.GoalsArray[i], currentIndex));
                currentIndex++;
                Debug.Log("Create tracker for goal");
            }

            return trackers;
        }

        public void Dispose()
        {
            _trackerFactory.Dispose();
            if (_trackerFactoryWithTimer != null)
                _trackerFactoryWithTimer.Dispose();
        }

        public class Factory : PlaceholderFactory<GoalsTrackerProvider<T1, T2>>
        { }
    }
}
