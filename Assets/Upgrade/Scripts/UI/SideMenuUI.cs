using LittleMars.Common;
using LittleMars.Common.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public class SideMenuUI : MonoBehaviour
    {
        [SerializeField] private MenuInitType _initType;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _panel;

        SignalBus _signalBus;
        bool _isOpened = false;
        bool _isInit = false;
        private void Start()
        {
            _isOpened = false;

            _openButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveListener(Open);
            _closeButton.onClick.RemoveListener(Close);
        }

        [Inject]
        public void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _isInit = false;
        }

        private void Open()
        {
            if (_isOpened) return;

            InitMenu();
            _isOpened = true;
            _panel.SetActive(true);
        }

        private void Close()
        {
            if (!_isOpened) return;

            _isOpened = false;
            _panel.SetActive(false);
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
    }
}
