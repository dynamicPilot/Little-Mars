using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.Radios
{
    public class RadioSystem : IInitializable
    {
        readonly RadioUI.Factory _factory;

        RadioUI _radioUI;
        SignalBus _signalBus;
        bool _isMute;
        public RadioSystem(RadioUI.Factory factory, SignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
            _radioUI = null;
            _isMute = false;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ConfigIsLoadedSignal>(OnConfigIsLoaded);
            _signalBus.Subscribe<MuteMusicSignal>(OnMuteMusic);
            _signalBus.Subscribe<UnmuteMusicSignal>(OnUnmuteMusic);
        }

        public void Dispose()
        {
            _signalBus?.Unsubscribe<ConfigIsLoadedSignal>(OnConfigIsLoaded);
            _signalBus?.Unsubscribe<MuteMusicSignal>(OnMuteMusic);
            _signalBus?.Unsubscribe<UnmuteMusicSignal>(OnUnmuteMusic);
        }

        void CreateRadio()
        {
            _radioUI = _factory.Create();
        }

        void StartRadio()
        {
            if (_radioUI == null) CreateRadio();
            _radioUI.StartRadio();
            _isMute = false;
        }

        void OnConfigIsLoaded(ConfigIsLoadedSignal args)
        {
            if (args.PlayerConfig == null) return;
            bool musicIsOn = args.PlayerConfig.IsMusicOn;

            _isMute = !musicIsOn;

            if (musicIsOn) StartRadio();
        }

        void OnMuteMusic()
        {
            if (_radioUI == null) return;
            if (_isMute) return;

            Debug.Log($"Radio: mute");
            _radioUI.StopRadio();
            _isMute = true;
        }

        void OnUnmuteMusic()
        {
            if (!_isMute) return;
            Debug.Log($"Radio: unmute");
            StartRadio();
        }

    }
}
