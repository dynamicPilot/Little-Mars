using LittleMars.Commands;
using LittleMars.Common;
using LittleMars.Model.TimeUpdate;

namespace LittleMars.UI.LevelMenus
{
    public class LevelMenu
    {
        CommandManager _commandManager;
        TimeSpeedManager _timeManager;

        public LevelMenu(CommandManager commandManager, TimeSpeedManager timeManager)
        {
            _commandManager = commandManager;
            _timeManager = timeManager;
        }

        public ICommand GetCommand(CommandType type)
        {
            return _commandManager.GetCommand(type);
        }

        public virtual void Open() 
        {
            _timeManager.Stop();
        }
        public virtual void Close() 
        {
            _timeManager.Start();
        }

    }
}
