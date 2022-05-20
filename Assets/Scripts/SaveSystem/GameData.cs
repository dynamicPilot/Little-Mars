using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private int currentLevelIndex;
    public int CurrentLevelIndex { get { return currentLevelIndex; } }

    private int[] completedLevels;
    public int[] CompletedLevels { get { return completedLevels; } }

    public GameData(int newCurrentLevelIndex, int[] newCompletedLevels)
    {
        currentLevelIndex = newCurrentLevelIndex;
        completedLevels = newCompletedLevels;
    }
}
