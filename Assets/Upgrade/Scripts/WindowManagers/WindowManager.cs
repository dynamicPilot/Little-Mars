using LittleMars.UI.WindowManagers;
using LittleMars.UI.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.WindowManagers
{
    // should be local for scene, factory can be project level
    public class WindowManager :IInitializable
    {
        Dictionary<WindowID, GameWindow> _activeMenus;
        WindowFactory _windowFactory;
        RectTransform _canvas;

        public WindowManager(WindowFactory windowFactory, RectTransform canvas)
        {
            _activeMenus = new Dictionary<WindowID, GameWindow>();
            _windowFactory = windowFactory;
            _canvas = canvas;
        }

        public bool TryOpenMenu(WindowID id)
        {
            return true;
        }

        void OpenMenu()
        {

        }

        void CreateMenu()
        {

        }

        public void Initialize()
        {
            _windowFactory.Create(WindowID.menu_levels, _canvas);
        }
    }
}

