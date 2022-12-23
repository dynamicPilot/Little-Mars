using LittleMars.Model.Interfaces;
using UnityEngine;

namespace LittleMars.Model.Trackers
{
    public class GoalTracker : IGoalTracker
    {
        protected bool _isDone = false;

        public bool Check()
        {
            return _isDone;
        }

        protected void CheckIsDone(bool isDone)
        {
            if (isDone == _isDone) return;

            _isDone = isDone;
            if (_isDone)
            {
                Debug.Log("Done!");
            }
            else
            {
                Debug.Log("Goal is lost!");
            }
        }
    }
}
