﻿using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.LevelMenus.Addons;
using LittleMars.UI.Tooltip;
using LittleMars.WindowManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class LevelMenuUI : LevelMenuUIWithControls
    {
        [Header("Menu Strategies")]
        [SerializeField] MenuStrategiesBase _menuStrategies;

        [Inject]
        public void Constructor(LevelMenu levelMenu, Common.Levels.LevelInfo levelInfo, 
            SoundsForGameMenuUI sounds)
        {
            base.BaseConstructor(levelMenu,levelInfo, sounds);
        }

        protected override void Init() => SetButtons();

        public override void OnOpenMenu(WindowContext context)
        {
            if (_menuStrategies != null) _menuStrategies.Set(_levelMenu.GetStrategies());
            SetMenu();
            base.OnOpenMenu(context);
        }

    }
}
