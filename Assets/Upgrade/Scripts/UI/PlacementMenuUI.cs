using LittleMars.Common.Signals;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public interface IPlacement
    {
        void Rotate();
        void Accept();
        void Remove();
    }
    public class PlacementMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _rotateButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private GameObject _panel;

        IPlacement _placement = null;
        SignalBus _signalBus;
        bool _isListenersSet = false;

        [Inject]
        public void Constructor(IPlacement placement, SignalBus signalBus)
        {
            _placement = placement;
            _signalBus = signalBus;
            _isListenersSet = false;

            _signalBus.Subscribe<StartBuildingPlacementSignal>(Open);
        }

        private void OnValidate()
        {
            _panel.SetActive(false);
        }

        private void OnDestroy()
        {
            _acceptButton.onClick.RemoveAllListeners();
            _rotateButton.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();

            _signalBus.TryUnsubscribe<StartBuildingPlacementSignal>(Open);
        }

        public void Open()
        {
            if (!_isListenersSet) SetListeners();
            _panel.SetActive(true);
        }

        private void Close()
        {
            _panel.SetActive(false);
        }

        private void SetListeners()
        {
            if (_isListenersSet || _placement == null) return;

            _acceptButton.onClick.AddListener(_placement.Accept);
            _acceptButton.onClick.AddListener(Close);

            _rotateButton.onClick.AddListener(_placement.Rotate);

            _removeButton.onClick.AddListener(_placement.Remove);
            _removeButton.onClick.AddListener(Close);

            _isListenersSet = true;
        }
    }
}
