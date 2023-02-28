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

        public AudioClip GetNotSound(int typeIndex)
        {
            return _settings.NotSoundsSettings.GetSound(typeIndex);
        }

        [Serializable]
        public class Settings
        {
            public UISoundsSettings UISoundsSettings;
            public NotSoundSettings NotSoundsSettings;
            public AudioClip RocketLaunch;
            public AudioClip TimerSound;
        }
    }
}
