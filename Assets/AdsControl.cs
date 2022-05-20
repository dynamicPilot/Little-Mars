using System.Collections.Generic;
using UnityEngine;

public class AdsControl : MonoBehaviour
{
    [SerializeField] private List<int> levelsWhenAdAfter;
    [SerializeField] private bool needSpecialAfterLastLevel = true;
    public bool NeedSpecialAfterLastLevel { get { return needSpecialAfterLastLevel; } }

    [Header("UI")]
    [SerializeField] private NotificationUI notificationUI;

    [Header("UI Element Settings For Final")]
    [SerializeField] private UIFullLabel finalHeaderLabel;
    [SerializeField] private UIFullLabel finalDetailsLabel;

    [Header("UI Element Settings For odinary level")]
    [SerializeField] private UIFullLabel levelHeaderLabel;
    [SerializeField] private UIFullLabel levelDetailsLabel;

    [Header("Scripts")]
    [SerializeField] private GameMaster gameMaster;

    public bool NeedAdAfterLevel(int finishedLevel)
    {
        return levelsWhenAdAfter.Contains(finishedLevel);
    }

    public void ShowAdAfterLevel(string newState, bool isTheLastLevel = false)
    {
        if (isTheLastLevel && needSpecialAfterLastLevel)
        {
            // set ui to final
            notificationUI.ShowAskForAdNotificationPanel(finalHeaderLabel, finalDetailsLabel, gameMaster.Lang, newState);
        }
        else
        {
            // set ui to level
            notificationUI.ShowAskForAdNotificationPanel(levelHeaderLabel, levelDetailsLabel, gameMaster.Lang, newState);
        }
    }
}
