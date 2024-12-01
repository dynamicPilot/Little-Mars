using UnityEngine;

public class SettingsPanelUI : MonoBehaviour
{
    [Header("UI Scripts")]
    [SerializeField] private ParameterSlider musicVolumeSlider;
    [SerializeField] private ParameterSlider soundsVolumeSlider;
    [SerializeField] private ParameterSlider scaleForUISlider;
    [SerializeField] private ParameterOnOffButton musicIsOnOffButton;
    [SerializeField] private ParameterOnOffButton soundsIsOnOffButton;
    [SerializeField] private LanguageDropdown languageDropdown;

    [Header("UI Scale Borders")]
    [SerializeField] private int minScaleBorder = 5;
    [SerializeField] private int maxScaleBorder = 20;

    private SettingsControl settingsControl;

    public void OpenPanel()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (settingsControl == null)
        {
            settingsControl = SettingsControl.Instance;
        }

        // set sliders
        musicVolumeSlider.SetSlider(settingsControl.GetMusicVolume());
        musicIsOnOffButton.SetValue(settingsControl.GetIsMusicOn());
        musicVolumeSlider.IsInteractableSlider(musicIsOnOffButton.GetValue());
        
        soundsVolumeSlider.SetSlider(settingsControl.GetSoundsVolume());
        soundsIsOnOffButton.SetValue(settingsControl.GetIsSoundsOn());
        soundsVolumeSlider.IsInteractableSlider(soundsIsOnOffButton.GetValue());

        scaleForUISlider.SetMinAndMax(minScaleBorder, maxScaleBorder);
        scaleForUISlider.SetSlider(settingsControl.GetUIScale());

        languageDropdown.SetDropdownOptions(settingsControl.GetLanguage());

    }

    public void ApplySettings()
    {
        settingsControl.SetNewSettings(musicIsOnOffButton.GetValue(), musicVolumeSlider.GetValue(), soundsIsOnOffButton.GetValue(), soundsVolumeSlider.GetValue(), (int)languageDropdown.GetLanguage(), scaleForUISlider.GetValue());
    }

    public void ResetToDefault()
    {
        settingsControl.ResetToDefault();
        UpdateUI();
    }

    public void ClosePanel()
    {
        settingsControl.ReserRadioIsPaused();
        settingsControl.ResetRadioIsSet();
        settingsControl.SetSoundsSettings();
        settingsControl.ResetLanguageSettings();
        gameObject.SetActive(false);
    }

    public void ParameterButtonAction(string sliderType)
    {
        if (sliderType == "music")
        {
            //Debug.Log("SettingsPanelUI: change musicOn to " + musicIsOnOffButton.GetValue());
            settingsControl.PauseUnpauseRadio(musicIsOnOffButton.GetValue());
            musicVolumeSlider.IsInteractableSlider(musicIsOnOffButton.GetValue());
        }
        else if (sliderType == "sounds")
        {
            //Debug.Log("SettingsPanelUI: change soundsOn to " + soundsIsOnOffButton.GetValue());
            settingsControl.PauseUnpauseSounds(soundsIsOnOffButton.GetValue());
            soundsVolumeSlider.IsInteractableSlider(soundsIsOnOffButton.GetValue());
        }
    }

    public void TryNewLanguage(int dropdownOptionIndex)
    {
        settingsControl.TryNewLanguage(dropdownOptionIndex);
    }
}
