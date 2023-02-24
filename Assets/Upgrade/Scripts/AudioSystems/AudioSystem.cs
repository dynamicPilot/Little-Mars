using LittleMars.Common;
using LittleMars.UI.AudioSystems;
using System;

namespace LittleMars.AudioSystems
{
    public class AudioSystem
    {
        readonly AudioSystemUI _systemUI;
        readonly Settings _settings;

        public AudioSystem(AudioSystemUI systemUI, Settings settings)
        {
            _systemUI = systemUI;
            _settings = settings;
        }

        public void PlayUISound(UISoundType type)
        {
            _systemUI.PlayUISound(type);
        }


        [Serializable]
        public class Settings
        {
            public VolumeGroupData MusicGroup;
            //public VolumeGroupData MusicGroup;
            public VolumeGroupData TotalGroup;
        }
    }
}
