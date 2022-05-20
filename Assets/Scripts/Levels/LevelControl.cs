using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;

    [Header("Levels UI")]
    [SerializeField] private LevelLimitsMenuUI levelLimitsMenuUI;

    [Header("Scripts")]
    [SerializeField] private State state;
    [SerializeField] private MapControl mapControl;
    [SerializeField] private BuildingsControl buildingsControl;
    [SerializeField] private NotificationUI notificationUI;
    [SerializeField] private GameTime gameTime;
    [SerializeField] private StoryControl storyControl;
    [SerializeField] private GameMaster gameMaster;

    private LimitsChecking limitsChecking;
    private ProductionControl productionControl;
    [SerializeField] private BasicLevelLimits winningLimits;
    //public BasicLevelLimits WinningLimits { get{ return winningLimits; } }
    [SerializeField] private BasicLevelLimits endOfLevelLimits;

    private void Awake()
    {
        limitsChecking = GetComponent<LimitsChecking>();
        productionControl = buildingsControl.GetComponent<ProductionControl>();

        productionControl.OnBuildingsTurnOnOff += CheckLevelLimits;
        state.OnProductionChange += CheckLevelLimits;
        gameTime.OnPeriodToNewDay += CheckLevelLimits;

    }

    public void SetLevel(LevelInfo newLevelInfo)
    {
        levelInfo = newLevelInfo;

        // create limits
        winningLimits = new BasicLevelLimits(levelInfo.WinningLimit, gameMaster.Lang);
        endOfLevelLimits = new BasicLevelLimits(levelInfo.EndOfLevelLimit, gameMaster.Lang);
        // create map
        mapControl.CreateMapForLevel(levelInfo);

        // set buildings amount
        buildingsControl.SetBuildingForLevel(levelInfo.Buildings);

        // set start resources
        state.SetStartResourcesForLevel(levelInfo.StartResources, levelInfo.TradePrices);

        // set UI
        levelLimitsMenuUI.SetLevel(levelInfo.Number, winningLimits.AllDescriptions, gameMaster.Lang);

        // set training story
        storyControl.SetStoryForLevel(levelInfo.TrainingStory);

        // show notification
        notificationUI.ShowStartLevelNotification(levelInfo, winningLimits, gameMaster.Lang);

        // set game time
        gameTime.StartLevel();
    }

    public void FromStartToTraining()
    {
        if (storyControl.HaveTrainingStory())
        {
            storyControl.StartTellingStory();
        }
    }


    public void LevelWinning()
    {
        //Debug.Log("LevelControl: WIN");
        notificationUI.ShowWinningNotification(levelInfo, gameMaster.Lang, levelInfo.NeedWaitingBeforeWinning);
    }

 

    public void LevelGameOver(string description)
    {
        //Debug.Log("LevelControl: GAME OVER");
        notificationUI.ShowGameOverNotification(levelInfo.Number, description, gameMaster.Lang);
    }


    public void CheckLevelLimits()
    {
        KeyValuePair<bool, List<int>> winingLimitsResults = limitsChecking.CheckLimits(winningLimits, true);
        KeyValuePair<bool, List<int>> endOfLevelResults = limitsChecking.CheckLimits(endOfLevelLimits);

        if (limitsChecking.CheckLimits(endOfLevelLimits).Key)
        {
            LevelGameOver(MakeDescriptionString(endOfLevelLimits, endOfLevelResults.Value));

        }
        else if (winingLimitsResults.Key)
        {           
            LevelWinning();
        }
        else
        {
            //Debug.Log("LevelControl: continue...");
            if (winingLimitsResults.Value != null)
            {
                ShowLimitNotification(winningLimits, winingLimitsResults.Value);
            }
        }
    }

    void ShowLimitNotification(BasicLevelLimits limits, List<int> indexesToNotification)
    {
        //Debug.Log("LimitChecking: prepaire descriptions");

        notificationUI.ShowReachLimitNotification(MakeDescriptionString(limits, indexesToNotification), gameMaster.Lang);
    }

    public void AddHourToLevelLimitsTimers()
    {
        bool needChecking = limitsChecking.AddHourToLevelLimitsTimers(winningLimits);
        //limitsChecking.AddHourToLevelLimitsTimers(levelInfo.EndOfLevelLimit);

        if (needChecking || winningLimits.IsResourcesNumber || endOfLevelLimits.IsResourcesNumber || winningLimits.IsProductionInSumNumber)
        {
            CheckLevelLimits();
        }
    }


    string MakeDescriptionString(BasicLevelLimits limits, List<int> indexesToNotification)
    {
        string descriptions = "";

        if (indexesToNotification == null)
        {
            return descriptions;
        }

        for (int i = 0; i < indexesToNotification.Count; i++)
        {
            descriptions += limits.AllDescriptions[indexesToNotification[i]];

            if (i != indexesToNotification.Count - 1)
            {
                descriptions += "\n";
            }
        }

        return descriptions;
    }
}
