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
            Debug.Log("Saving...");
            _converter.ToJson(config, _path, Newtonsoft.Json.Formatting.None);
        }

        void Load()
        {
            Debug.Log("Loading...");
            if (File.Exists(_path))
            {
                Debug.Log("Has confug");
                var config = _converter.FromJson<PlayerConfig>(_path);
                PrintConfig(config);
                _signalBus.Fire(new ConfigIsLoadedSignal { PlayerConfig = config, NeedUpdate = true });
            }
            else
            {
                Debug.Log("No confug");
                var config = _provider.GetData();
                Debug.Log("Creating config file " + config != null);
                Save(config);
                //_signalBus.Fire(new NoConfigIsLoadedSignal { PlayerConfig = config });
                _signalBus.Fire(new ConfigIsLoadedSignal { PlayerConfig = config, NeedUpdate = true });
            }
                
        }

        void PrintConfig(PlayerConfig config)
        {
            //Debug.Log($"Player Config : musicVolume {config.MusicVolume}, is Music on {config.IsMusicOn}.");
            //Debug.Log($"Player Config : soundsVolume {config.SoundsVolume}, is Sounds on {config.IsSoundsOn}.");
            //Debug.Log($"Player Config : langIndex {config.Lang}");
        }


        [Serializable]
        public class Settings
        {
            public string ConfigFileName;
        }
    }
}
