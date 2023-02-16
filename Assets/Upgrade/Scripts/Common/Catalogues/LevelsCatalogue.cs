using LittleMars.Settings;
using System;

namespace LittleMars.Common.Catalogues
{
    public class LevelsCatalogue
    {
        readonly Settings _settings;

        public LevelsCatalogue(Settings settings)
        {
            _settings = settings;
        }

        public LevelSettings[] GetLevels() => _settings.Levels.GetLevels();

        public bool HasLevel(int levelNumber) => _settings.Levels.HasLevel(levelNumber);

        [Serializable]
        public class Settings
        {
            public LevelsSettings Levels;
        }
    }
}
