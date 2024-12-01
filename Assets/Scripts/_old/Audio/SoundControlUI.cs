using UnityEngine;

public class SoundControlUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private ParameterOnOffButton musicIsOnOffButton;
    [SerializeField] private ParameterOnOffButton soundsIsOnOffButton;

    private SettingsControl settingsControl;

    private void Start()
    {
        settingsControl = SettingsControl.Instance;
    }

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
        musicIsOnOffButton.SetValue(settingsControl.GetIsMusicOn());
        soundsIsOnOffButton.SetValue(settingsControl.GetIsSoundsOn());
    }

    public void ParameterButtonAction(string sliderType)
    {
        if (sliderType == "music")
        {
            //Debug.Log("SoundControlUI: change musicOn to " + musicIsOnOffButton.GetValue());
            settingsControl.PauseUnpauseRadio(musicIsOnOffButton.GetValue());
        }
        else if (sliderType == "sounds")
        {
            //Debug.Log("SoundControlUI: change soundsOn to " + soundsIsOnOffButton.GetValue());
            settingsControl.PauseUnpauseSounds(soundsIsOnOffButton.GetValue());
        }
    }

    public void ClosePanel()
    {
        settingsControl.SetNewSoundSettingsForLevel(musicIsOnOffButton.GetValue(), soundsIsOnOffButton.GetValue());
    }
}
