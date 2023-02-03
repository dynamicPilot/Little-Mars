using LittleMars.Common.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.TimeUpdate
{
    public class TimeUpdaterTickable : IInitializable, ITickable, IDisposable
    {
        readonly Settings _settings;
        readonly TimeManager _manager;
        readonly SignalBus _signalBus;

        bool _pauseUpdate = true;
        float _totalSeconds = 0f;
        float _timer = 0f;

        public TimeUpdaterTickable(Settings settings, TimeManager manager, SignalBus signalBus)
        {
            _settings = settings;
            _manager = manager;
            _signalBus = signalBus;
            _totalSeconds = 0f;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanaged);

            // move to signals
            StartUpdater();
        }
        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<PeriodChangeSignal>(OnPeriodChanaged);
        }

        public void Tick()
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

        [Serializable]
        public class Settings
        {
            public float UpdateTime;
        }
    }
}
