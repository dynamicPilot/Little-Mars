using LittleMars.Settings;
using System;

namespace LittleMars.Common
{
    public class ColorsCatalogue
    {
        readonly Settings _settings;

        public ColorsCatalogue(Settings settings)
        {
            _settings = settings;
        }

        [Serializable]
        public class Settings
        {
            public ColorsSettings Colors;
        }
    }
}
