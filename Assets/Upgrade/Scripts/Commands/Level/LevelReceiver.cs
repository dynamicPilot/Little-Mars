using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.PlayerStates;
using LittleMars.SceneControls;
using Zenject;

namespace LittleMars.Commands.Level
{
    public class LevelReceiver : Receiver
    {
        readonly SignalBus _signalBus;
        readonly LevelSceneControl _sceneControl;
        readonly IPlayerState _playerState;
        public LevelReceiver(SignalBus signalBus, LevelSceneControl sceneControl,
            IPlayerState playerState)
        {
            _signalBus = signalBus;
            _sceneControl = sceneControl;
            _playerState = playerState;
        }

        public override void Next()
        {
            // next level to player state
            _playerState.ToNextLevel();
            _sceneControl.NextSceneType(SceneType.level);
            _signalBus.Fire<EndLevelSignal>();
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
            _sceneControl.NextSceneType(SceneType.level);
            _signalBus.Fire<EndSceneSignal>();
        }
        public virtual void Start()
        {
            _signalBus.Fire<StartLevelSignal>();
        }
    }
}
