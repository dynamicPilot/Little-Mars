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
    public class GameOverLevelMenuUI : LevelMenuUIWithControls
    {
        [Header("Goals Display Slot")]
        [SerializeField] private GoalDisplayUI _displayUI;

        GameOverLevelMenu _gameOverMenu;

        [Inject]
        public void Constructor(GameOverLevelMenu levelMenu, Common.Levels.LevelInfo levelInfo,
            SoundsForGameMenuUI sounds)
        {
            base.BaseConstructor(levelMenu,levelInfo, sounds);
            _gameOverMenu = levelMenu;
        }

        protected override void Init() => SetButtons();

        public override void OnOpenMenu(WindowContext context)
        {
            SetMenu();
            SetGoalDisplays(GetStrategy(context));
            base.OnOpenMenu(context);
        }

        bool CheckContext(WindowContext context)
        {
            return (context != null && context.Indexes != null
                && context.Indexes.Length > 1);
        }


        IGoalDisplayStrategy GetStrategy(WindowContext context)
        {
            if (!CheckContext(context)) return null;

            var isStaff = context.Indexes[1] == 1;
            return _gameOverMenu.GetStrategy(context.Indexes[0], isStaff);
        }

        void SetGoalDisplays(IGoalDisplayStrategy strategy)
        {
            _displayUI.gameObject.SetActive(strategy != null);

            if (strategy == null) return;
            _displayUI.SetSlot(strategy);
        }
    }
}
