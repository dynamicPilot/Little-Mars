using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private ChooseLevelUI chooseLevelUI;
    [SerializeField] private SettingsPanelUI settingsPanelUI;
    [SerializeField] private GameObject creditsPanel;


    private GameDataSaveAndLoad gameDataSaveAndLoad;
    private LoadNextScene loadNextScene;
    private SettingsControl settingsControl;

    private void Awake()
    {
        loadNextScene = GetComponent<LoadNextScene>();
    }

    private void Start()
    {
        gameDataSaveAndLoad = GameDataSaveAndLoad.Instance;
        settingsControl = SettingsControl.Instance;

        SetSettings();
}

    void SetSettings()
    {
        settingsControl.SetSoundsSettings();
    }

    public void ContinueOrNewGame()
    {
        loadNextScene.LoadSceneByName(gameDataSaveAndLoad.CurrentLevel.SceneName);
    }

    public void OpenSettings()
    {
        settingsPanelUI.gameObject.SetActive(true);
        settingsPanelUI.OpenPanel();
        mainMenuUI.ClosePanel();
    }

    public void OpenChooseLevel()
    {
        chooseLevelUI.enabled = true;
        chooseLevelUI.gameObject.SetActive(true);
        mainMenuUI.ClosePanel();
    }

    public void CloseChooseLevel()
    {
        chooseLevelUI.enabled = false;
        chooseLevelUI.gameObject.SetActive(false);
        mainMenuUI.OpenPanel();
    }

    public void OpenCreditsPanel()
    {
        creditsPanel.SetActive(true);
        mainMenuUI.ClosePanel();
    }

    public void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
        mainMenuUI.OpenPanel();
    }

    public void OpenAd()
    {
        mainMenuUI.ClosePanel();
    }

    public void CloseAd()
    {
        mainMenuUI.OpenPanel();
    }

    public void QuitGame()
    {
        //Debug.Log("QUIT");
        Application.Quit();
    }
}
