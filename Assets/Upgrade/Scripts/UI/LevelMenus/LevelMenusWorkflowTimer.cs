using System;

namespace LittleMars.UI.LevelMenus
{
    public class LevelMenusWorkflowTimer
    {
        readonly AsyncSignalGunTimer _timer;
        readonly EndLevelSignalGun.Factory _factory;

        int _delay = 2;

        public LevelMenusWorkflowTimer(AsyncSignalGunTimer timer, EndLevelSignalGun.Factory factory,
            Settings _settings)
        {
            _timer = timer;
            _factory = factory;
            _delay = _settings.SecondsDelay;
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
        }
    }
}
