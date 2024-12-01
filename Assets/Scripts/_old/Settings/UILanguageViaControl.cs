using UnityEngine;

public class UILanguageViaControl : UILanguage
{
    [SerializeField] private bool needSetOnEnable = false;
    [SerializeField] private bool needSetOnAwake = false;

    private SettingsControl settingsControl;


    private void Awake()
    {
        if (needSetOnAwake) UpdateText();
    }

    private void OnEnable()
    {
        if (needSetOnEnable)
        {
            UpdateText();
        }
    }

    public void UpdateText()
    {
        if (settingsControl == null)
        {
            settingsControl = SettingsControl.Instance;
        }

        SetTexts(settingsControl.GetLanguage());
    }
}
