using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Text continueOrNewGameText;

    [Header("Labels")]
    [SerializeField] private UIFullLabel[] continueOrNewGameLabels;

    //[Header("Settings")]
    private GameDataSaveAndLoad gameDataSaveAndLoad;
    private SettingsControl settingsControl;
    private void Start()
    {
        gameDataSaveAndLoad = GameDataSaveAndLoad.Instance;
        settingsControl = SettingsControl.Instance;
        SetButtonsText();
    }
    public void OpenPanel()
    {
        panel.SetActive(true);
        SetButtonsText();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    void SetButtonsText()
    {
        if (settingsControl == null)
        {
            settingsControl = SettingsControl.Instance;
        }


        if (gameDataSaveAndLoad.IsANewGame())
        {
            continueOrNewGameText.text = continueOrNewGameLabels[0].GetLabelByLang(settingsControl.GetLanguage()).Label;
        }
        else
        {
            continueOrNewGameText.text = continueOrNewGameLabels[1].GetLabelByLang(settingsControl.GetLanguage()).Label;
        }
    }
}
