using LittleMars.UI.GoalDisplays;

namespace LittleMars.Common.Signals
{
    public struct GoalUpdatedSignal
    {
        public int Index;
        public float[] Values;
    }

    public struct GoalStrategiesIsReadySignal
    {
        public IGoalDisplayStrategy[] Strategies;
    }
}
