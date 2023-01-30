using LittleMars.Common.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.TimeUpdate
{
    public class TimeUpdater : MonoBehaviour
    {
        bool _pauseUpdate = true;
        float _totalSeconds = 0f;
        float _timer = 0f;

        Settings _settings;
        TimeManager _manager;
        SignalBus _signalBus;

        [Inject]
        public void Constructor(Settings settings, TimeManager manager, SignalBus signalBus)
        {
            _settings = settings;
            _manager = manager;
            _signalBus = signalBus;
            _totalSeconds = 0f;

            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanaged);
            
            // move to signals
            StartUpdater();
        }

        private void OnDestroy()
        {
            //_signalBus.TryUnsubscribe<PeriodChangeSignal>(OnPeriodChanaged);
        }
        void StartUpdater()
        {
            _pauseUpdate = false;
        }

        void StopUpdater()
        {
            _pauseUpdate = true;
        }

        void ResetTotalTime()
        {
            _totalSeconds = 0f;
        }

        void OnPeriodChanaged(PeriodChangeSignal arg)
        {
            if (arg.Period == Common.Period.day) ResetTotalTime();
        }

        private void Update()
        {
            if (_pauseUpdate) return;
            var period = Time.deltaTime;
            _totalSeconds += period;
            _timer += period;

            if (_timer >= _settings.UpdateTime)
            {
                _manager.UpdateTime(_totalSeconds);
                _timer = 0f;
            }
            
        }

        [Serializable]
        public class Settings
        {
            public float UpdateTime;
        }
    }
}
