using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using LittleMars.UI.GoalDisplays;

namespace LittleMars.Model.GoalDisplays
{
    public class StaffGoalDisplayStrategiesFactory
    {
        readonly DisplayStrategiesFactory _strategiesFactory;
        readonly WithTimerGoalInfoFactory<BuildingUnit<int>> _timerFactory;

        public StaffGoalDisplayStrategiesFactory(DisplayStrategiesFactory strategiesFactory, 
            WithTimerGoalInfoFactory<BuildingUnit<int>> timerFactory)
        {
            _strategiesFactory = strategiesFactory;
            _timerFactory = timerFactory;
        }

        public IGoalDisplayStrategy CreateTimerStrategy(GoalWithTime<BuildingUnit<int>> goal)
        {
            return _strategiesFactory.CreateSlotWithTimer<BuildingUnit<int>>(GoalType.time, goal, _timerFactory);
        }
    }
}
