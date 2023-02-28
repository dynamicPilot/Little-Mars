using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.TimeUpdate
{
    public class TimeSpeedManager : IInitializable
    {
        SignalBus _signalBus;
        float _speed;
        int _stopCounter;
        public TimeSpeedManager(SignalBus signalBus)
        {
            _signalBus = signalBus;            
        }

        public void Initialize()
        {
            _speed = 1f;
            _stopCounter = 0;
            Stop(false);
        }
        public void Start(bool isExternal = true)
        {
            if (isExternal) _stopCounter--;
            if (_stopCounter > 0) return;
            UpdateTimeScale(_speed);
        }

        public void Stop(bool isExternal = true)
        {
            if (isExternal) _stopCounter++;
            UpdateTimeScale(0f);
        }

        public void ChangeSpeed()
        {
            _speed = (_speed == 1f) ? 2f : 1f;
            UpdateTimeScale(_speed);
        }

        public int GetSpeed()
        {
            return (int)Time.timeScale;
        }

        private void UpdateTimeScale(float speed)
        {
            Time.timeScale = speed;
            _signalBus.Fire<TimeSpeedChangedSignal>();
        }

    }
}
