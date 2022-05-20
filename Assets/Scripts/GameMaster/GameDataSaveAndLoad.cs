using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder (-1)]
public class GameDataSaveAndLoad : Singleton<GameDataSaveAndLoad>
{
    [Header("All Levels")]
    [SerializeField] private LevelInfo[] levels;

    public LevelInfo[] Levels { get { return levels; } }

    private List<LevelInfo> completedLevels = new List<LevelInfo>();
    public List<LevelInfo> CompletedLevels { get { return completedLevels; } }

    private LevelInfo currentLevel;
    public LevelInfo CurrentLevel { get { return currentLevel; } }

    private LoadNextScene loadNextScene;
    private int currentLevelIndex = 0;

    [Header("For LO Only")]
    [SerializeField] private AdsControl adsControl;
    private void Awake()
    {
        LoadData();
        loadNextScene = GetComponent<LoadNextScene>();
    }

    public bool IsANewGame()
    {
        if (completedLevels == null)
        {
            return true;
        }
        else if (completedLevels.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetNewCurrentLevel(LevelInfo newCurrentLevel, bool saveAfter = false, bool loadAfter = false)
    {
        currentLevel = newCurrentLevel;

        if (saveAfter)
        {
            SaveData();
        }

        if (loadAfter)
        {
            loadNextScene.LoadSceneByName(currentLevel.SceneName);
        }
    }

    public void LoadSceneAfterAd(string state)
    {
        if (state == "menu")
        {
            loadNextScene.LoadMainManu();
        }
        else
        {
            loadNextScene.LoadSceneByName(currentLevel.SceneName);
        }
    }

    public void SaveCurrentLevel(bool needLoadNext = true, bool needLoadMenu = false)
    {
        AddCurrentLevelToCompletedLevels();

        if (CheckNextLevel())
        {
            if (needLoadMenu)
            {
                SetNewCurrentLevel(levels[currentLevelIndex + 1], true, false);

                if (adsControl.NeedAdAfterLevel(currentLevelIndex))
                {
                    adsControl.ShowAdAfterLevel("menu");
                }
                else
                {
                    loadNextScene.LoadMainManu();
                }               
            }
            else if (needLoadNext)
            {
                if (adsControl.NeedAdAfterLevel(currentLevelIndex))
                {
                    SetNewCurrentLevel(levels[currentLevelIndex + 1], true,false);
                    adsControl.ShowAdAfterLevel("level");
                }
                else
                {
                    SetNewCurrentLevel(levels[currentLevelIndex + 1], true, needLoadNext);
                }
            }
            else
            {
                SetNewCurrentLevel(levels[currentLevelIndex + 1], true, false);
            }
            
        }
        else
        {
            SaveData();

            if (currentLevelIndex + 1 >= levels.Length && adsControl.NeedSpecialAfterLastLevel)
            {
                // last level --> show ads ask panel
                adsControl.ShowAdAfterLevel("menu", true);
                //askForAdPanel.OpenPanel();
                //askForAdPanel.SetNextSceneState("menu");
            }
            else
            {
                loadNextScene.LoadMainManu();
            }
        }
    }

    void AddCurrentLevelToCompletedLevels()
    {
        completedLevels.Add(currentLevel);
    }

    bool CheckNextLevel()
    {
        if (currentLevelIndex + 1 < levels.Length)
        {
            // have level --> check availability
            if (levels[currentLevelIndex + 1].LevelsToBecomeAvailable != null)
            {
                foreach (LevelInfo levelInfo in levels[currentLevelIndex + 1].LevelsToBecomeAvailable)
                {
                    if (!completedLevels.Contains(levelInfo))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveData()
    {
        int[] completedLevelsIndexes = new int[completedLevels.Count];
        int currentLevelIndex = 0;

        int index = 0;

        for(int i = 0; i < levels.Length; i++)
        {
            if (completedLevels.Contains(levels[i]))
            {
                completedLevelsIndexes[index] = i;
                index++;
            }

            if (levels[i] == currentLevel)
            {
                currentLevelIndex = i;
            }
        }

        SaveSystem.SaveData(currentLevelIndex, completedLevelsIndexes);
    }

    void LoadData()
    {
        //Debug.Log("GameDataSaveAndLoad: loading data....");
        GameData data = SaveSystem.LoadData();
        ParseData(data);
    }

    void ParseData(GameData data)
    {
        if (data == null)
        {
            //SetIndexesForLevels();
            currentLevel = levels[0];
            currentLevelIndex = 0;
           
            return;
        }

        try
        {
            currentLevel = levels[data.CurrentLevelIndex];
            currentLevelIndex = data.CurrentLevelIndex;
        }
        catch
        {
            currentLevel = levels[0];
            currentLevelIndex = 0;
        }

        //Debug.Log("GameDataSaveAndLoad: current level is ...." + currentLevel.Number);
        completedLevels.Clear();


        foreach (int level in data.CompletedLevels)
        {
            try
            {
                completedLevels.Add(levels[level]);
            }
            catch
            {
                continue;
            }
        }
    }

    //void SetIndexesForLevels()
    //{
    //    for(int i= 0; i< levels.Length; i++)
    //    {
    //        levels[i].Index = i;
    //    }
    //}
}
