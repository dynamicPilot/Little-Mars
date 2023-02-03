using LittleMars.Common.Signals;

namespace LittleMars.Model.Interfaces
{
    public interface IGoalTracker
    {
        bool Check();
        GoalUpdatedSignal GetInfo();
    }

    public interface IOnGoalUpdated
    {
        void OnGoalUpdated();
    }

}
