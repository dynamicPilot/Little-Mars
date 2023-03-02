﻿using LittleMars.Common;
using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.AudioSystems
{
    public class AudioSystem : IInitializable, IDisposable
    {
        readonly Settings _settings;
        readonly VolumeGroupControl.Factory _factory;
        readonly SignalBus _signalBus;

        Dictionary<VolumeGroupType, VolumeGroupControl> _groups;

        public AudioSystem(VolumeGroupControl.Factory factory, SignalBus signalBus, Settings settings)
        {
            _factory = factory;
            _signalBus = signalBus;
            _settings = settings;

            CreateGroups();
        }

        public void Initialize()
        {
            Debug.Log("Initialize AudioSystems");
            _signalBus.Subscribe<ConfigIsLoadedSignal>(OnConfigIsLoaded);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<ConfigIsLoadedSignal>(OnConfigIsLoaded);
            _groups = null;
        }

        public void ChangeGroupVolume(float value, VolumeGroupType type)
        {
            _groups[type].UpdateVolume(value);
        }

        public bool TryGetGroupVolume(VolumeGroupType type, out float volume)
        {
            volume = _groups[type].GetVolume();
            return !_groups[type].IsMute;
        }
        public float ToDefaultAndGetVolume(VolumeGroupType type)
        {
            return _groups[type].ToDefault();
        }

        public void UpdateIsMuteGroup(bool isMute, VolumeGroupType type)
        {
            _groups[type].UpdateIsMute(isMute);
        }

        void OnConfigIsLoaded(ConfigIsLoadedSignal args)
        {
            UpdateViaConfig(args.PlayerConfig);
        }

        void UpdateViaConfig(PlayerConfig config)
        {
            if (config == null) return;

            _groups[VolumeGroupType.music].UpdateViaConfig(config.MusicVolume, config.IsMusicOn);
            _groups[VolumeGroupType.total].UpdateViaConfig(config.SoundsVolume, config.IsSoundsOn);
        }

        void CreateGroups()
        {
            _groups = new();
            _groups.Add(VolumeGroupType.music, _factory.Create(_settings.MusicGroup));
            _groups.Add(VolumeGroupType.total, _factory.Create(_settings.TotalGroup));
        }

        [Serializable]
        public class Settings
        {
            public VolumeGroupData MusicGroup;
            public VolumeGroupData TotalGroup;
        }
    }
}
