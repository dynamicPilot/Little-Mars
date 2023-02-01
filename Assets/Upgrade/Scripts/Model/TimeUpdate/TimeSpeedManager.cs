using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.TimeUpdate
{
    public class TimeSpeedManager : IInitializable
    {
        SignalBus _signalBus;
        float _speed;

        public TimeSpeedManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _speed = 1f;
            Stop();
        }
        public void Start()
        {
            UpdateTimeScale(_speed);
        }

        public void Stop()
        {
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
