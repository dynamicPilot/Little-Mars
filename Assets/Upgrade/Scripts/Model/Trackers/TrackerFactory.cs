using LittleMars.Common.LevelGoal;
using LittleMars.Model.Interfaces;
using System;
using Zenject;

namespace LittleMars.Model.Trackers
{
    /// <summary>
    /// Class for PlaceholderFactory for any goal tracker.
    /// </summary>
    /// <typeparam name="T">BuildingUnit or ResourceUnit</typeparam>
    public class TrackerFactory<T> : PlaceholderFactory<Goal<T>, int, IGoalTracker>, IDisposable
    {
        public void Dispose()
        {
        }
    }

    public class TrackerFactoryWithTimer<T> : PlaceholderFactory<GoalWithTime<T>, int, IGoalTracker>, IDisposable
    {
        public void Dispose()
        {
        }
    }
}
