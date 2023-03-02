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
        public int LangIndex { get; private set; }

        public LangSettings(Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            _signalBus = signalBus;
            ToDefaultAndGetLang();
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

        public int ToDefaultAndGetLang()
        {
            UpdateLang((int)_settings.DefaultLang);
            return LangIndex;
        }

        public void UpdateLang(int langIndex)
        {
            LangIndex = langIndex;
        }

        void UpdateViaConfig(PlayerConfig config)
        {
            if (config == null) return;
            LangIndex = (int)config.Lang;
        }

        [Serializable]
        public class Settings
        {
            public Langs DefaultLang;
        }
    }
}
