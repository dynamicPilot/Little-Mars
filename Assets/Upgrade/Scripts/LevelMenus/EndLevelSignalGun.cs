using Zenject;
using UnityEngine;
using LittleMars.Common.Signals;

namespace LittleMars.LevelMenus
{
    public class EndLevelSignalGun : ISignalGun
    {
        readonly SignalBus _signalBus;

        public EndLevelSignalGun(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Dispose()
        {
        }

        public void FireSignal()
        {
            _signalBus.Fire<EndGameSignal>();
        }

        public class Factory : PlaceholderFactory<EndLevelSignalGun>
        { }
    }
}
