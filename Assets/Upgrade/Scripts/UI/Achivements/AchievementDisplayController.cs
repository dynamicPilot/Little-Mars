using LittleMars.Commands;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.LevelMenus;

namespace LittleMars.UI.Achievements
{
    public class AchievementDisplayLevelMenu : LevelMenu
    {
        readonly GoalDisplayStrategiesManager _manager;

        public AchievementDisplayLevelMenu(CommandManager commandManager,
            TimeSpeedManager timeSpeedManager,
            GoalDisplayStrategiesManager manager) : base(commandManager, timeSpeedManager)
        {
            _manager = manager;
        }

        public IGoalDisplayStrategy GetDisplayStrategy(int index)
        {
            return _manager.GetStrategy(index);
        }  
    }
}
