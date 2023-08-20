using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.PlayerStates;
using LittleMars.SceneControls;
using LittleMars.UI.GoalInfoMenu;
using UnityEngine;
using Zenject;

namespace LittleMars.Commands.Level
{
    public class LevelReceiver : Receiver
    {
        readonly SignalBus _signalBus;
        readonly LevelSceneControl _sceneControl;
        readonly IPlayerState _playerState;

        bool _isGameOver;
        public LevelReceiver(SignalBus signalBus, LevelSceneControl sceneControl,
            IPlayerState playerState)
        {
            _signalBus = signalBus;
            _sceneControl = sceneControl;
            _playerState = playerState;

            _isGameOver = false;
        }
        public override void Next()
        {
            // next level to player state
            _playerState.AutoNextLevel();

            bool _hasNext = _playerState.CheckNextLevelAndGetCheck();
            if (!_isGameOver) _playerState.SaveCurrentLevelAsCompleted(); // save if no game over

            if (_hasNext) ToNextLevel();
            else MainMenu();
        }

        public override void MainMenu()
        {
            _sceneControl.NextSceneType(SceneType.menu);
            _signalBus.Fire<EndLevelSignal>();
        }

        public virtual void MainMenuByStart()
        {
            _sceneControl.NextSceneType(SceneType.menu);
            _signalBus.Fire<EndSceneSignal>();
        }
        public virtual void Restart()
        {
            Debug.Log("Level Receiver: restart");
            _sceneControl.NextSceneType(SceneType.level);
            _signalBus.Fire<EndSceneSignal>();
        }
        public virtual void Start()
        {
            _signalBus.Fire<StartLevelSignal>();
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        public void GoalInfo()
        {
            _signalBus.Fire<NeedGoalInfoSignal>();
        }
        void ToNextLevel()
        {
            _playerState.ToNextLevel();
            _sceneControl.NextSceneType(SceneType.level);
            _signalBus.Fire<EndLevelSignal>();
        }

        void OnGameOver()
        {
            _isGameOver = true;
        }
    }
}
