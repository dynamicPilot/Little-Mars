using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Localization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class SettingsParameterUI : MonoBehaviour
    {
        [SerializeField] TextTagElement _title;
        [SerializeField] Slider _slider;
        [SerializeField] MuteStateButton _muteButton;
        [SerializeField] VolumeGroupType _type;
        
        AudioSystem _audioSystem;
        UISoundSystem _soundSystem;
        SignalBus _signalBus;
        bool _hasChanged;

        [Inject]
        public void Constructor(AudioSystem audioSystem, UISoundSystem soundSystem, SignalBus signalBus)
        {
            _audioSystem = audioSystem;
            _soundSystem = soundSystem;
            _signalBus = signalBus;
            _hasChanged = false;
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

        public void SetParameter(float value, bool isTurnedOn)
        {
            _hasChanged = false;
            UpdateText();
            UpdateVolume(value, isTurnedOn);
        }

        public bool NeedSave() => _hasChanged;

        public void ToDefault()
        {
            var defaultVolume = _audioSystem.ToDefaultAndGetVolume(_type);
            UpdateVolume(defaultVolume, true);
            _hasChanged = true;
        }
        public void OnSliderMove(float value)
        {
            _hasChanged = true;
            _audioSystem.ChangeGroupVolume(value, _type);
        }

        void UpdateVolume(float value, bool isTurnedOn)
        {
            _slider.value = value;
            SliderMode(isTurnedOn);
            MuteButtonMode(isTurnedOn);
        }

        void UpdateText()
        {
            _title.SetText();
        }

        void SliderMode(bool isActive)
        {
            _slider.interactable = isActive;
        }

        void MuteButtonMode(bool isActive)
        {
            _muteButton.SetState((isActive) ? States.on : States.off);
        }

        void OnMuteButtonClick()
        {
            var state = _muteButton.ChangeStateToOpposite();
            _audioSystem.UpdateIsMuteGroup(!state, _type);
            _soundSystem.PlayUISound(UISoundType.clickSecond);
            SliderMode(state);
        }

        void SetListeners()
        {
            _slider.onValueChanged.AddListener(OnSliderMove);
            _muteButton.Button.onClick.AddListener(OnMuteButtonClick);
        }

        void RemoveListeners()
        {
            _slider.onValueChanged.RemoveListener(OnSliderMove);
            _muteButton.Button.onClick.RemoveListener(OnMuteButtonClick);
        }
    }
}
