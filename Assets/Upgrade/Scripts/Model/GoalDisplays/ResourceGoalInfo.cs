using LittleMars.Common;

namespace LittleMars.Model.GoalDisplays
{
    public class ResourceGoalInfo : GoalInfo
    {
        public ResourceGoalInfo(GoalType type, ResourceUnit<float> unit) : base(type, false)
        {
            _amount = new float[1] { unit.Amount};
            _iconIndex = (int)unit.Type;
            _size = 0;
        }
    }
}
