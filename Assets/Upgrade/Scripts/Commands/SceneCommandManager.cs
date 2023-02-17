using LittleMars.Common;
using System.Collections.Generic;

namespace LittleMars.Commands
{
    /// <summary>
    /// A base class for Level and MainMenu command managers. 
    /// Uses for the command support for GameMenu.
    /// </summary>
    public class SceneCommandManager
    {
        readonly ProjectCommandManager _projectManager;

        protected Dictionary<CommandType, ICommand> _commands;
        public SceneCommandManager(ProjectCommandManager projectManager)
        {
            _projectManager = projectManager;

            _commands = new();
        }

        public ICommand GetCommand(CommandType type)
        {
            if (!_commands.ContainsKey(type)) CreateCommand(type);
            return _commands[type];
        }

        protected virtual void CreateCommand(CommandType type)
        { }

        protected ICommand GetCommandFromProjectManager(CommandType type, Receiver receiver)
        {
            var extendedCommand = _projectManager.GetCommand(type);
            extendedCommand.ChangeReceiver(receiver);

            return extendedCommand;
        }
    }
}
