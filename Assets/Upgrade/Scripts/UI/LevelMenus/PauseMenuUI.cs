using LittleMars.Common;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class PauseMenuUI : GameMenuUI
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _muteMusicButton;
        [SerializeField] private Button _muteAllButton;

        [Header("Button to Open Pause Menu")]
        [SerializeField] private Button _pauseMenuButton;

        bool _isListenersSet = false;
        protected override void Awake()
        {
            base.Awake();

            _buttons.Add(CommandType.back, _continueButton);
            _buttons.Add(CommandType.restart, _restartButton);
            _buttons.Add(CommandType.muteMusic, _muteMusicButton);
            _buttons.Add(CommandType.muteAll, _muteAllButton);
        }

        [Inject]
        public void Constructor(LevelMenu levelMenu)
        {
            _levelMenu = levelMenu;

            Init();
        }

        private void Init()
        {
            _pauseMenuButton.onClick.AddListener(OnPauseMenuButtonClick);

        }

        private void OnDestroy()
        {
            _pauseMenuButton.onClick.RemoveAllListeners();
            RemoveListeners();
        }

        protected override void SetListeners()
        {
            base.SetListeners();
            AddCommandToButtonListener(_continueButton, CommandType.back);
            AddCommandToButtonListener(_restartButton, CommandType.restart);
            AddCommandToButtonListener(_muteMusicButton, CommandType.muteMusic, false);
            AddCommandToButtonListener(_muteAllButton, CommandType.muteAll, false);

            _isListenersSet = true;
        }

        private void OnPauseMenuButtonClick()
        {
            if (!_isListenersSet) base.SetButtons();
            Open();
        }
    }
}
