using LittleMars.Common.Signals;
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

        public MenuReceiver(SignalBus signalBus, MenuSceneControl sceneControl)
        {
            _signalBus = signalBus;
            _sceneControl = sceneControl;
        }

        public override void Next()
        {
            Debug.Log("It was next command");
            _sceneControl.NextSceneType(Common.SceneType.level);
            _signalBus.Fire<EndSceneSignal>();
        }

        public void ToLevel(int levelIndex)
        {
            Debug.Log("It was toLevel command");
            // set next level to playerState
            Next();
        }

        public void Quit()
        {
            Debug.Log("It was quit command");
        }
    }
}
