using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
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
        void OnBeginDrag();
    }
    public class PlacementMenuUI : MenuUI
    {
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _rotateButton;
        [SerializeField] private Button _removeButton;

        UISoundSystem _audioSystem;
        IPlacement _placement = null;
        SignalBus _signalBus;

        bool _isListenersSet = false;

        private void OnValidate()
        {
            _panel.SetActive(false);
        }

        [Inject]
        public void Constructor(IPlacement placement, SignalBus signalBus, UISoundSystem audioSystem)
        {
            _placement = placement;
            _signalBus = signalBus;
            _audioSystem = audioSystem;
            _isListenersSet = false;

            Init();
        }

        private void Init()
        {
            _signalBus.Subscribe<StartBuildingPlacementSignal>(OnOpen);
            _signalBus.Subscribe<BeginBuildingDragSignal>(OnBeginDrag);
        }

        private void OnDestroy()
        {
            RemoveListeners();
            _signalBus.TryUnsubscribe<StartBuildingPlacementSignal>(OnOpen);
            _signalBus.TryUnsubscribe<BeginBuildingDragSignal>(OnBeginDrag);
        }

        public void OnOpen()
        {
            if (!_isListenersSet) SetListeners();
            Open();
        }

        void SetListeners()
        {
            if (_isListenersSet || _placement == null) return;

            _acceptButton.onClick.AddListener(Accept);
            _rotateButton.onClick.AddListener(Rotate);
            _removeButton.onClick.AddListener(Remove);

            _isListenersSet = true;
        }
        void RemoveListeners()
        {
            _acceptButton.onClick.RemoveAllListeners();
            _rotateButton.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();
        }

        void Accept()
        {
            Debug.Log("Accept");
            _placement.Accept();
            _audioSystem.PlayUISound(UISoundType.clickFirst);
            Close();
        }

        void Rotate()
        {
            _placement.Rotate();
            _audioSystem.PlayUISound(UISoundType.clickThird);
        }

        void Remove()
        {
            _placement.Remove();
            _audioSystem.PlayUISound(UISoundType.destroy);
            Close();
        }

        void OnBeginDrag()
        {
            _placement.OnBeginDrag();
            if (_isOpen) Close();
        }
    }
}
