using LittleMars.Commands.Level;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.GoalDisplays;
using Zenject;

namespace LittleMars.LevelMenus
{
    public class GameOverLevelMenu : LevelMenu
    {
        readonly StaffGoalDisplayStrategiesManager _staffStrategyManager;
        readonly GoalDisplayStrategiesManager _strategyManager;
        public GameOverLevelMenu(LevelCommandManager commandManager, TimeSpeedManager timeManager,
            GoalDisplayStrategiesManager strategyManager,
            StaffGoalDisplayStrategiesManager staffStrategyManager,
            SignalBus signalBus)
            : base(commandManager, timeManager, signalBus)
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
