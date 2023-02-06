using LittleMars.Common;
using LittleMars.UI.GoalDisplays;

namespace LittleMars.Model.GoalDisplays
{
    public class StaffGoalDisplayStrategiesManager
    {
        readonly StaffGoalDisplayStrategiesFactory _factory;
        readonly GoalForStaffGoalCreator _goalCreator;
        readonly GoalsManager _goalManager;
        public StaffGoalDisplayStrategiesManager(StaffGoalDisplayStrategiesFactory factory, 
            GoalForStaffGoalCreator goalCreator,
            GoalsManager goalManager)
        {
            _factory = factory;
            _goalCreator = goalCreator;
            _goalManager = goalManager;
        }

        public IGoalDisplayStrategy GetStrategy(int index)
        {
            var tracker = _goalManager.GetStaffTracker(index);
            if (tracker == null) return null;

            var info = tracker.GetInfo();
            var goal = _goalCreator.CreateTimerGoal((BuildingType)info.Values[0], (Size)info.Values[1]);

            return _factory.CreateTimerStrategy(goal);
        }
    }
}

