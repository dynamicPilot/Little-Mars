﻿using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.LevelMenus
{
    public class LevelManager : IInitializable
    {
        SignalBus _signalBus;
        
        public LevelManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartLevelSignal>(StartLevel);
            _signalBus.Subscribe<EndLevelSignal>(EndLevel);
        }

        private void StartLevel()
        {
            if (ReadyToStart())
                StartGame();
        }

        bool ReadyToStart()
        {
            //Debug.Log("LevelManager: Check for tutorial");
            return true;

        }

        void StartGame()
        {
            _signalBus.Fire<StartGameSignal>();
        }

        void EndLevel()
        {
            _signalBus.Fire<NeedSaveDataSignal>();
            if (ReadyToEnd())
                EndScene();
        }

        bool ReadyToEnd()
        {
            //Debug.Log("LevelManager: Check for ads");
            return true;
        }

        void EndScene()
        {
            _signalBus.Fire<EndSceneSignal>();
        }

    }
}
