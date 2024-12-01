using UnityEngine;

public class WinningNotificationPanelUI : NotificationPanelUI
{
    [SerializeField] private UIFullLabel prefixes;
    public void SetWinningLevel(LevelInfo levelInfo, GameMaster.LANG language)
    {
        SetImage(levelInfo.WinningIcon);
        SetHeaderText(language);
        SetText(prefixes.GetLabelByLang(language).Label + " " + levelInfo.Number);
        OpenPanel();
    }
    public override void ClosePanel()
    {
        base.ClosePanel();
        //Time.timeScale = 0f;
    }
}
