using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelUI : MenuUI
{
    [SerializeField] private List<LevelSlotsPage> allPages;

    [Header("Settings")]
    [SerializeField] private int slotNumberInPageForLandscape = 4;
    [SerializeField] private bool needSwipeSound = true;
    [SerializeField] private int swipeSoundIndex = 2;

    [Header("Scripts")]
    private GameDataSaveAndLoad gameDataSaveAndLoad;
    //[SerializeField] private SwipeDetection swipeDetection;

    private List<List<LevelInfo>> infosDevidedByPages = new List<List<LevelInfo>>();
    private List<LevelSlotsPage> pages = new List<LevelSlotsPage>();
    private int currentPageIndex = 0;
    private bool pagesHaveSet = false;
    private AudioControl audioControl;

    private void Awake()
    {
        pagesHaveSet = false;
        audioControl = AudioControl.Instance;
        gameDataSaveAndLoad = GameDataSaveAndLoad.Instance;
    }
    private void OnEnable()
    {
        //swipeDetection.enabled = true;
        //swipeDetection.OnSwipeToHorizontal += SwipePage;
        SetPages();
    }

    private void OnDisable()
    {
        //try
        //{
        //    swipeDetection.OnSwipeToHorizontal -= SwipePage;
        //    swipeDetection.enabled = false;
        //}
        //catch
        //{

        //}
    }

    public void SetPages()
    {
        if (!pagesHaveSet)
        {
            pagesHaveSet = true;
            DevidedSlots(gameDataSaveAndLoad.Levels);
        }
        
        UpdateUI();
    }

    void DevidedSlots(LevelInfo[] infos)
    {
        int listNumber = 0;
        listNumber = (int) (infos.Length / slotNumberInPageForLandscape);

        if (infos.Length % slotNumberInPageForLandscape > 0)
        {
            listNumber += 1;
        }
        //Debug.Log("ChooseLevelUI: listNumber " + listNumber);

        for (int i = 0; i < listNumber; i++)
        {
            infosDevidedByPages.Add(new List<LevelInfo>());
            for(int j = 0; j < slotNumberInPageForLandscape; j++)
            {
                int infoLevelIndex = i * slotNumberInPageForLandscape + j;

                if (infoLevelIndex < infos.Length)
                {
                    // add level info
                    infosDevidedByPages[i].Add(infos[infoLevelIndex]);
                }
                else
                {
                    // no info left
                    return;
                }
            }
        }
    }

    public override void UpdateUI()
    {
        int index = 0;
        pages.Clear();
        foreach (List<LevelInfo> infoList in infosDevidedByPages)
        {
            if (infoList == null)
            {
                continue;
            }

            if (index < allPages.Count)
            {
                //allPages[index].gameObject.SetActive(false);
                allPages[index].SetSlotsPage(infoList, gameDataSaveAndLoad.CompletedLevels);
            }
            else
            {
                allPages.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<LevelSlotsPage>());
                allPages[index].SetSlotsPage(infoList, gameDataSaveAndLoad.CompletedLevels);
                //bannerAds.AddToLevelsToMove(allPages[index].gameObject.GetComponent<RectTransform>());
            }
            allPages[index].gameObject.SetActive(false);
            pages.Add(allPages[index]);
            index++;
        }

        if (index < allPages.Count)
        {
            for (int i = index; i < allPages.Count; i++)
            {
                allPages[i].gameObject.SetActive(false);
            }
        }


        //currentPageIndex = 0;
        SetCurrentPageAndCurrentPageIndex(0);
    }

    void SetCurrentPageAndCurrentPageIndex(int newIndex = 0)
    {
        // the same index
        if (currentPageIndex == newIndex)
        {
            if (!pages[currentPageIndex].gameObject.activeSelf)
            {
                pages[currentPageIndex].gameObject.SetActive(true);
            }
            return;
        }

        // not the same
        if (newIndex < 0)
        {
            newIndex = pages.Count - 1;
        }

        newIndex %= pages.Count;
        pages[currentPageIndex].gameObject.SetActive(false);
        currentPageIndex = newIndex;
        pages[currentPageIndex].gameObject.SetActive(true);
    }

    public void SwipePage(int direction)
    {
        // left -1, right 1
        SetCurrentPageAndCurrentPageIndex(currentPageIndex + direction);
        if (needSwipeSound) audioControl.PlayClickSound(swipeSoundIndex);
    }
}
