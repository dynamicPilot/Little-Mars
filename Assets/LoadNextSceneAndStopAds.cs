using UnityEngine;

public class LoadNextSceneAndStopAds : LoadNextScene
{
    [SerializeField] private BannerAds bannerAds;

    //public override void CloseAllBeforeLoading()
    //{
    //    Debug.Log("LoadNextSceneAndStopAds: BannerAds it going to be stop");
    //    //base.CloseAllBeforeLoading();
    //    //bannerAds.StopAdvertisement();
    //}
}
