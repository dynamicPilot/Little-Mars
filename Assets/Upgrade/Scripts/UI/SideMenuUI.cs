using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public class SideMenuUI : MenuUI
    {
        [SerializeField] private MenuInitType _initType;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        SignalBus _signalBus;
        UISoundSystem _audioSystem;
        bool _isInit = false;
        private void Start()
        {
            _openButton.onClick.AddListener(OnOpen);
            _closeButton.onClick.AddListener(OnClose);
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveListener(OnOpen);
            _closeButton.onClick.RemoveListener(OnClose);
        }

        [Inject]
        public void Constructor(SignalBus signalBus, UISoundSystem audioSystem)
        {
            _signalBus = signalBus;
            _audioSystem = audioSystem;
            _isInit = false;
        }

        void OnOpen()
        {
            _audioSystem.PlayUISound(UISoundType.clickThird);

            if (_isOpen) return;

            InitMenu();
            SetOpenButtonState(false);
            Open();
        }

        void OnClose()
        {
            _audioSystem.PlayUISound(UISoundType.quit);

            if (!_isOpen) return;
            SetOpenButtonState(true);
            Close();
        }

        private void InitMenu()
        {
            if (_isInit) return;

            _isInit = true;
            _signalBus.Fire(new NeedMenuInitSignal
            {
                Type = _initType
            });
        }

        void SetOpenButtonState(bool state)
        {
            _openButton.gameObject.SetActive(state);
        }
    }
}
