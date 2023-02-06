using LittleMars.Commands;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.GoalDisplays;

namespace LittleMars.UI.LevelMenus
{
    public class GameOverLevelMenu : LevelMenu
    {
        readonly StaffGoalDisplayStrategiesManager _staffStrategyManager;
        readonly GoalDisplayStrategiesManager _strategyManager;
        public GameOverLevelMenu(CommandManager commandManager, TimeSpeedManager timeManager,
            GoalDisplayStrategiesManager strategyManager,
            StaffGoalDisplayStrategiesManager staffStrategyManager) 
            : base(commandManager, timeManager)
        {
            _strategyManager = strategyManager;
            _staffStrategyManager = staffStrategyManager;
        }


        public IGoalDisplayStrategy GetStrategy(int index, bool isStaff)
        {
            if (isStaff) return _staffStrategyManager.GetStrategy(index);
            else return _strategyManager.GetStrategy(index);
        }
    }
}
