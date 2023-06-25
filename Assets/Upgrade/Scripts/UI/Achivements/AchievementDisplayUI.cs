using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.LevelMenus;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Achievements
{
    public class AchievementDisplayUI : GameMenuUI
    {
        [SerializeField] private GoalDisplayUI _displayUI;

        SignalBus _signalBus;
        AchievementDisplayLevelMenu _achievementMenu;

        [Inject]
        public void Constructor(SignalBus signalBus, AchievementDisplayLevelMenu achievementMenu,
            SoundsForGameMenuUI sounds)
        {
            _signalBus = signalBus;
            _achievementMenu = achievementMenu;
            _gameMenu = achievementMenu;
            _sounds = sounds;

            _isOpen = false;

            Init();
        }

        private void Init()
        {
            _signalBus.Subscribe<CallAchivementMenuSignal>(OnCallAchivementMenu);
            SetButtons();         
        }
        
        private void OnDestroy()
        {
            RemoveListeners();
            //_signalBus?.TryUnsubscribe<CallAchivementMenuSignal>(OnCallAchivementMenu);
        }

        void OnCallAchivementMenu(CallAchivementMenuSignal args)
        {
            Debug.Log("OnCallAchivementMenu");
            if (_isOpen) return;

            Debug.Log("......CheckStrategy");
            if (CheckStrategy(args.GoalIndex))
            {
                Debug.Log("......Open");
                Open();
            }
            else
            {
                Debug.Log("......Close");
                Close();
            }
        }

        bool CheckStrategy(int index)
        {
            Debug.Log("Check Strategy " + index);
            var strategy = _achievementMenu.GetDisplayStrategy(index);

            if (strategy == null) return false;
            else
            {
                Debug.Log("Updating strategy");
                UpdateGoalDisplay(strategy);
                return true;
            }           
        }

        void UpdateGoalDisplay(IGoalDisplayStrategy strategy)
        {
            _displayUI.SetSlot(strategy);
        }
    }
}
