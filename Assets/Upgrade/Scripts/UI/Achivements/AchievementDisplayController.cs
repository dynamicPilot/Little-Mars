using LittleMars.Commands.Level;
using LittleMars.LevelMenus;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.GoalDisplays;
using Zenject;

namespace LittleMars.UI.Achievements
{
    public class AchievementDisplayLevelMenu : LevelMenu
    {
        //readonly GoalDisplayStrategiesManager _manager;

        public AchievementDisplayLevelMenu(LevelCommandManager commandManager,
            TimeSpeedManager timeSpeedManager,
            GoalDisplayStrategiesManager manager,
            SignalBus signalBus) : base(commandManager, timeSpeedManager, manager, signalBus)
        {
            //_manager = manager;
        }

        public IGoalDisplayStrategy GetDisplayStrategy(int index)
        {
            return _strategiesManager.GetStrategy(index);
        }  
    }
}
