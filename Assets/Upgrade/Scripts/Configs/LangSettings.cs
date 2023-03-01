using LittleMars.Common;
using LittleMars.Common.Signals;
using System;
using Zenject;

namespace LittleMars.Configs
{ 
    public class LangSettings : IInitializable
    {
        readonly Settings _settings;
        readonly SignalBus _signalBus;
        public int Lang { get; private set; }

        public LangSettings(Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            _signalBus = signalBus;
            ToDefault();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ConfigIsLoadedSignal>(OnConfigIsLoaded);
        }

        void OnConfigIsLoaded(ConfigIsLoadedSignal args)
        {
            if (args.NeedUpdate)
                UpdateViaConfig(args.PlayerConfig);
        }

        void ToDefault()
        {
            Lang = (int)_settings.DefaultLang;
        }

        void UpdateViaConfig(PlayerConfig config)
        {
            if (config == null) return;
            Lang = (int)config.Lang;
        }

        [Serializable]
        public class Settings
        {
            public Langs DefaultLang;
        }
    }
}
