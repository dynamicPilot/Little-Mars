using LittleMars.Common.Signals;
using LittleMars.UI.Windows;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.WindowManagers
{
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

            if (needCloseSender) CloseWindow(arg.SenderId, arg.NextSenderState);
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

            GameObject.Destroy(window);
        }


    }
}

