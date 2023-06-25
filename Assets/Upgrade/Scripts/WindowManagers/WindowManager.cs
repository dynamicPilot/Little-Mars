using LittleMars.Common.Signals;
using LittleMars.UI.Windows;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.WindowManagers
{

    public class LevelWindowControl : IInitializable
    {
        SignalBus _signalBus;

        public LevelWindowControl(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReadySignal);
        }

        void OnStrategiesIsReadySignal()
        {
            _signalBus.Unsubscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReadySignal);
            _signalBus.Fire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_startMenu,
                SenderId = -1,
                NextSenderState = 0
            });
        }
    }

    // should be local for scene, factory can be project level
    public class WindowManager : IInitializable
    {
        Dictionary<int, GameWindow> _windows;
        WindowFactory _windowFactory;
        SceneWindows _sceneWindows;
        SignalBus _signalBus;

        public WindowManager(WindowFactory windowFactory, SceneWindows sceneWindows, SignalBus signalBus)
        {
            _windows = new Dictionary<int, GameWindow>();
            _windowFactory = windowFactory;
            _sceneWindows = sceneWindows;
            _signalBus = signalBus;

        }
        public void Initialize()
        {
            _signalBus.Subscribe<OpenWindowByIdSignal>(OnOpenWindowById);
            _signalBus.Subscribe<WindowStateByIdSignal>(OnWindowStateById);
            CreateStartSceneWindows();
        }

        void CreateStartSceneWindows()
        {
            for (int i = 0; i < _sceneWindows.StartWindows.Length; i++) 
                CreateWindow((int)_sceneWindows.StartWindows[i]);
        }

        void OnOpenWindowById(OpenWindowByIdSignal arg)
        {
            bool needCloseSender = (_windows.ContainsKey(arg.Id)) ? 
                OpenWindow(arg.Id) : CreateWindow(arg.Id);

            if (needCloseSender && arg.NextSenderState != (int) WindowState.show) 
                CloseWindow(arg.SenderId, arg.NextSenderState);
        }

        void OnWindowStateById(WindowStateByIdSignal arg)
        {
            CloseWindow(arg.Id, arg.SenderState);
        }

        bool OpenWindow(int id)
        {
            var window = _windows[id];
            if (window != null) return window.Open();
            return false;
        }

        bool CreateWindow(int id)
        {
            var window = _windowFactory.Create((WindowID)id, _sceneWindows.Canvas);
            // check for duplicates
            _windows[id] = window;
            return OpenWindow(id);
        }

        void CloseWindow(int id, int nextState)
        {
            if (id < 0) return;
            if (!_windows.ContainsKey(id)) return;

            if ((WindowState)nextState == WindowState.hide) HideWindow(id);
            else DeleteWindow(id);
        }

        void HideWindow(int id)
        {
            _windows[id].Close();
        }

        void DeleteWindow(int id)
        {
            HideWindow(id);
            var window = _windows[id];
            _windows.Remove(id);

            GameObject.Destroy(window.gameObject);
        }


    }
}

