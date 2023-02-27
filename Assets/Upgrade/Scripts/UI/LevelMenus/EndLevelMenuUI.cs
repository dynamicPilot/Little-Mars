using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class EndLevelMenuUI : LevelMenuUI
    {
        [Header("Buttons")]
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _restartButton;

        protected override void Awake()
        {
            base.Awake();
            _buttons.Add(CommandType.next, _nextButton);
            _buttons.Add(CommandType.restart, _restartButton);
        }

        [Inject]
        public void Constructor(LevelMenu levelMenu, SignalBus signalBus, Common.Levels.LevelInfo levelInfo,
            SoundsForGameMenuUI sounds)
        {
            base.BaseConstructor(levelMenu, signalBus, levelInfo, sounds);
        }

        protected override void Init()
        {
            _signalBus.Subscribe<EndGameSignal>(OnEndGame);
        }

        protected override void SetListeners()
        {
            base.SetListeners();
            AddCommandToButtonListener(_nextButton, CommandType.next);
            AddCommandToButtonListener(_restartButton, CommandType.restart);
        }

        void OnEndGame()
        {
            _signalBus.Unsubscribe<EndGameSignal>(OnEndGame);

            SetMenu();
            SetButtons();
            Open();
        }
    }
}
