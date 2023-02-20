using System;
using UnityEngine;

namespace LittleMars.SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public int CurrentLevel { get; private set;}
        public int[] CompletedLevels { get; private set; }

        public PlayerData()
        {
            CurrentLevel = 0;
            CompletedLevels = new int[0];
        }

        public PlayerData(int currentLevel, int[] completedLevels)
        {
            Debug.Log("Create player data " + currentLevel);
            CurrentLevel = currentLevel;
            CompletedLevels = completedLevels;
        }
    }
}
