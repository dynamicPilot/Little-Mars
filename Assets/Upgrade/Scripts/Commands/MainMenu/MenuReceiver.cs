using LittleMars.Common.Signals;
using LittleMars.PlayerStates;
using LittleMars.SceneControls;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Commands.MainMenu
{
    public class MenuReceiver : Receiver
    {
        readonly SignalBus _signalBus;
        readonly MenuSceneControl _sceneControl;
        readonly IPlayerState _playerState;

        public MenuReceiver(SignalBus signalBus, MenuSceneControl sceneControl, IPlayerState playerState)
        {
            _signalBus = signalBus;
            _sceneControl = sceneControl;
            _playerState = playerState;
        }

        public override void Next()
        {
            Debug.Log("It was next command");
            _playerState.ToNextLevel();
            _sceneControl.NextSceneType(Common.SceneType.level);
            _signalBus.Fire<EndSceneSignal>();
        }

        public void ToLevel(int levelIndex)
        {
            Debug.Log("It was toLevel command");
            _playerState.SetNextLevel(levelIndex);
            Next();
        }

        public void Quit()
        {
            // save
            Debug.Log("It was quit command");
            Application.Quit();
        }
    }
}
