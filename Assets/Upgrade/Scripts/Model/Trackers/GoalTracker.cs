using LittleMars.Model.Interfaces;
using UnityEngine;

namespace LittleMars.Model.Trackers
{
    public class GoalTracker : IGoalTracker, IOnGoalUpdated
    {
        protected bool _isDone = false;
        protected bool _isFirstDone = true;

        public bool Check()
        {
            return _isDone;
        }

        public virtual void OnGoalUpdated()
        {
        }

        public virtual void OnGoalIsDone()
        {
        }

        protected void CheckIsDone(bool isDone)
        {
            if (isDone == _isDone) return;

            _isDone = isDone;
            if (_isDone)
            {               
                Debug.Log("Done!");
                OnGoalIsDone();
                if (_isFirstDone) _isFirstDone = false;
            }
            else
            {
                Debug.Log("Goal is lost!");
            }
        }
    }
}
