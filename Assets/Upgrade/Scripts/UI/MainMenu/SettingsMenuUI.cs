using LittleMars.AudioSystems;
using LittleMars.Common.Signals;
using LittleMars.Localization;
using LittleMars.MainMenus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class SettingsMenuUI : MenuUI
    {
        [SerializeField] Button _openButton;
        [SerializeField] Button _defaultsButton;
        [SerializeField] Button _backButton;

        [Header("UI Elements")]
        [SerializeField] TextTagElement _defaultButtonText;
        [SerializeField] SettingsParametersUI _parameters;

        UISoundSystem _soundSystem;
        SettingsMenu _menu;
        SignalBus _signalBus;

        [Inject]
        public void Constructor(UISoundSystem soundSystem, SettingsMenu menu,SignalBus signalBus)
        {
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

        void SetListeners()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
            _defaultsButton.onClick.AddListener(OnResetButtonClick);
        }

        void RemoveListeners()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            _defaultsButton.onClick.RemoveListener(OnResetButtonClick);
        }

        void OnOpenButtonClick()
        {
            _soundSystem.PlayUISound(Common.UISoundType.clickFirst);

            var config = _menu.GetPlayerConfig();
            if (config == null)
            {
                Debug.Log("Null config");
                Close();
            }
            _parameters.SetParameters(config);
            UpdateText();
            Open();
        }

        void OnBackButtonClick()
        {
            var needSave = _parameters.NeedSave();
            if (needSave) _menu.SavePlayerConfig();
            _soundSystem.PlayUISound(Common.UISoundType.quit);
            Close();
        }

        void OnResetButtonClick()
        {
            _soundSystem.PlayUISound(Common.UISoundType.clickFirst);
            _parameters.ResetToDefaults();
        }

        void UpdateText()
        {
            _defaultButtonText.SetText();
        }
    }
}
