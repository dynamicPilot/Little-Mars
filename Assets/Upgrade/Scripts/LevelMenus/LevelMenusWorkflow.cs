using LittleMars.Common;
using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.LevelMenus
{
    public class LevelMenusWorkflow : IInitializable
    {
        readonly SignalBus _signalBus;
        readonly LevelMenusWorkflowTimer _timer;

        MenuState _state;
        CallAchivementMenuSignal _achivementSignal;

        public LevelMenusWorkflow(SignalBus signalBus, LevelMenusWorkflowTimer timer)
        {
            _signalBus = signalBus;
            _state = MenuState.none;
            _achivementSignal = new CallAchivementMenuSignal();
            _timer = timer;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);
            _signalBus.Subscribe<StartLevelSignal>(OnStartLevel);
            _signalBus.Subscribe<AchievementReachedSignal>(OnAchievementReached);
            _signalBus.Subscribe<EndGameReachedSignal>(OnEndGameReached);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        private void ChangeStateTo(MenuState state)
        {
            Debug.Log("LevelMenusWorkflow: Change state to" + state);
            _state = state;
        }

        private bool CanChangeStateTo(MenuState state)
        {
            Debug.Log("LevelMenusWorkflow: CanChangeStateTo " + state + ". CurrentState " + _state);
            if (state == MenuState.achievement)
                return _state == MenuState.none;
            else
                return false;
        }

        // start menu start signal
        private void OnStrategiesIsReady(GoalStrategiesIsReadySignal args)
        {
            _signalBus.Unsubscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);
            ChangeStateTo(MenuState.start);
        }

        // start menu closures for the game
        private void OnStartLevel()
        {
            _signalBus.Unsubscribe<StartLevelSignal>(OnStartLevel);
            ChangeStateTo(MenuState.none);
        }


        // achievement
        private void OnAchievementReached(AchievementReachedSignal args)
        {
            Debug.Log("LevelMenusWorkflow: Achivement Reached");
            //_signalBus.Unsubscribe<AchievementReachedSignal>(OnAchievementReached);

            if (CanChangeStateTo(MenuState.achievement))
            {
                ChangeStateTo(MenuState.achievement);
                CallAchievementMenu(args.GoalIndex);
            }
        }


        // end game is reached -> need call for a delay before end game menu appearce
        private void OnEndGameReached()
        {
            EndGameUnsubscribe();
            ChangeStateTo(MenuState.end);
            _timer.StartEndMenuTimer();
        }

        private void OnGameOver()
        {
            EndGameUnsubscribe();
            ChangeStateTo(MenuState.gameOver);
        }

        private void EndGameUnsubscribe()
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<EndGameReachedSignal>(OnEndGameReached);
        }

        private void CallAchievementMenu(int goalIndex)
        {
            _achivementSignal.GoalIndex = goalIndex;
            _signalBus.Fire(_achivementSignal);
        }
    }
}