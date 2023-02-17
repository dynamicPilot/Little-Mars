using LittleMars.Commands;
using LittleMars.Common;
using UnityEngine;

namespace LittleMars.LevelMenus
{
    public class GameMenu
    {
        readonly SceneCommandManager _commandManager;

        public GameMenu(SceneCommandManager commandManager)
        {
            _commandManager = commandManager;
        }

        public ICommand GetCommand(CommandType type)
        {
            return _commandManager.GetCommand(type);
        }

        public virtual void Open()
        { }

        public virtual void Close()
        { }
    }
}
