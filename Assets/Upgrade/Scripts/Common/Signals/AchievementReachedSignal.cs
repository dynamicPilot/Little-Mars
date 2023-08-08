namespace LittleMars.Common.Signals
{
    public struct AchievementReachedSignal
    {
        public int GoalIndex;
    }

    public struct CallAchivementMenuSignal
    {
        public int GoalIndex;
    }

    public struct CallGameStateMenuSignal
    {
    }

    public struct AchievementIsClosedSignal
    { }

    public struct WindowIsClosedSignal
    {
        public int MenuState;
    }

}
