using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.LevelMenus;
using LittleMars.UI.LevelMenus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class MainMenuUI : GameMenuUI
    {
        [Header("Buttons With Commands")]
        [SerializeField] Button _quitButton;
        [Header("No Commands Buttons")]
        [SerializeField] Button[] _changeStateButtons;

        protected override void Awake()
        {
            base.Awake();
            _isOpen = true;
            _buttons.Add(CommandType.quit, _quitButton);
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        [Inject]
        public void Constructor(GameMenu gameMenu, SoundsForGameMenuUI sounds)
        {
            _gameMenu = gameMenu;
            _sounds = sounds;
            Init();
        }

        void Init()
        {
            SetButtons();
        }

        protected override void SetListeners()
        {
            // set listener to next button
            base.SetListeners();
            AddCommandToButtonListener(_quitButton, CommandType.quit, true);
            SetListenersForChangeStateButtons();
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            RemoveListenersForChangeStateButtons();
        }

        void SetListenersForChangeStateButtons()
        {
            foreach (var button in _changeStateButtons)
                button.onClick.AddListener(ChangeState);
        }

        void RemoveListenersForChangeStateButtons()
        {
            foreach (var button in _changeStateButtons)
                button.onClick.RemoveListener(ChangeState);
        }

        void ChangeState()
        {
            _sounds.PlaySoundForCommandType(CommandType.quit);
            if (_isOpen) Close();
            else Open();
        }
    }
}
