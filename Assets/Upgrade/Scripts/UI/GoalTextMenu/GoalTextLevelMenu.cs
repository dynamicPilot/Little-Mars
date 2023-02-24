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

namespace LittleMars.UI.GoalTextMenu
{
    public class GoalTextLevelMenu : LevelMenu
    {
        readonly LevelLangsManager _langManager;
        public GoalTextLevelMenu(LevelCommandManager commandManager, 
            TimeSpeedManager timeManager, LevelLangsManager langManager) : base(commandManager, timeManager)
        {
            _langManager = langManager;
        }

        public string[] GetGoalTexts()
        {
            return _langManager.GetAllTagTexts("goal", Common.TagGroup.level);
        }
    }
}
