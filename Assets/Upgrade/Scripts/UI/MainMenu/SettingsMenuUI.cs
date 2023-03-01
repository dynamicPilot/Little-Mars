using LittleMars.AudioSystems;
using LittleMars.MainMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [SerializeField] SettingsParametersUI _parameters;

        UISoundSystem _soundSystem;
        SettingsMenu _menu;

        [Inject]
        public void Constructor(UISoundSystem soundSystem, SettingsMenu menu)
        {
            _soundSystem = soundSystem;
            _menu = menu;

            Init();
        }
        void Init()
        {
            SetListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        void SetListeners()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
            _defaultsButton.onClick.AddListener(ResetToDefaults);
        }

        void RemoveListeners()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            _defaultsButton.onClick.RemoveListener(ResetToDefaults);
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
            Open();
        }

        void OnBackButtonClick()
        {
            var needSave = _parameters.NeedSave();
            if (needSave) _menu.SavePlayerConfig();
            Close();
        }

        void ResetToDefaults()
        {
            _parameters.ResetToDefaults();
        }
    }
}
