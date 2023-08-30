using LittleMars.Settings;
using System;
using UnityEngine;

namespace LittleMars.Common.Catalogues
{
    public class ColorsCatalogue
    {
        readonly Settings _settings;

        public ColorsCatalogue(Settings settings)
        {
            _settings = settings;
        }

        public Color BStateColor(BStates state)
        {
            return _settings.Colors.GetColor((int)state, ColorType.bState);
        }

        public Color ConnectionColor(BuildingType type)
        {
            return _settings.Colors.GetColor((int)type, ColorType.connection);
        }

        [Serializable]
        public class Settings
        {
            public ColorsSettings Colors;
        }
    }
}
