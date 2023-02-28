using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Commands.Level
{
    public class LevelCommandManager : SceneCommandManager
    {
        readonly LevelReceiver _levelReceiver;

        readonly StartCommand.Factory _startFactory;
        readonly MainMenuByStartCommand.Factory _mainMenuByStartFactory;
        readonly GoalInfoCommand.Factory _goalInfoFactory;

        public LevelCommandManager(ProjectCommandManager projectManager,
            StartCommand.Factory startFactory,
            MainMenuByStartCommand.Factory mainMenuByStartFactory,
            LevelReceiver levelReceiver,
            GoalInfoCommand.Factory goalInfoFactory)
            : base(projectManager)
        {
            _startFactory = startFactory;
            _mainMenuByStartFactory = mainMenuByStartFactory;
            _levelReceiver = levelReceiver;
            _goalInfoFactory = goalInfoFactory;
        }
            
        protected override void CreateCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.next:
                case CommandType.mainMenu:
                    _commands.Add(type, GetCommandFromProjectManager(type, _levelReceiver));
                    break;
                case CommandType.start:
                    _commands.Add(type, _startFactory.Create());
                    break;
                case CommandType.mainMenuByStart:
                    _commands.Add(type, _mainMenuByStartFactory.Create());
                    break;
                case CommandType.goalsInfo:
                    _commands.Add(type, _goalInfoFactory.Create());
                    break;
                default:
                    _commands.Add(type, GetCommandFromProjectManager(type, _levelReceiver));
                    break;
            }
        }
    }
}
