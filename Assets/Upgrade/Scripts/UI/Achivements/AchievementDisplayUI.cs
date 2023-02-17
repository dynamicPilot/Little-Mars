using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.LevelMenus;
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
        public void Constructor(SignalBus signalBus, AchievementDisplayLevelMenu achievementMenu)
        {
            _signalBus = signalBus;
            _achievementMenu = achievementMenu;
            _gameMenu = achievementMenu;
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
            _signalBus?.TryUnsubscribe<CallAchivementMenuSignal>(OnCallAchivementMenu);
        }

        private void OnCallAchivementMenu(CallAchivementMenuSignal args)
        {
            if (_isOpen) return;

            if (CheckStrategy(args.GoalIndex)) Open();
            else Close();
        }

        private bool CheckStrategy(int index)
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

        private void UpdateGoalDisplay(IGoalDisplayStrategy strategy)
        {
            _displayUI.SetSlot(strategy);
        }
    }
}
