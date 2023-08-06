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

            _timer = timer;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);
            _signalBus.Subscribe<StartLevelSignal>(OnStartLevel);
            _signalBus.Subscribe<AchievementReachedSignal>(OnAchievementReached);
            _signalBus.Subscribe<AchievementIsClosedSignal>(OnAchievemtIsClosed);
            _signalBus.Subscribe<EndGameReachedSignal>(OnEndGameReached);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        void ChangeStateTo(MenuState state)
        {
            Debug.Log("LevelMenusWorkflow: Change state to" + state);
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

        // start menu closures for the game
        private void OnStartLevel()
        {
            _signalBus.Unsubscribe<StartLevelSignal>(OnStartLevel);
            ChangeStateTo(MenuState.none);
        }


        // achievement
        void OnAchievementReached(AchievementReachedSignal args)
        {
            Debug.Log("LevelMenusWorkflow: Achivement Reached");
            //_signalBus.Unsubscribe<AchievementReachedSignal>(OnAchievementReached);

            if (CanChangeStateTo(MenuState.achievement))
            {
                ChangeStateTo(MenuState.achievement);
                CallAchievementMenu(args.GoalIndex);
            }
        }

        void OnAchievemtIsClosed()
        {
            ChangeStateTo(MenuState.none);
        }


        // end game is reached -> need call for a delay before end game menu appearce
        void OnEndGameReached()
        {
            EndGameUnsubscribe();
            ChangeStateTo(MenuState.end);
            _timer.StartEndMenuTimer();
            _signalBus.Subscribe<EndGameSignal>(OnEndGame);
        }

        void OnEndGame()
        {
            _signalBus.Unsubscribe<EndGameSignal>(OnEndGame);
            // open end menu
            _signalBus.Fire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_end,
                SenderId = (int)WindowID.level_achievement,
                NextSenderState = (int)WindowState.hide,
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
                SenderId = (int)WindowID.level_achievement,
                NextSenderState = (int)WindowState.hide,
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
            // open achievemt menu
            //_signalBus.Fire(new OpenWindowByIdSignal
            //{
            //    Id = (int)WindowID.level_achievement,
            //    SenderId = -1,
            //    NextSenderState = 0
            //});
        }
    }
}