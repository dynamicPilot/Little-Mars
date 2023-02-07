using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.LevelMenus;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Achievements
{
    public class AchievementDisplayUI : GameMenuUI
    {
        //[SerializeField] private GameObject _panel;
        [SerializeField] private GoalDisplayUI _displayUI;
        //[SerializeField] private Button _quitButton;

        SignalBus _signalBus;
        AchievementDisplayLevelMenu _achievementMenu;

        [Inject]
        public void Constructor(SignalBus signalBus, AchievementDisplayLevelMenu achievementMenu)
        {
            _signalBus = signalBus;
            _achievementMenu = achievementMenu;
            _levelMenu = achievementMenu;
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

        //override 

        //private void Open()
        //{
        //    Debug.Log("OPEN");
        //    _achievementMenu.Open();
        //    _panel.SetActive(true);
        //    _isOpen = true;
        //}

        //private void Close()
        //{
        //    Debug.Log("CLOSE");
        //    _isOpen = false;
        //    _panel.SetActive(false);
        //    _achievementMenu.Close();
        //}

        private void UpdateGoalDisplay(IGoalDisplayStrategy strategy)
        {
            _displayUI.SetSlot(strategy);
        }
    }
}
