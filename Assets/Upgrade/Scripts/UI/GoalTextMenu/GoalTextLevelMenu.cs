using JetBrains.Annotations;
using LittleMars.Commands.Level;
using LittleMars.LevelMenus;
using LittleMars.Localization;
using LittleMars.Model.TimeUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.UI.GoalTextMenu
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
