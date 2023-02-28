using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.LevelMenus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.GoalTextMenu
{
    public class GoalTextMenuUI : GameMenuUI
    {
        [Header("OpenButton")]
        [SerializeField] Button _openButton;

        [Header("Text Slot")]
        [SerializeField] TextMeshProUGUI[] _texts;

        GoalTextLevelMenu _goalTextMenu;
        SignalBus _signalBus;
        bool _isTextSet = false;
        protected override void Awake()
        {
            base.Awake();
            _buttons.Add(CommandType.goalsInfo, _openButton);
        }

        [Inject]
        public void Constructor(GoalTextLevelMenu levelMenu, SoundsForGameMenuUI sounds, SignalBus signalBus)
        {
            _gameMenu = levelMenu;
            _goalTextMenu = levelMenu;
            _signalBus = signalBus;
            _sounds = sounds;

            Init();
        }

        public void Init()
        {
            _signalBus.Subscribe<NeedGoalInfoSignal>(OnOpenButtonClicked);
            SetButtons();
        }

        private void OnDestroy()
        {
            RemoveListeners();
            _signalBus?.TryUnsubscribe<NeedGoalInfoSignal>(OnOpenButtonClicked);
        }

        protected override void SetListeners()
        {
            base.SetListeners();
            //_openButton.onClick.AddListener(OnOpenButtonClicked);
            AddCommandToButtonListener(_openButton, CommandType.goalsInfo, false);
        }

        protected override void Close()
        {
            base.Close();
            _sounds.PlaySoundForCommandType(CommandType.quit);
            _openButton.interactable = true;
        }

        void OnOpenButtonClicked()
        {
            if (_isOpen) return;
            _openButton.interactable = false;
            _sounds.PlaySoundForCommandType(CommandType.empty);
            if (!_isTextSet) SetGoalTexts();
            Open();
        }

        private void SetGoalTexts()
        {
            var texts = _goalTextMenu.GetGoalTexts();
            if (texts == null) return;

            _isTextSet = true;
            int index = 0;

            for (int i = 0; i < texts.Length; i++)
            {
                if (i >= _texts.Length)
                {
                    return;
                }

                _texts[i].gameObject.SetActive(true);
                _texts[i].text = texts[i];
                index++;
            }

            if (index < _texts.Length)
            {
                for (int i = index; i < _texts.Length; i++)
                    _texts[i].gameObject.SetActive(false);
            }

        }
    }
}
