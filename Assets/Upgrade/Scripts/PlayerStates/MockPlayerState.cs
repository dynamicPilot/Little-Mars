using LittleMars.Common.Catalogues;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.PlayerStates
{
    public class MockPlayerState : IPlayerState
    {
        readonly LevelsCatalogue _catalogue;

        int _levelIndex;
        int _nextLevelIndex;
        List<int> _completedLevels;
        bool _needSave;

        public MockPlayerState(LevelsCatalogue catalogue)
        {
            _catalogue = catalogue;

            _levelIndex = 0;
            _nextLevelIndex = 0;
            _completedLevels = new();
            _needSave = false;
        }

        public bool NeedSave() => _needSave;
        public int GetLevelNumber() => _levelIndex;
        public int[] GetCompletedLevels() => _completedLevels.ToArray();
        public void ToNextLevel()
        {
            Debug.Log("MockPlayer: to next level " + _nextLevelIndex + " need save " + _needSave);
            _levelIndex = _nextLevelIndex;
        }

        public void SetNextLevel(int levelIndex)
        {
            Debug.Log("MockPlayer: next level is " + levelIndex);
            _nextLevelIndex = levelIndex;
        }

        public void AutoNextLevel()
        {
            SetNextLevel(_levelIndex + 1);
        }

        public void SaveCurrentLevelAsCompleted()
        {
            Debug.Log("MockPlayer: save level " + _levelIndex + " has level ? " + (_completedLevels.Contains(_levelIndex)));
            if (_completedLevels.Contains(_levelIndex)) return;

            Debug.Log("MockPlayer: need save level " + _levelIndex);
            _needSave = true;
            _completedLevels.Add(_levelIndex);
        }

        public bool CheckNextLevelAndGetCheck()
        {
            var hasLevel = _catalogue.HasLevel(_nextLevelIndex);
            _nextLevelIndex = (hasLevel) ? _levelIndex : _nextLevelIndex;

            return hasLevel;
        }
    }
}
