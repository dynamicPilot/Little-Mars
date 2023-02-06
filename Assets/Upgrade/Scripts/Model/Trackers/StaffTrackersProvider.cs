using LittleMars.Model.Interfaces;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Model.Trackers
{
    public class StaffTrackersProvider : IDisposable
    {
        readonly FakeTrackerFactory _timerFactory;

        public StaffTrackersProvider(FakeTrackerFactory timerFactory)
        {
            _timerFactory = timerFactory;
        }

        public List<IGoalTracker> CreateTrackers()
        {
            var trackers = new List<IGoalTracker>();
            trackers.Add(_timerFactory.Create(0));

            return trackers;
        }

        public void Dispose()
        {
            _timerFactory.Dispose();
        }

        public class Factory : PlaceholderFactory<StaffTrackersProvider>
        { }
    }
}
