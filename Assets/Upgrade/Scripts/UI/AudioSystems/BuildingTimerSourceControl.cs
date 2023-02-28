using LittleMars.Common.Catalogues;
using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.AudioSystems
{
    public class BuildingTimerSourceControl : MonoBehaviour
    {
        [SerializeField] AudioSource _source;

        SignalBus _signalBus;
        bool _isPlaying;
        int _activeTimersCounter;

        [Inject]
        public void Constructor(SoundsCatalogue.Settings settings, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _source.clip = settings.TimerSound;

            _isPlaying = false;
            _activeTimersCounter = 0;

            Init();
        }

        void Init()
        {
            _signalBus.Subscribe<TimerOnSignal>(OnTimerOn);
            _signalBus.Subscribe<TimerOffSignal>(OnTimerOff);
        }

        private void OnDestroy()
        {
            _signalBus?.TryUnsubscribe<TimerOnSignal>(OnTimerOn);
            _signalBus?.TryUnsubscribe<TimerOffSignal>(OnTimerOff);
        }

        void OnTimerOn()
        {
            _activeTimersCounter++;
            if (_isPlaying) return;

            StartPlaying();
        }

        void OnTimerOff()
        {
            if (!_isPlaying) return;

            _activeTimersCounter--;
            if (_activeTimersCounter == 0) 
                StopPlaying();
        }

        void StartPlaying()
        {
            _isPlaying = true;
            _source.Play();
        }

        void StopPlaying()
        {
            _isPlaying = false;
            _source.Stop();
        }
    }
}
