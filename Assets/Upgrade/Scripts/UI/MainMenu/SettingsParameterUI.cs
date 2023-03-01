using LittleMars.AudioSystems;
using LittleMars.Common;
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
        bool _hasChanged;

        [Inject]
        public void Constructor(AudioSystem audioSystem)
        {
            _audioSystem = audioSystem;
            _hasChanged = false;
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

        public void SetParameter(float value, bool isTurnedOn)
        {
            Debug.Log($"Set {_type}: value {value}, is TurnedOn {isTurnedOn}");
            _slider.value = value;
            _hasChanged = false;
            _title.SetText();
            SliderMode(isTurnedOn);

            _muteButton.SetState((isTurnedOn) ? States.on : States.off);
        }

        public bool NeedSave()
        {
            return _hasChanged;
        }

        void SliderMode(bool isActive)
        {
            _slider.interactable = isActive;
        }

        void OnMuteButtonClick()
        {
            var state = _muteButton.ChangeStateToOpposite();
            _audioSystem.UpdateIsMuteGroup(!state, _type);
        }

        public void OnSliderMove(float value)
        {
            //Debug.Log("On slider move");
            _hasChanged = true;
            _audioSystem.ChangeGroupVolume(value, _type);
        }

        void SetListeners()
        {
            //Debug.Log("set listeners");
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
