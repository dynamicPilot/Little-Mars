using LittleMars.Animations;
using LittleMars.Common.Signals;
using LittleMars.UI;
using System;
using Zenject;

namespace LittleMars.Effects
{
    public class PeriodChangeEffectControl : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly NightFadeAnimation _animation;

        public PeriodChangeEffectControl(SignalBus signalBus, GameAnimations animations)
        {
            _signalBus = signalBus;
            _animation = animations.NightAnimation;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanged);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<PeriodChangeSignal>(OnPeriodChanged);
        }

        private void OnPeriodChanged(PeriodChangeSignal args)
        {
            if (args.Period == Common.Period.day)
                _animation.ToDayState();
            else
                _animation.ToNightState();
        }
    }
}
