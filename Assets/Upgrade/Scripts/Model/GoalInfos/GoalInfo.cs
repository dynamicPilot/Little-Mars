using LittleMars.Common;
using Zenject;

namespace LittleMars.Model.GoalInfos
{
    public interface IGoalInfo
    {

    }

    public interface IGetInfo<T>
    {
        float[] GetAmount();
        int GetIconIndex();
        int GetSize();
    }


    public class GoalInfo<T> : IGoalInfo
    {
        GoalType _type;
        bool _withTimer;
        T _unit;

        public GoalInfo(GoalType type, bool withTimer, T unit)
        {
            _type = type;
            _withTimer = withTimer;
            _unit = unit;
        }

        public class Factory : PlaceholderFactory<GoalType, bool, T, GoalInfo<T>>
        { }
    }
}
