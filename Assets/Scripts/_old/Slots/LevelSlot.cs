using UnityEngine;
using UnityEngine.UI;

public class LevelSlot : BasicSlot
{
    private LevelInfo levelInfo;

    [Header("UI")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image checkmark;

    [Header("Settings")]
    [SerializeField] private Color[] colorsForCheckmark;
    [SerializeField] private UIFullLabel prefixes;

    private int colorIndex;
    private GameDataSaveAndLoad gameDataSaveAndLoad;
    private AudioControl audioControl;
    private SettingsControl settingsControl;

    private void Awake()
    {
        gameDataSaveAndLoad = GameDataSaveAndLoad.Instance;
        audioControl = AudioControl.Instance;
        settingsControl = SettingsControl.Instance;
    }

    public void SetLevelInfo(LevelInfo newLevelInfo, bool isCompleted, bool isAvailable, int newColorIndex)
    {
        levelInfo = newLevelInfo;
        colorIndex = newColorIndex;

        if (isCompleted)
        {
            isAvailable = isCompleted;
        }

        UpdateUI(isCompleted, isAvailable);
    }

    void UpdateUI(bool isCompleted, bool isAvailable)
    {
        if (levelInfo == null)
            return;

        panel.SetActive(isAvailable);
        button.interactable = isAvailable;

        // toggle --> SET AFTER SAVE SYSTEM
        toggle.isOn = isCompleted;

        if (settingsControl == null) settingsControl = SettingsControl.Instance;

        text.text = prefixes.GetLabelByLang(settingsControl.GetLanguage()).Label + " " + levelInfo.Number;
        SetImage(levelInfo.WinningIcon);
        checkmark.color = colorsForCheckmark[colorIndex];
    }

    public void StartLevel()
    {
        if (levelInfo.SceneName != "")
        {
            //Debug.Log("LevelSlot: start level " + levelInfo.Number);

            // set current level, save data, load scene
            audioControl.PlayClickSound();
            gameDataSaveAndLoad.SetNewCurrentLevel(levelInfo, true, true);
        }
    }
}
