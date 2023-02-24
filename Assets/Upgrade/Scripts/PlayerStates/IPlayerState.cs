using System;
using Zenject;

namespace LittleMars.PlayerStates
{
    public interface IPlayerState
    {
        bool NeedSave();
        int GetLevelNumber();
        int[] GetCompletedLevels();
        void ToNextLevel();
        void SetNextLevel(int levelIndex);
        void AutoNextLevel();
        void SaveCurrentLevelAsCompleted();

        bool CheckNextLevelAndGetCheck();
    }
}
