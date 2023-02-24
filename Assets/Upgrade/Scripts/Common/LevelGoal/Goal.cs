using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LittleMars.Common.LevelGoal
{
    /// <summary>
    /// A base goal class for goals without timers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Goal<T>
    {
        public ResultType Type;
        public T Unit;
    }

    /// <summary>
    /// Goal with timer class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class GoalWithTime<T> : Goal<T>
    {
        public float Time;
    }

}

