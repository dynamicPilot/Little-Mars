using UnityEngine;

public class AskForAdNotificationPanel : NotificationPanelUI
{
    [SerializeField] private InterstitialAds interstitialAds;

    private string nextSceneState = "";

    public void SetNextSceneState(string newState)
    {
        nextSceneState = newState;
    }

    public void LoadAd()
    {
        //interstitialAds.ShowAds(nextSceneState);
    }

    public void SkipAd()
    {
        //GameDataSaveAndLoad.Instance.LoadSceneAfterAd(nextSceneState);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }
}
