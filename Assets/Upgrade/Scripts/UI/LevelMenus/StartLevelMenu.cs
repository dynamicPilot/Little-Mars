using LittleMars.Commands;
using LittleMars.Model.TimeUpdate;

namespace LittleMars.UI.LevelMenus
{
    public class StartLevelMenu : LevelMenu
    {
        public StartLevelMenu(CommandManager commandManager, TimeSpeedManager timeManager) 
            : base(commandManager, timeManager)
        {
        }
    }
}
