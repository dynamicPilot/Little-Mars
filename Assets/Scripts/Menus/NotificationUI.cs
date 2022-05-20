using System.Collections;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private NotificationPanelUI reachLimitNotification;
    [SerializeField] private WinningNotificationPanelUI winningNotification;
    [SerializeField] private GameOverNotificationPanelUI gameOverNotification;
    [SerializeField] private LevelStartNotificationPanelUI levelStartNotification;
    [SerializeField] private AskForAdNotificationPanel askForAdNotificationPanel;

    [Header("Settings")]
    [SerializeField] private float waitingTime = 3f;

    private bool coroutineIsRunning = false;

    public void ShowAskForAdNotificationPanel(UIFullLabel headerLabels, UIFullLabel detailsLabels, GameMaster.LANG language, string newState)
    {
        askForAdNotificationPanel.SetHeaderReadyText(headerLabels.GetLabelByLang(language).Label);
        askForAdNotificationPanel.SetText(detailsLabels.GetLabelByLang(language).Label);

        askForAdNotificationPanel.SetNextSceneState(newState);
        askForAdNotificationPanel.OpenPanel();
    }

    public void ShowReachLimitNotification(string limitDescription, GameMaster.LANG language)
    {
        reachLimitNotification.SetHeaderText(language);
        reachLimitNotification.SetText(limitDescription, true);
    }

    public void ShowWinningNotification(LevelInfo levelInfo, GameMaster.LANG language, bool needWaiting = false)
    {
        if (needWaiting)
        {
            StartCoroutine(WaitingTillWining(levelInfo, language));
        }
        else
        {
            winningNotification.SetWinningLevel(levelInfo, language);
        }
        
    }

    public void ShowGameOverNotification(int levelNumber, string description, GameMaster.LANG language)
    {
        if (!coroutineIsRunning)
        {
            gameOverNotification.SetHeaderText(language);
            gameOverNotification.SetText(description, true);
        }
    }

    public void ShowStartLevelNotification(LevelInfo levelInfo, BasicLevelLimits levelLimits, GameMaster.LANG language)
    {
        coroutineIsRunning = false;
        levelStartNotification.SetNewLevel(levelInfo, levelLimits, language);
    }

    IEnumerator WaitingTillWining(LevelInfo levelInfo, GameMaster.LANG language)
    {
        coroutineIsRunning = true;
        yield return new WaitForSeconds(waitingTime);
        coroutineIsRunning = false;
        winningNotification.SetWinningLevel(levelInfo, language);
    }
}
