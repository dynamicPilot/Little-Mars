using LittleMars.AudioSystems;
using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Configs
{
    public class PlayerConfigProvider
    {
        readonly LangSettings _langSettings;
        readonly AudioSystem _audioSystem;
        public PlayerConfigProvider(LangSettings settings, AudioSystem audio)
        {
            _langSettings = settings;
            _audioSystem = audio;
        }

        public PlayerConfig GetData()
        {
            var isMusicOn = _audioSystem.TryGetGroupVolume(VolumeGroupType.music, out var musicVolume);
            var isSoundOn = _audioSystem.TryGetGroupVolume(VolumeGroupType.total, out var soundVolume);
            Debug.Log("PlayerConfigProvider: Creating data...");
            return new PlayerConfig(isMusicOn, musicVolume, isSoundOn, soundVolume, _langSettings.LangIndex);
        }
    }
}
