using LittleMars.Common;

namespace LittleMars.Model.GoalDisplays
{
    public class BuildingWithTimerGoalInfo : GoalInfo
    {
        public BuildingWithTimerGoalInfo(GoalType type, float timer, BuildingUnit<int> unit) 
            : base(type, true)
        {
            _amount = new float[2] { unit.Amount, timer };
            _iconIndex = (int)unit.Type;
            _size = (int)unit.Size;
        }
    }
}
