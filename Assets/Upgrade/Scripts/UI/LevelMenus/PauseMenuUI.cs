using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.LevelMenus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class PauseMenuUI : GameMenuUI
    {
        [SerializeField] Button _continueButton;
        [SerializeField] Button _restartButton;
        [SerializeField] PauseAudioSettingsUI _audioUI;

        [Header("Button to Open Pause Menu")]
        [SerializeField] Button _pauseMenuButton;

        PauseLevelMenu _pauseMenu;
        bool _isListenersSet = false;

        [Inject]
        public void Constructor(PauseLevelMenu levelMenu, SoundsForGameMenuUI sounds)
        {
            _pauseMenu = levelMenu;
            _gameMenu = levelMenu;
            _sounds = sounds;

            Init();
        }

        protected override void Awake()
        {
            base.Awake();

            _buttons.Add(CommandType.back, _continueButton);
            _buttons.Add(CommandType.restart, _restartButton);
        }

        void Init()
        {
            _pauseMenuButton.onClick.AddListener(OnPauseMenuButtonClick);
        }

        void OnDestroy()
        {
            _pauseMenuButton.onClick.RemoveAllListeners();
            RemoveListeners();
        }

        protected override void SetListeners()
        {
            base.SetListeners();
            AddCommandToButtonListener(_continueButton, CommandType.back);
            AddCommandToButtonListener(_restartButton, CommandType.restart);

            _isListenersSet = true;
        }

        void OnPauseMenuButtonClick()
        {
            if (!_isListenersSet) base.SetButtons();
            _sounds.PlaySoundForCommandType(CommandType.empty);
            _audioUI.SetParameters();
            Open();
        }

        protected override void Close()
        {
            base.Close();
            if (_audioUI.NeedSave())
                _pauseMenu.SaveSettings();
        }
    }
}
