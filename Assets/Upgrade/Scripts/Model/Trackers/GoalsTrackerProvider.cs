using LittleMars.Common.LevelGoal;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.Trackers
{
    /// <summary>
    /// Provider class for the trackers creation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GoalsTrackerProvider<T> : IDisposable
    {
        readonly TrackerFactory<T> _trackerFactory;
        readonly TrackerFactoryWithTimer<T> _withTimeTrackerFactory;

        public GoalsTrackerProvider(TrackerFactory<T> trackerFactory, TrackerFactoryWithTimer<T> withTimeTrackerFactory)
        {
            _trackerFactory = trackerFactory;
            _withTimeTrackerFactory = withTimeTrackerFactory;
        }

        public void Create(Goals<T> goals)
        {
            if (goals.NoTimeGoals != null && goals.NoTimeGoals.Length > 0)
                CreateNoTimeTrackers(goals);

            if (goals.TimeGoal != null && goals.TimeGoal.Length > 0)
                CreateTimeSensitiveTrackers(goals);
        }

        private void CreateNoTimeTrackers(Goals<T> goals)
        {
            for (int i = 0; i < goals.NoTimeGoals.Length; i++)
            {
                _trackerFactory.Create(goals.NoTimeGoals[i]);
                Debug.Log("Create tracker for goal");
            }           
        }

        private void CreateTimeSensitiveTrackers(Goals<T> goals)
        {
            for (int i = 0; i < goals.TimeGoal.Length; i++)
            {
                _withTimeTrackerFactory.Create(goals.TimeGoal[i]);
                Debug.Log("Create tracker for goal with timer");
            }            
        }

        public void Dispose()
        {
            _trackerFactory.Dispose();
            _withTimeTrackerFactory.Dispose();
        }

        public class Factory : PlaceholderFactory<GoalsTrackerProvider<T>>
        { }
    }
}
