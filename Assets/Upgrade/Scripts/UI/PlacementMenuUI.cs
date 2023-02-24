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
    }
    public class PlacementMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _rotateButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private GameObject _panel;
        
        AudioSystem _audioSystem;
        IPlacement _placement = null;
        SignalBus _signalBus;

        bool _isListenersSet = false;

        private void OnValidate()
        {
            _panel.SetActive(false);
        }

        [Inject]
        public void Constructor(IPlacement placement, SignalBus signalBus, AudioSystem audioSystem)
        {
            _placement = placement;
            _signalBus = signalBus;
            _audioSystem = audioSystem;
            _isListenersSet = false;

            Init();
        }

        private void Init()
        {
            _signalBus.Subscribe<StartBuildingPlacementSignal>(Open);
        }

        private void OnDestroy()
        {
            RemoveListeners();
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

            _acceptButton.onClick.AddListener(Accept);
            _rotateButton.onClick.AddListener(Rotate);
            _removeButton.onClick.AddListener(Remove);

            _isListenersSet = true;
        }

        void Accept()
        {
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

        void RemoveListeners()
        {
            _acceptButton.onClick.RemoveAllListeners();
            _rotateButton.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();
        }
    }
}
