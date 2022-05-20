using UnityEngine;

public class LevelStartNotificationPanelUI : NotificationPanelUI
{
    [Header("UI Elements")]
    [SerializeField] private LimitDescriptionMenuUI limitDescriptionMenuUI;

    public void SetNewLevel(LevelInfo levelInfo, BasicLevelLimits basicLevelLimits, GameMaster.LANG language)
    {
        SetImage(levelInfo.StartIcon);
        //SetHeaderText(language);
        SetText(HeaderLabels.GetLabelByLang(language).Label + " " + levelInfo.Number);
        limitDescriptionMenuUI.SetUI(basicLevelLimits);
        OpenPanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }
}
