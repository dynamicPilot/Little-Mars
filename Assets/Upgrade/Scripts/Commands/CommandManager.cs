using LittleMars.Common;
using System.Collections.Generic;


namespace LittleMars.Commands
{
    public class CommandManager
    {
        readonly NextCommand.Factory _nextFactory;
        readonly StartCommand.Factory _startFactory;

        readonly NullCommand _nullCommand;

        Dictionary<CommandType, ICommand> _commands;
        

        public CommandManager(NextCommand.Factory nextFactory, StartCommand.Factory startFactory, 
            NullCommand nullCommand)
        {
            _nextFactory = nextFactory;
            _startFactory = startFactory;
            _nullCommand = nullCommand;

            _commands = new Dictionary<CommandType, ICommand>();
        }

        public ICommand GetCommand(CommandType type)
        {
            if (!_commands.ContainsKey(type)) CreateCommand(type);
            return _commands[type];
        }

        void CreateCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.next:
                    _commands.Add(CommandType.next, _nextFactory.Create());
                    break;
                case CommandType.start:
                    _commands.Add(CommandType.start, _startFactory.Create());
                    break;
                default:
                    _commands.Add(type, _nullCommand);
                    break;
            }
        }
    }
}
