using UnityEngine;

public class PauseMenuUI : NotificationPanelUI
{
    [SerializeField] private SoundControlUI soundControlUI;
    public override void OpenPanel()
    {
        base.OpenPanel();
        soundControlUI.OpenPanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        soundControlUI.ClosePanel();
    }
}
