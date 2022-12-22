using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LittleMars.Common.LevelGoal
{
    [Serializable]
    public class Goal<T>
    {
        public ResultType Type;
        public T Unit;
    }

    [Serializable]
    public class GoalWithTime<T> : Goal<T>
    {
        public float Time;
    }

}

