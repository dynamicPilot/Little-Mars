using LittleMars.Settings;
using System;
using UnityEngine;

namespace LittleMars.Common.Catalogues
{
    public class LevelsCatalogue
    {
        readonly Settings _settings;

        public LevelsCatalogue(Settings settings)
        {
            _settings = settings;
        }

        public LevelSettings GetLevel(int levelIndex)
        {
            Debug.Log("Level catalogue: try get level by index " + levelIndex);
            return _settings.Levels.GetLevel(levelIndex);
        }
        
        public LevelSettings[] GetLevels() => _settings.Levels.GetLevels();
        public bool HasLevel(int levelIndex) => _settings.Levels.HasLevel(levelIndex);
        public bool HasLevelByNumber(int levelNumber) => _settings.Levels.HasLevel(levelNumber - 1);

        [Serializable]
        public class Settings
        {
            public LevelsSettings Levels;
        }
    }
}
