using LittleMars.Common.Signals;
using LittleMars.Localization;
using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace LittleMars.Configs
{
    public class PlayerConfigSystem : IInitializable
    {
        readonly JsonConverter _converter;
        readonly PlayerConfigProvider _provider;
        readonly SignalBus _signalBus;

        string _path;
        public PlayerConfigSystem(JsonConverter converter, Settings settings, 
            PlayerConfigProvider provider, SignalBus signalBus)
        {
            _converter = converter;
            _provider = provider;
            _signalBus = signalBus;

            _path = string.Join("/", new string[2] { Application.persistentDataPath, settings.ConfigFileName });           
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartLoadingSignal>(Load);
            _signalBus.Subscribe<NeedSaveConfigSignal>(SaveConfig);
        }

        void SaveConfig()
        {
            var config = _provider.GetData();
            Save(config);
        }

        void Save(PlayerConfig config)
        {
            _converter.ToJson(config, _path);
        }

        void Load()
        {
            if (File.Exists(_path))
            {
                var config = _converter.FromJson<PlayerConfig>(_path);
                _signalBus.Fire(new ConfigIsLoadedSignal { PlayerConfig = config, NeedUpdate = true });
            }
            else
            {
                var config = _provider.GetData();
                Save(config);
                _signalBus.Fire(new NoConfigIsLoadedSignal { PlayerConfig = config });
            }
                
        }


        [Serializable]
        public class Settings
        {
            public string ConfigFileName;
        }
    }
}
