using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Commands
{
    public class CommandManager
    {
        readonly NextCommand.Factory _nextFactory;
        readonly StartCommand.Factory _startFactory;
        readonly MainMenuCommand.Factory _mainMenuFactory;
        readonly MainMenuByStartCommand.Factory _mainMenuByStartFactory;

        readonly NullCommand _nullCommand;

        Dictionary<CommandType, ICommand> _commands;
        

        public CommandManager(NextCommand.Factory nextFactory, StartCommand.Factory startFactory, 
            NullCommand nullCommand, MainMenuCommand.Factory mainMenuFactory,
            MainMenuByStartCommand.Factory mainMenuByStartFactory)
        {
            _nextFactory = nextFactory;
            _startFactory = startFactory;
            _nullCommand = nullCommand;
            _mainMenuFactory = mainMenuFactory;
            _mainMenuByStartFactory = mainMenuByStartFactory;

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
                    _commands.Add(type, _nextFactory.Create());
                    break;
                case CommandType.start:
                    _commands.Add(type, _startFactory.Create());
                    break;
                case CommandType.mainMenu:
                    _commands.Add(type, _mainMenuFactory.Create());
                    break;
                case CommandType.mainMenuByStart:
                    _commands.Add(type, _mainMenuByStartFactory.Create());
                    break;
                default:
                    _commands.Add(type, _nullCommand);
                    break;
            }
        }
    }
}
