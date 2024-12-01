using UnityEngine;
using UnityEngine.UI;

public class NotificationPanelUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Text detailsText;
    [SerializeField] private Image image;
    [SerializeField] private Text headerText;

    [Header("Settings")]
    [SerializeField] private int soundIndex;
    [SerializeField] private UIFullLabel headerLabels;
    public UIFullLabel HeaderLabels { get { return headerLabels; } }

    [Header("Options")]
    [SerializeField] private bool needToStopGameTime;
    [SerializeField] private bool needToRestartGameTimeAfterClose;
    [SerializeField] private bool needToHideOnAwake;
    [SerializeField] private bool needSoundOnOpen = false;

    [Header("Scripts")]
    [SerializeField] private UILanguageViaControl uILanguageViaControl;
    public TimeSpeedUI timeSpeedUI;

    private AudioControl audioControl;

    private void Awake()
    {
        if (needToHideOnAwake)
            panel.SetActive(false);

        audioControl = AudioControl.Instance;
    }

    public virtual void SetText(string newDetailsText, bool openAfterSet = false)
    {
        detailsText.text = newDetailsText;

        if (openAfterSet)
            OpenPanel();
    }

    public virtual void SetHeaderReadyText(string newHeaderText, bool openAfterSet = false)
    {
        headerText.text = newHeaderText;

        if (openAfterSet)
            OpenPanel();
    }

    public virtual void SetHeaderText(GameMaster.LANG language, bool openAfterSet = false)
    {
        headerText.text = headerLabels.GetLabelByLang(language).Label;

        if (openAfterSet)
            OpenPanel();
    }

    public virtual void SetImage(Sprite newIcon, bool openAfterSet = false)
    {
        if (image == null)
        {
            return;
        }

        image.enabled = newIcon != null;
        image.sprite = newIcon;

        if (openAfterSet)
            OpenPanel();
    }

    public virtual void OpenPanel()
    {
        panel.SetActive(true);

        if (uILanguageViaControl != null)
        {
            uILanguageViaControl.UpdateText();
        }

        if (needToStopGameTime && timeSpeedUI != null)
        {
            //Time.timeScale = 0f;
            //Debu
            timeSpeedUI.PauseGameTime();
        }

        if (needSoundOnOpen)
        {
            audioControl.PlayNotificationSound(soundIndex);
        }
    }

    public virtual void ClosePanel()
    {
        panel.SetActive(false);
        //Time.timeScale = 1f;
        if (needToRestartGameTimeAfterClose && timeSpeedUI != null)
            timeSpeedUI.UnpauseGameTime();
    }
}
