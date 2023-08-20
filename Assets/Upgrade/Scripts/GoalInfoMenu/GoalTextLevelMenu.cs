using LittleMars.Commands.Level;
using LittleMars.LevelMenus;
using LittleMars.Localization;
using LittleMars.Model.TimeUpdate;
using Zenject;

namespace LittleMars.GoalInfoMenu
{
    public class GoalTextLevelMenu : LevelMenu
    {
        readonly LevelLangsManager _langManager;
        public GoalTextLevelMenu(LevelCommandManager commandManager,
            TimeSpeedManager timeManager, LevelLangsManager langManager,
            SignalBus signalBus) : base(commandManager, timeManager, null, signalBus)
        {
            _langManager = langManager;
        }

        public string[] GetGoalTexts()
        {
            return _langManager.GetAllTagTexts("goal", Common.TagGroup.level);
        }
    }
}
