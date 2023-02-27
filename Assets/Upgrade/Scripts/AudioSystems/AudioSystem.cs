using System;

namespace LittleMars.AudioSystems
{
    public class AudioSystem
    {
        [Serializable]
        public class Settings
        {
            public VolumeGroupData MusicGroup;
            public VolumeGroupData TotalGroup;
        }
    }
}
