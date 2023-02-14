using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;

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
            //Debug.Log("Try get command " + type + ". Has it: " + (_commands.ContainsKey(type)));
            if (!_commands.ContainsKey(type)) CreateCommand(type);
            return _commands[type];
        }

        void CreateCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.next:
                    //Debug.Log("create command for " + type);
                    _commands.Add(type, _nextFactory.Create());
                    break;
                case CommandType.start:
                    _commands.Add(type, _startFactory.Create());
                    break;
                default:
                    //Debug.Log("null command for "+ type);
                    _commands.Add(type, _nullCommand);
                    break;
            }
        }
    }
}
