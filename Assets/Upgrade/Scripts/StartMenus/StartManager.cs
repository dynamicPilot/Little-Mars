using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.StartMenus
{
    public class StartManager : IInitializable
    {
        readonly SignalBus _signalBus;

        bool _isLoaded;
        bool _isLangSet;
        public StartManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _isLoaded = false;
            _isLangSet = false;
        }

        public void Initialize()
        {
            
            _signalBus.Subscribe<DataIsLoadedSignal>(OnLoadingIsReady);
            _signalBus.Subscribe<ConfigIsLoadedSignal>(OnLangIsSet);

            StartGame();
        }

        void StartGame()
        {
            Debug.Log("StartGame");
            _signalBus.Fire<StartLoadingSignal>();
        }

        void OnLangIsSet()
        {
            Debug.Log("OnLangIsSet");
            _isLangSet = true;

            _signalBus.Unsubscribe<ConfigIsLoadedSignal>(OnLangIsSet);
            ReadyToEnd();
        }

        void OnLoadingIsReady()
        {
            Debug.Log("OnLoadingIsReady");
            _isLoaded = true;
            _signalBus.Unsubscribe<DataIsLoadedSignal>(OnLoadingIsReady);

            ReadyToEnd();
        }

        void ReadyToEnd()
        {
            if (_isLoaded && _isLangSet)
                EndScene();
        }


        void EndScene()
        {
            Debug.Log("EndScene");
            _signalBus.Fire<EndSceneSignal>();
        }


    }
}
