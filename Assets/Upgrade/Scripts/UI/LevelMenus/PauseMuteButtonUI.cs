using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class PauseMuteButtonUI : MonoBehaviour
    {
        [SerializeField] MuteStateButton _button;
        [SerializeField] VolumeGroupType _type;

        AudioSystem _audioSystem;
        UISoundSystem _soundSystem;
        bool _needSave;

        [Inject]
        public void Constructor(AudioSystem audioSystem, UISoundSystem soundSystem)
        {
            _audioSystem = audioSystem;
            _soundSystem = soundSystem;
            _needSave = false;
        }

        private void Start()
        {
            SetListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        public void SetParameters()
        {
            bool isMute = _audioSystem.GetIsMuteForGroup(_type);
            _button.SetState((isMute) ? States.off : States.on);
            _needSave = false;
        }

        public bool NeedSave() => _needSave;

        void OnMuteButtonClick()
        {
            var state = _button.ChangeStateToOpposite();
            _soundSystem.PlayUISound(UISoundType.clickSecond);
            _audioSystem.UpdateIsMuteGroup(!state, _type);
            _needSave = true;
        }

        void SetListeners()
        {
            _button.Button.onClick.AddListener(OnMuteButtonClick);
        }

        void RemoveListeners()
        {
            _button.Button.onClick.RemoveListener(OnMuteButtonClick);
        }
    }
}
