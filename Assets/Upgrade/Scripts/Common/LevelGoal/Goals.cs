using System;

namespace LittleMars.Common.LevelGoal
{
    [Serializable]
    public class Goals<T>
    {
        // no time goals
        public Goal<T>[] GoalsArray;
   
    }
    [Serializable]
    public class GoalsWithTimer<T>
    {
        // goals with time
        public GoalWithTime<T>[] GoalsArray;
    }
}
