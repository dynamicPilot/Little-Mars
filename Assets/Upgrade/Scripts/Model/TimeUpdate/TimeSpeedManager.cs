using UnityEngine;
using Zenject;

namespace LittleMars.Model.TimeUpdate
{
    public class TimeSpeedManager : IInitializable
    {
        float _speed;
        public void Initialize()
        {
            _speed = 1f;
            Stop();
        }
        public void Start()
        {
            Time.timeScale = _speed;
        }

        public void Stop()
        {
            Time.timeScale = 0f;
        }

        public void ChangeSpeed()
        {
            _speed = (_speed == 1f) ? 2f : 1f;

        }

    }
}
