using LittleMars.Common;
using System.Collections.Generic;


namespace LittleMars.Commands
{
    /// <summary>
    /// Project level command manager: for levels and menu loading commands.
    /// </summary>
    public class ProjectCommandManager
    {
        readonly NextCommand.Factory _nextFactory;
        readonly MainMenuCommand.Factory _mainMenuFactory;

        readonly NullCommand _nullCommand;

        Dictionary<CommandType, IExtendedCommand> _commands;

        public ProjectCommandManager(NextCommand.Factory nextFactory, 
            MainMenuCommand.Factory mainMenuFactory, NullCommand nullCommand)
        {
            _nextFactory = nextFactory;
            _mainMenuFactory = mainMenuFactory;
            _nullCommand = nullCommand;

            _commands = new();
        }

        public IExtendedCommand GetCommand(CommandType type)
        {
            if (!_commands.ContainsKey(type)) CreateCommand(type);
            return _commands[type];
        }

        void CreateCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.next:
                    _commands.Add(type, _nextFactory.Create());
                    break;
                case CommandType.mainMenu:
                    _commands.Add(type, _mainMenuFactory.Create());
                    break;
                default:
                    _commands.Add(type, _nullCommand);
                    break;
            }
        }
    }
}
