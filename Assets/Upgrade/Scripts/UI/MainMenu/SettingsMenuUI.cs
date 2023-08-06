using LittleMars.AudioSystems;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.Localization;
using LittleMars.MainMenus;
using LittleMars.UI.LevelMenus;
using LittleMars.WindowManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace LittleMars.UI.MainMenu
{
    public class SettingsMenuUI : MenuUIWithControls
    {
        [SerializeField] Button _defaultsButton;
        [SerializeField] Button _backButton;

        [Header("UI Elements")]
        [SerializeField] TextTagElement _defaultButtonText;
        [SerializeField] SettingsParametersUI _parameters;

        UISoundSystem _soundSystem;
        SettingsMenu _menu;
        SignalBus _signalBus;

        [Inject]
        public void Constructor(GameMenu gameMenu, SoundsForGameMenuUI sounds, 
            SettingsMenu menu, SignalBus signalBus, UISoundSystem soundSystem)
        {
            _gameMenu = gameMenu;
            _sounds = sounds;
            _soundSystem = soundSystem;
            _signalBus = signalBus;
            _menu = menu;

            Init();
        }
        void Init()
        {
            SetListeners();
            _signalBus.Subscribe<NeedTextUpdateSignal>(UpdateText);
        }

        private void OnDestroy()
        {
            RemoveListeners();
            _signalBus?.TryUnsubscribe<NeedTextUpdateSignal>(UpdateText);
        }

        protected override void SetListeners()
        {
            _defaultsButton.onClick.AddListener(OnResetButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
            base.SetListeners();
        }

        protected override void RemoveListeners()
        {
            _defaultsButton.onClick.RemoveListener(OnResetButtonClick);
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            base.RemoveListeners();
        }

        public override void OnOpenMenu(WindowContext context)
        {
            var config = _menu.GetPlayerConfig();
            if (config == null)
            {
                Debug.Log("Null config");
                NullCongig();
                return;
            }
            _parameters.SetParameters(config);
            UpdateText();
            base.OnOpenMenu(context);
        }

        void OnBackButtonClick()
        {
            var needSave = _parameters.NeedSave();
            if (needSave) _menu.SavePlayerConfig();
            _soundSystem.PlayUISound(Common.UISoundType.quit);
        }

        void OnResetButtonClick()
        {
            _soundSystem.PlayUISound(Common.UISoundType.clickFirst);
            _parameters.ResetToDefaults();
        }

        void NullCongig()
        {
            _gameMenu.OpenWindowById(WindowID.menu_mainMenu, _id, WindowState.hide);
        }

        void UpdateText()
        {
            _defaultButtonText.SetText();
        }
    }
}
