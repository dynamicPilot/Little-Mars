using UnityEngine;

namespace LittleMars.UI.MainMenu
{
    public class SettingsParametersUI : MonoBehaviour
    {
        [SerializeField] SettingsParameterUI _musicVolume;
        [SerializeField] SettingsParameterUI _totalVolume;
        [SerializeField] LangParameterUI _langUI;

        public void SetParameters(PlayerConfig config)
        {
            Debug.Log($"Player Config : musicVolume {config.MusicVolume}, is Music on {config.IsMusicOn}.");
            Debug.Log($"Player Config : soundsVolume {config.SoundsVolume}, is Sounds on {config.IsSoundsOn}.");
            _musicVolume.SetParameter(config.MusicVolume, config.IsMusicOn);
            _totalVolume.SetParameter(config.SoundsVolume, config.IsSoundsOn);
            _langUI.SetIndex(config.Lang);
        }

        public void ResetToDefaults()
        {
            _musicVolume.ToDefault();
            _totalVolume.ToDefault();
            _langUI.ToDefault();
        }

        public bool NeedSave()
        {
            return _musicVolume.NeedSave() && _totalVolume.NeedSave() && _langUI.NeedSave();
        }
    }
}
