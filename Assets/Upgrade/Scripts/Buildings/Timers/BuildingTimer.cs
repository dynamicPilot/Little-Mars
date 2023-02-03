using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.Timers
{
    public class BuildingTimer : IFixedTickable
    {
        //readonly Settings _settings;
        readonly SignalBus _signalBus;

        bool _wasEverOn;
        bool _isRunning;
        float _targetValue;
        float _timer;

        public BuildingTimer(Settings settings, SignalBus signalBus)
        {
            _signalBus = signalBus;

            _targetValue = settings.TimerTargetValue;
            _isRunning = false;
            _wasEverOn = false;
        }

        public void StartTimer()
        {
            if (!_wasEverOn) return;
            if (_isRunning) return;

            _timer = 0f;
            _isRunning = true;
            Debug.Log("Start timer");
        }

        private void CheckTimer()
        {
            if (_timer >= _targetValue) TimerIsOver();
        }

        private void TimerIsOver()
        {
            Debug.Log("Timer is over");
            StopTimer();
        }

        public void StopTimer()
        {
            if (!_wasEverOn) _wasEverOn = true;

            if (!_isRunning) return;
            _isRunning = false;

            Debug.Log("Stop timer");
        }

        public void FixedTick()
        {
            if (!_isRunning) return;

            _timer += Time.fixedDeltaTime;
            CheckTimer();
        }

        [Serializable]
        public class Settings
        {
            public float TimerTargetValue;
        }

        public class Factory : PlaceholderFactory<BuildingTimer>
        {
        }
    }
}
