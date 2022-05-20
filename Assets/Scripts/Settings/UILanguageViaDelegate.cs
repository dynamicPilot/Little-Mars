using UnityEngine;

public class UILanguageViaDelegate : UILanguage
{
    [Header("Options")]
    [SerializeField] private bool setOnAwake = false;

    private SettingsControl settingsControl;

    private void Awake()
    {
        if (setOnAwake)
        {
            settingsControl = SettingsControl.Instance;
            settingsControl.OnNewLanguage += UpdateText;

            UpdateText(settingsControl.GetLanguageIndex());
        }
    }

    private void OnEnable()
    {
        if (!setOnAwake)
        {
            settingsControl = SettingsControl.Instance;
            settingsControl.OnNewLanguage += UpdateText;

            UpdateText(settingsControl.GetLanguageIndex());
        }

        
    }

    private void OnDisable()
    {
        if (!setOnAwake)
        {
            try
            {
                settingsControl.OnNewLanguage -= UpdateText;
            }
            catch { }
        }

    }

    private void OnDestroy()
    {
        if (setOnAwake)
        {
            try
            {
                settingsControl.OnNewLanguage -= UpdateText;
            }
            catch { }
        }
    }

    void UpdateText(int langIndex)
    {
        SetTexts((GameMaster.LANG) langIndex);
    }
}
