using LittleMars.Common;

namespace LittleMars.Model.GoalDisplays
{
    public interface IGoalInfo
    {
        GoalType GetType();
        bool WithTimer();
        float[] GetAmount();
        int GetIconIndex();
        int GetSize();
    }

    public abstract class GoalInfo : IGoalInfo
    {
        GoalType _type;
        bool _withTimer;

        protected float[] _amount = null;
        protected int _iconIndex;
        protected int _size;

        public GoalInfo(GoalType type, bool withTimer)
        {
            _type = type;
            _withTimer = withTimer;
        }

        public float[] GetAmount()
        {
            return _amount;
        }
        public int GetIconIndex()
        {
            return _iconIndex;
        }
        public int GetSize()
        {
            return _size;
        }

        public bool WithTimer()
        {
            return _withTimer;
        }

        GoalType IGoalInfo.GetType()
        {
            return _type;
        }
    }
}
