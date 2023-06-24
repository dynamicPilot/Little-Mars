using LittleMars.AudioSystems;
using LittleMars.Commands.Level;
using LittleMars.Common;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.GoalDisplays;
using Zenject;

namespace LittleMars.LevelMenus
{
    /// <summary>
    /// Support class for LevelMenuUI, provides time management and command management.
    /// </summary>
    public class LevelMenu : GameMenu
    {
        readonly TimeSpeedManager _timeManager;
        protected readonly GoalDisplayStrategiesManager _strategiesManager;
        public LevelMenu(LevelCommandManager commandManager, TimeSpeedManager timeManager,
            GoalDisplayStrategiesManager strategiesManager, SignalBus signalBus)
            : base(commandManager, signalBus)
        {
            _timeManager = timeManager;
            _strategiesManager = strategiesManager;
        }

        public override void Open()
        {
            _timeManager.Stop();
        }
        public override void Close()
        {
            _timeManager.Start();
        }

        public virtual IGoalDisplayStrategy[] GetStrategies()
        {
            return _strategiesManager.GetStrategies();
        }


    }
}
