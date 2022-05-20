using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public enum LANG { eng, rus }

    [SerializeField] private bool loadlLevelFromSaveData = true;

    [SerializeField] private LANG lang;
    public LANG Lang { get { return lang; }}

    [SerializeField] private LevelInfo levelInfo;

    [Header("Scripts")]
    [SerializeField] private LevelControl levelControl;

    private GameDataSaveAndLoad gameDataSaveAndLoad;
    private SettingsControl settingsControl;
    private void Start()
    {
        gameDataSaveAndLoad = GameDataSaveAndLoad.Instance;
        settingsControl = SettingsControl.Instance;

        SetSettings();
        SetLevelInfoAndLoadLevel();
    }

    void SetLevelInfoAndLoadLevel()
    {
        if (loadlLevelFromSaveData) levelInfo = gameDataSaveAndLoad.CurrentLevel;
        levelControl.SetLevel(levelInfo);
    }

    void SetSettings()
    {
        if (loadlLevelFromSaveData) settingsControl.SetSoundsSettings();

        lang = settingsControl.GetLanguage();
        //Debug.Log("GameMaster: language is " + lang);
    }

    public void EndLevel(bool needLoadNext)
    {
        // add level to completed levels, change current level, save data, load next scene
        gameDataSaveAndLoad.SaveCurrentLevel(needLoadNext, !needLoadNext);

    }
}