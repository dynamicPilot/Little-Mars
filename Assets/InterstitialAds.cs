using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour//, IUnityAdsListener
{
    [SerializeField] string gameID = "4298193";
    [SerializeField] private string placementID = "Interstitial_Android";
    [SerializeField] private bool testMode = true;

    [Header("Loading UI")]
    [SerializeField] private GameObject panel;

    [Header("Options")]
    [SerializeField] private bool needLoadSceneAfterAd = true;

    private string state = "menu";
    private GameDataSaveAndLoad dataSaveAndLoad;
    private RadioControl radioControl;

    private void Start()
    {
        //Advertisement.AddListener(this);
        //Advertisement.Initialize(gameID, testMode);
        dataSaveAndLoad = GameDataSaveAndLoad.Instance;
        radioControl = RadioControl.Instance;
    }
    //IEnumerator StartAdvertisement()
    //{
    //    //Advertisement.AddListener(this);
    //    Advertisement.Initialize(gameID, testMode);

    //    if (panel == null)
    //    {
    //        panel = GameObject.FindGameObjectWithTag("AdLoadingPanel");
    //    }

    //    if (panel != null) panel.SetActive(true);
        
    //    // for test --- rewrite it
    //    yield return null;
    //    //while (!Advertisement.IsReady(placementID))
    //    //{
    //    //    
    //    //}

    //    Advertisement.Show(placementID);
    //}

    //public void ShowAds(string newState = "menu")
    //{
    //    state = newState;
    //    StartCoroutine(StartAdvertisement());
    //    //Advertisement.Show(placementID);
    //}

    //public void QuitAdsWhileLoadingTooLong()
    //{
    //    StopAllCoroutines();
    //    if (panel != null) panel.SetActive(false);
    //    radioControl.ContinuePlayingRadio();
    //    if (needLoadSceneAfterAd && dataSaveAndLoad!= null) dataSaveAndLoad.LoadSceneAfterAd(state);
    //}

    //public void OnUnityAdsDidError(string message)
    //{
    //    if (panel != null) panel.SetActive(false);
    //    radioControl.ContinuePlayingRadio();

    //    //if (dataSaveAndLoad == null) dataSaveAndLoad = GameDataSaveAndLoad.Instance;
    //    if (needLoadSceneAfterAd && dataSaveAndLoad != null) dataSaveAndLoad.LoadSceneAfterAd(state);
    //}

    //public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    //{
    //    radioControl.ContinuePlayingRadio();

    //    //if (dataSaveAndLoad == null) dataSaveAndLoad = GameDataSaveAndLoad.Instance;
    //    if (needLoadSceneAfterAd && dataSaveAndLoad != null) dataSaveAndLoad.LoadSceneAfterAd(state);
    //}

    //public void OnUnityAdsDidStart(string placementId)
    //{
    //    if (panel != null) panel.SetActive(false);
    //    radioControl.PausePlayingRadio();
    //}

    //public void OnUnityAdsReady(string placementId)
    //{

    //}
}
