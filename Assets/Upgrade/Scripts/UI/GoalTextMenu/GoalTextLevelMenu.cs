using JetBrains.Annotations;
using LittleMars.Commands;
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
        readonly LangsManager _langManager;
        public GoalTextLevelMenu(CommandManager commandManager, 
            TimeSpeedManager timeManager, LangsManager langManager) : base(commandManager, timeManager)
        {
            _langManager = langManager;
        }

        public string[] GetGoalTexts()
        {
            return _langManager.GetAllTagTexts("goal", Common.TagGroup.level);
        }
    }
}
