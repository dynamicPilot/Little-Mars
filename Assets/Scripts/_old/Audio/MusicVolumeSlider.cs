using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeSlider : MonoBehaviour
{
    private SettingsControl settingsControl;

    public void OnAirControl(float value)
    {
        if (settingsControl == null) settingsControl = SettingsControl.Instance;

        settingsControl.InAirMusicVolume(value);
    }
}
