using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.LevelMenus;
using LittleMars.WindowManagers;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Achievements
{
    public class AchievementDisplayUI : MenuUIWithControls
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

        void Init() => SetButtons();
        //{
            //_signalBus.Subscribe<CallAchivementMenuSignal>(OnCallAchivementMenu);
                  
        //}
        
        private void OnDestroy()
        {
            RemoveListeners();
            //_signalBus?.TryUnsubscribe<CallAchivementMenuSignal>(OnCallAchivementMenu);
        }

        public override void OnOpenMenu(WindowContext context)
        {
            //Debug.Log("OnCallAchivementMenu");
            if (_isOpen) return;

            //Debug.Log("......Check Context");
            if (CheckContext(context))
            {
                //Debug.Log("......Open");
                base.OnOpenMenu(context);
            }
            else
            {
                Debug.Log("......Close");
                _achievementMenu.SetWindowState(_id, WindowState.hide);
            }
        }

        bool CheckContext(WindowContext context)
        {
            if (context == null && context.Indexes == null
                && context.Indexes.Length < 1)
            {
                return false;
            }
            else return CheckStrategy(context.Indexes[0]);
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

        protected override void Close()
        {
            // signal
            _signalBus.TryFire(new WindowIsClosedSignal { MenuState = (int) MenuState.achievement});
            base.Close();
        }
    }
}
