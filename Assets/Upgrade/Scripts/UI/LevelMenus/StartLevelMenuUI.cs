using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.LevelMenus
{
    public class StartLevelMenuUI : LevelMenuUI
    {
        [SerializeField] private Button _startButton;

        [Header("Goals Display Slot")]
        [SerializeField] private GoalDisplayUI[] _displayUIs;

        protected override void Awake()
        {
            base.Awake();
            _buttons.Add(CommandType.start, _startButton);
        }
        protected override void SetListeners()
        {
            base.SetListeners();
            AddCommandToButtonListener(_startButton, CommandType.start);
        }

        protected override void Init()
        {
            _signalBus.Subscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);
        }

        private void OnStrategiesIsReady(GoalStrategiesIsReadySignal args)
        {
            _signalBus.Unsubscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);

            // set display
            SetMenu();
            SetGoalDisplays(args.Strategies);
            SetButtons();
            Open();
        }

        private void SetGoalDisplays(IGoalDisplayStrategy[] strategies)
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
