using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class GameOverLevelMenuUI : LevelMenuUI
    {
        [Header("Buttons")]
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _restartButton;

        [Header("Goals Display Slot")]
        [SerializeField] private GoalDisplayUI _displayUI;

        GameOverLevelMenu _gameOverMenu;

        protected override void Awake()
        {
            base.Awake();
            _buttons.Add(CommandType.next, _nextButton);
            _buttons.Add(CommandType.restart, _restartButton);
        }

        [Inject]
        public void Constructor(GameOverLevelMenu levelMenu, SignalBus signalBus, Common.Levels.LevelInfo levelInfo)
        {
            base.BaseConstructor(levelMenu, signalBus, levelInfo);
            _gameOverMenu = levelMenu;
        }

        protected override void Init()
        {
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        protected override void SetListeners()
        {
            base.SetListeners();
            Debug.Log("Add next button");
            AddCommandToButtonListener(_nextButton, CommandType.next);
            Debug.Log("Add restart button");
            AddCommandToButtonListener(_restartButton, CommandType.restart);
        }


        private void OnGameOver(GameOverSignal args)
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);

            // set display
            SetMenu();
            SetGoalDisplays(GetStrategy(args));
            SetButtons();
            Open();
        }

        private IGoalDisplayStrategy GetStrategy(GameOverSignal args)
        {
            return _gameOverMenu.GetStrategy(args.GoalIndex, args.IsStaff);
        }

        private void SetGoalDisplays(IGoalDisplayStrategy strategy)
        {
            _displayUI.gameObject.SetActive(strategy != null);

            if (strategy == null) return;
            _displayUI.SetSlot(strategy);
        }
    }
}
