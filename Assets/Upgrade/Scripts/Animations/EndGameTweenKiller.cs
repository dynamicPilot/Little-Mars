using DG.Tweening;
using LittleMars.Common.Signals;
using Zenject;

namespace LittleMars.Animations
{
    public class EndGameTweenKiller : IInitializable
    {
        readonly SignalBus _signalBus;

        public EndGameTweenKiller(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EndSceneSignal>(OnEndScene);
        }

        void OnEndScene()
        {
            _signalBus?.TryUnsubscribe<EndSceneSignal>(OnEndScene);
            DOTween.KillAll();
        }

    }
}
