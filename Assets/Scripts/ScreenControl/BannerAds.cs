using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] string gameID = "4298193";
    [SerializeField] private string placementID = "Banner_Android";
    [SerializeField] private bool testMode = true;

    [Header("UI To Move")]
    [SerializeField] private RectTransform menuToMove;
    [SerializeField] private float botAndTopToMovePosition = 25f;
    [SerializeField] private float initialBotAndTopPosition = 0;

    [SerializeField] private RectTransform settingsPanelToMove;
    [SerializeField] private float heightToMoveForSettings = 260f;
    [SerializeField] private float initialHeightForSettings = 300f;

    [SerializeField] private List<RectTransform> levelPanelToMove;
    [SerializeField] private float botAndTopMovePositionToLevels = 20f;
    [SerializeField] private float initialBotAndTopPositionToLevels = 0f;

    bool isAdsOn = false;

    IEnumerator StartAdvertisement()
    {
        isAdsOn = false;
        menuToMove.anchoredPosition = new Vector2(menuToMove.anchoredPosition.x, menuToMove.anchoredPosition.y + (initialBotAndTopPosition - menuToMove.anchoredPosition.y));
        settingsPanelToMove.sizeDelta = new Vector2(settingsPanelToMove.rect.width, initialHeightForSettings);
        SetAllLevelsToMove();

        Advertisement.Initialize(gameID, testMode);

        while (!Advertisement.IsReady(placementID))
            yield return null;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementID);

        // move UI
        isAdsOn = true;
        menuToMove.anchoredPosition = new Vector2(menuToMove.anchoredPosition.x, menuToMove.anchoredPosition.y + (botAndTopToMovePosition - menuToMove.anchoredPosition.y));
        settingsPanelToMove.sizeDelta = new Vector2 (settingsPanelToMove.rect.width, heightToMoveForSettings);
        SetAllLevelsToMove();


    }

    private void OnEnable()
    {
        StartCoroutine(StartAdvertisement());
    }

    private void OnDestroy()
    {
        //StopAdvertisement();
        
    }

    public void StopAdvertisement()
    {
        //Debug.Log("BannerAds: stop");
        StopAllCoroutines();

        isAdsOn = false;
        Advertisement.Banner.Hide();
        menuToMove.anchoredPosition = new Vector2(menuToMove.anchoredPosition.x, menuToMove.anchoredPosition.y + (initialBotAndTopPosition - menuToMove.anchoredPosition.y));
        settingsPanelToMove.sizeDelta = new Vector2(settingsPanelToMove.rect.width, initialHeightForSettings);
        SetAllLevelsToMove();

    }

    void SetAllLevelsToMove(int index = -1)
    {
        if (index >= 0)
        {
            if (isAdsOn)
            {
                levelPanelToMove[index].anchoredPosition = new Vector2(levelPanelToMove[index].anchoredPosition.x, levelPanelToMove[index].anchoredPosition.y + (botAndTopMovePositionToLevels - levelPanelToMove[index].anchoredPosition.y));
            }
            else
            {
                levelPanelToMove[index].anchoredPosition = new Vector2(levelPanelToMove[index].anchoredPosition.x, levelPanelToMove[index].anchoredPosition.y + (initialBotAndTopPositionToLevels - levelPanelToMove[index].anchoredPosition.y));
            }
            return;
        }

        foreach (RectTransform rectTransform in levelPanelToMove)
        {
            if (isAdsOn)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + (botAndTopMovePositionToLevels - rectTransform.anchoredPosition.y));
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + (initialBotAndTopPositionToLevels - rectTransform.anchoredPosition.y));
            }
        }
    }

    public void AddToLevelsToMove(RectTransform newPage)
    {
        levelPanelToMove.Add(newPage);
        SetAllLevelsToMove(levelPanelToMove.Count - 1);
    }

}
