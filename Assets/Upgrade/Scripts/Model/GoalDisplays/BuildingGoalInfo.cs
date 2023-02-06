using LittleMars.Common;
using static UnityEngine.UI.CanvasScaler;

namespace LittleMars.Model.GoalDisplays
{
    public class BuildingGoalInfo : GoalInfo
    {
        public BuildingGoalInfo(GoalType type, BuildingUnit<int> unit)
            : base(type, false)
        {
            _amount = new float[1] { unit.Amount };
            _iconIndex = (int)unit.Type;
            _size = (int)unit.Size;
        }
    }
}
