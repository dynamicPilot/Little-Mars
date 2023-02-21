using LittleMars.Common;
using LittleMars.Common.Signals;
using System;
using Zenject;

namespace LittleMars.Configs
{ 
    public class PlayerSettings : IInitializable
    {
        readonly Settings _settings;
        readonly SignalBus _signalBus;
        public bool IsMusicOn { get; private set; }
        public bool IsSoundOn { get; private set; }
        public float MusicVolume { get; private set; }
        public float SoundVolume {  get; private set; }
        public int Lang { get; private set; }

        public PlayerSettings(Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            _signalBus = signalBus;
            SetDefaults();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ConfigIsLoadedSignal>(OnConfigIsLoaded);
        }

        void OnConfigIsLoaded(ConfigIsLoadedSignal args)
        {
            if (args.NeedUpdate)
                UpdateWithConfig(args.PlayerConfig);
        }

        void SetDefaults()
        {
            IsMusicOn = true;
            IsSoundOn = true;
            MusicVolume = _settings.DefaultMusicVolume;
            SoundVolume = _settings.DefaultTotalVolume;

            Lang = (int)_settings.DefaultLang;
        }

        void UpdateWithConfig(PlayerConfig config)
        {
            IsMusicOn = config.IsMusicOn;
            IsSoundOn = config.IsSoundsOn;
            MusicVolume = config.MusicVolume;
            SoundVolume = config.SoundsVolume;
            Lang = (int)config.Lang;
        }

        [Serializable]
        public class Settings
        {
            public float DefaultMusicVolume;
            public float DefaultTotalVolume;
            public Langs DefaultLang;
        }
    }
}
