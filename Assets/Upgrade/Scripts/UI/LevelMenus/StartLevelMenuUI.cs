using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.UI.GoalDisplays;
using LittleMars.WindowManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class StartLevelMenuUI : LevelMenuUIWithControls
    {
        [Header("Goals Display Slot")]
        [SerializeField] GoalDisplayUI[] _displayUIs;

        [Inject]
        public void Constructor(LevelMenu levelMenu, Common.Levels.LevelInfo levelInfo, 
            SoundsForGameMenuUI sounds)
        {
            base.BaseConstructor(levelMenu,levelInfo, sounds);
        }

        protected override void Init() => SetButtons();

        public override void OnOpenMenu(WindowContext context)
        {
            SetGoalDisplays(_levelMenu.GetStrategies());
            SetMenu();
            base.OnOpenMenu(context);
        }

        void SetGoalDisplays(IGoalDisplayStrategy[] strategies)
        {
            int index = 0;
            for(int i = 0; i < strategies.Length; i++)
            {
                if (i >= _displayUIs.Length) return;

                _displayUIs[i].gameObject.SetActive(true);
                _displayUIs[i].SetSlot(strategies[i]);
                index++;
            }

            if (index < _displayUIs.Length)
            {
                for(int i = index; i< _displayUIs.Length; i++)
                    _displayUIs[i].gameObject.SetActive(false);
            }
        }

    }
}
