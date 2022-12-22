using System;

namespace LittleMars.Common.LevelGoal
{
    [Serializable]
    public class Goals<T>
    {
        // no time goals
        public Goal<T>[] NoTimeGoals;

        // goals with time
        public GoalWithTime<T>[] TimeGoal;
    }
}
