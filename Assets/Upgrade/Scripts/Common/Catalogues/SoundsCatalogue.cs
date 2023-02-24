using LittleMars.Settings;
using System;
using UnityEngine;

namespace LittleMars.Common.Catalogues
{
    public class SoundsCatalogue
    {
        readonly Settings _settings;

        public SoundsCatalogue(Settings settings)
        {
            _settings = settings;
        }

        public AudioClip GetUISound(int typeIndex)
        {
            return _settings.UISoundsSettings.GetSound(typeIndex);
        }

        [Serializable]
        public class Settings
        {
            public UISoundsSettings UISoundsSettings;
        }
    }
}
