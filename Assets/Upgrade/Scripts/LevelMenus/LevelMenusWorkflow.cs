using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

namespace LittleMars.LevelMenus
{
    public class LevelMenusWorkflow : IInitializable
    {
        readonly SignalBus _signalBus;
        readonly LevelMenusWorkflowTimer _timer;

        MenuState _state;
        OpenWindowByIdSignal _achivementSignal;
        OpenWindowByIdSignal _gameStateSignal;

        public LevelMenusWorkflow(SignalBus signalBus, LevelMenusWorkflowTimer timer)
        {
            _signalBus = signalBus;
            _state = MenuState.none;
            _achivementSignal = new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_achievement,
                SenderId = -1,
                NextSenderState = 0,
                Context = new WindowContext()
            };

            _gameStateSignal = new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_state,
                SenderId = -1,
                NextSenderState = 0,
                Context = null
            };

            _timer = timer;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);
            _signalBus.Subscribe<StartLevelSignal>(OnStartLevel);
            _signalBus.Subscribe<AchievementReachedSignal>(OnAchievementReached);
            _signalBus.Subscribe<EndGameReachedSignal>(OnEndGameReached);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<CallGameStateMenuSignal>(OnCallForGameStateMenu);
            _signalBus.Subscribe<WindowIsClosedSignal>(OnWindowIsClosed);
        }

        void ChangeStateTo(MenuState state)
        {
            Debug.Log("LevelMenusWorkflow: Change state to" + state);
            if (state != MenuState.none && state != _state) HideOpenedWindow();
            _state = state;
        }

        bool CanChangeStateTo(MenuState state)
        {
            Debug.Log("LevelMenusWorkflow: CanChangeStateTo " + state + ". CurrentState " + _state);
            if (state == MenuState.achievement)
                return _state == MenuState.none;
            else
                return false;
        }

        // start menu start signal
        void OnStrategiesIsReady(GoalStrategiesIsReadySignal args)
        {
            _signalBus.Unsubscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);
            ChangeStateTo(MenuState.start);

            // open start menu
            _signalBus.Fire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_startMenu,
                SenderId = -1,
                NextSenderState = 0,
                Context = null
            });
        }

        // start menu -> go to the game
        void OnStartLevel()
        {
            _signalBus.Unsubscribe<StartLevelSignal>(OnStartLevel);
            ChangeStateTo(MenuState.none);
        }

        // achievement
        void OnAchievementReached(AchievementReachedSignal args)
        {
            Debug.Log("LevelMenusWorkflow: Achivement Reached");
            if (CanChangeStateTo(MenuState.achievement))
            {
                ChangeStateTo(MenuState.achievement);
                CallAchievementMenu(args.GoalIndex);
            }
        }

        void OnCallForGameStateMenu()
        {
            ChangeStateTo(MenuState.state);
            _signalBus.Fire(_gameStateSignal);
        }

        // end game is reached -> need call for a delay before end game menu appearce
        void OnEndGameReached()
        {
            EndGameUnsubscribe();           
            _timer.StartEndMenuTimer();
            _signalBus.Subscribe<EndGameSignal>(OnEndGame);
        }

        void OnEndGame()
        {
            _signalBus.Unsubscribe<EndGameSignal>(OnEndGame);
            ChangeStateTo(MenuState.end);
            // open end menu
            _signalBus.Fire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_end,
                SenderId = -1,
                NextSenderState = 0,
                Context = null
            });
        }

        void OnGameOver(GameOverSignal args)
        {
            EndGameUnsubscribe();
            ChangeStateTo(MenuState.gameOver);
            // open game over menu
            _signalBus.Fire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_gameOver,
                SenderId = -1,
                NextSenderState = 0,
                Context = new WindowContext(new int[2] { args.GoalIndex, (args.IsStaff) ? 1 : 0 })
            });
        }

        void EndGameUnsubscribe()
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<EndGameReachedSignal>(OnEndGameReached);
        }

        void CallAchievementMenu(int goalIndex)
        {
            _achivementSignal.Context.Indexes = new int[1] { goalIndex };
            _signalBus.Fire(_achivementSignal);
        }

        void OnWindowIsClosed(WindowIsClosedSignal arg)
        {
            var state = (MenuState)arg.MenuState;
            if (_state == state) ChangeStateTo(MenuState.none);
        }

        void HideOpenedWindow()
        {
            if (_state == MenuState.none) return;

            var id = -1;
            if (_state == MenuState.state) id = (int)WindowID.level_state;
            else if (_state == MenuState.achievement) id = (int)WindowID.level_achievement;

            if (id < 0) return;

            Debug.Log("LevelMenuWorkflow: close window " + (MenuState)id);
            _signalBus.Fire(new WindowStateByIdSignal
            {
                Id = id,
                SenderState = (int)WindowState.hide,
            });

        }
    }
}