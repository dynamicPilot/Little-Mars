using LittleMars.Rockets;
using LittleMars.Settings;
using System;
using static LittleMars.Settings.LevelSettings;

namespace LittleMars.LevelMenus
{
    public class LevelMenusWorkflowTimer
    {
        readonly AsyncSignalGunTimer _timer;
        readonly EndLevelSignalGun.Factory _factory;

        int _delay = 2;

        public LevelMenusWorkflowTimer(AsyncSignalGunTimer timer, EndLevelSignalGun.Factory factory,
            Settings _settings, RocketsManager.Settings rocketSettings)
        {
            _timer = timer;
            _factory = factory;

            var isLongDelay = rocketSettings.HasRockets;
            _delay = (isLongDelay) ? _settings.LongDelay : _settings.SecondsDelay;
        }

        public void StartEndMenuTimer()
        {
            using var gun = _factory.Create();
            _timer.StartTimer(_delay, gun);
        }

        [Serializable]
        public class Settings
        {
            public int SecondsDelay;
            public int LongDelay;
        }
    }
}
