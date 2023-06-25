using LittleMars.Common;
using LittleMars.Common.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.Timers
{
    public class BuildingTimer : IFixedTickable
    {
        readonly SignalBus _signalBus;
        readonly BuildingType _type;
        readonly Size _size;

        bool _wasEverOn;
        bool _isRunning;
        float _targetValue;
        float _timer;

        public BuildingTimer(Settings settings, SignalBus signalBus, BuildingType type, Size size)
        {
            _signalBus = signalBus;
            _type = type;
            _size = size;
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
            //Debug.Log("Start timer");
            _signalBus.Fire<TimerOnSignal>();
        }

        private void CheckTimer()
        {
            //Debug.Log("Check timer " + _timer + ". Target value " + _targetValue);
            if (_timer >= _targetValue) TimerIsOver();
        }

        private void TimerIsOver()
        {
            Debug.Log("Timer is over");
            StopTimer();

            _signalBus.Fire(GetSignal());
        }

        public void StopTimer()
        {
            if (!_wasEverOn) _wasEverOn = true;

            if (!_isRunning) return;
            _isRunning = false;

            //Debug.Log("Stop timer");
            _signalBus.Fire<TimerOffSignal>();
        }

        public void FixedTick()
        {
            if (!_isRunning) return;

            _timer += Time.fixedDeltaTime;
            CheckTimer();
        }

        public void ResetTimer()
        {
            StopTimer();
            _wasEverOn = false;
        }

        private BuildingTimerIsOverSignal GetSignal()
        {
            Debug.Log($"Create signal for timer over for type { _type} ans size {_size}.");
            return new BuildingTimerIsOverSignal
            {
                Type = _type,
                Size = _size
            };
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
