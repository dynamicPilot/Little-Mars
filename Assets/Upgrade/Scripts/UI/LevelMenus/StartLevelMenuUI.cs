using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.UI.GoalDisplays;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class StartLevelMenuUI : LevelMenuUI
    {
        [Header("Buttons")]
        [SerializeField] Button _startButton;
        [SerializeField] Button _infoButton;

        [Header("Goals Display Slot")]
        [SerializeField] GoalDisplayUI[] _displayUIs;


        protected override void Awake()
        {
            base.Awake();
            _buttons.Add(CommandType.start, _startButton);
            _buttons.Add(CommandType.goalsInfo, _infoButton);
        }

        [Inject]
        public void Constructor(LevelMenu levelMenu, SignalBus signalBus, Common.Levels.LevelInfo levelInfo,
            SoundsForGameMenuUI sounds)
        {
            base.BaseConstructor(levelMenu, signalBus, levelInfo, sounds);
        }

        protected override void Init()
        {
            _signalBus.Subscribe<GoalStrategiesIsReadySignal>(OnStrategiesIsReady);
        }

        protected override void SetListeners()
        {
            base.SetListeners();
            AddCommandToButtonListener(_startButton, CommandType.start);
            AddCommandToButtonListener(_infoButton, CommandType.goalsInfo, false);
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
