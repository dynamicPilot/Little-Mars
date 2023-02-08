using LittleMars.Buildings.Timers;
using LittleMars.Common;
using LittleMars.Common.LevelGoal;

namespace LittleMars.Model.GoalDisplays
{
    public class GoalForStaffGoalCreator
    {
        readonly BuildingTimer.Settings _timerSettings;

        public GoalForStaffGoalCreator(BuildingTimer.Settings timerSettings)
        {
            _timerSettings = timerSettings;
        }

        public GoalWithTime<BuildingUnit<int>> CreateTimerGoal(BuildingType type, Size size = Size.small)
        {
            var unit = new GoalWithTime<BuildingUnit<int>>();
            unit.Time = _timerSettings.TimerTargetValue;

            unit.Unit = new BuildingUnit<int>();
            unit.Unit.Type = type;
            unit.Unit.Size = size;
            unit.Unit.Amount = 1;

            return unit;

        }
    }
}
