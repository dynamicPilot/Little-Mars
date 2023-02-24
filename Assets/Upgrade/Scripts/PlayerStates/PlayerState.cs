using LittleMars.Common.Catalogues;
using LittleMars.Common.Signals;
using LittleMars.SaveSystem;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.PlayerStates
{
    public class PlayerState : IPlayerState, IInitializable, IDisposable
    {
        readonly LevelsCatalogue _catalogue;
        readonly SignalBus _signalBus;

        int _level;
        int _nextLevel;
        List<int> _completedLevels;
        bool _needSave;
        public PlayerState(LevelsCatalogue catalogue, SignalBus signalBus)
        {
            _catalogue = catalogue;
            _signalBus = signalBus;

            _level = 0;
            _nextLevel = 0;
            _completedLevels = new();
            _needSave = false;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<DataIsLoadedSignal>(OnDataLoaded);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<DataIsLoadedSignal>(OnDataLoaded);
        }

        public bool NeedSave() => _needSave;
        public int GetLevelNumber() => _level;
        public int[] GetCompletedLevels() => _completedLevels.ToArray();
        public void ToNextLevel()
        {
            _level = _nextLevel;
        }

        public void SetNextLevel(int levelIndex)
        {
            _nextLevel = levelIndex;           
        }
        public void AutoNextLevel()
        {
            SetNextLevel(_level + 1);
        }

        public void SaveCurrentLevelAsCompleted()
        {
            if (_completedLevels.Contains(_level)) return;

            _needSave = true;
            _completedLevels.Add(_level);
        }

        void OnDataLoaded(DataIsLoadedSignal args)
        {
            UpdateWithData(args.PlayerData);
        }

        void UpdateWithData(PlayerData data)
        {
            _level = data.CurrentLevel;
            _nextLevel = _level;
            _completedLevels.Clear();
            _completedLevels.AddRange(data.CompletedLevels);
        }

        public bool CheckNextLevelAndGetCheck()
        {
            var hasLevel = _catalogue.HasLevel(_nextLevel);
            _nextLevel = (hasLevel) ? _level : _nextLevel;

            return hasLevel;
        }
    }
}
