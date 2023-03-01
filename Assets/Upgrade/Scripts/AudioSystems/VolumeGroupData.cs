using LittleMars.Common;
using System;
using UnityEngine.Audio;

namespace LittleMars.AudioSystems
{
    [Serializable]
    public class VolumeGroupData
    {
        public AudioMixer Mixer;
        public string Parameter;
        public float MinVolume;
        public float MaxVolume;
        public float DefaultVolume;
    }
}
