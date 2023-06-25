using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Commands.Level
{
    /// <summary>
    /// Create commands from factories for level. Set levelReceiver as a receiver for common commands.
    /// </summary>
    public class LevelCommandManager : SceneCommandManager
    {
        readonly LevelReceiver _levelReceiver;

        readonly StartCommand.Factory _startFactory;
        readonly MainMenuByStartCommand.Factory _mainMenuByStartFactory;
        readonly GoalInfoCommand.Factory _goalInfoFactory;
        readonly RestartLevelCommand.Factory _restartFactory;

        public LevelCommandManager(ProjectCommandManager projectManager, StartCommand.Factory startFactory,
            MainMenuByStartCommand.Factory mainMenuByStartFactory, LevelReceiver levelReceiver,
            GoalInfoCommand.Factory goalInfoFactory, RestartLevelCommand.Factory restartFactory)
            : base(projectManager)
        {
            _startFactory = startFactory;
            _mainMenuByStartFactory = mainMenuByStartFactory;
            _levelReceiver = levelReceiver;
            _goalInfoFactory = goalInfoFactory;
            _restartFactory = restartFactory;
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
                case CommandType.restart:
                    _commands.Add(type, _restartFactory.Create());
                    break;
                default:
                    _commands.Add(type, GetCommandFromProjectManager(type, _levelReceiver));
                    break;
            }
        }
    }
}
