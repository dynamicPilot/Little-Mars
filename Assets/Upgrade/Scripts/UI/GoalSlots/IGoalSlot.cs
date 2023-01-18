using LittleMars.Common.Signals;

namespace LittleMars.UI.GoalSlots
{
    public interface IGoalSlot
    {
        void UpdateSlot(GoalUpdatedSignal args);
    }

}
