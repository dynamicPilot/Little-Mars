using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Loaders;
using Zenject;

namespace LittleMars.SceneControls
{
    public class GameSceneControl : IInitializable
    {
        readonly SceneLoader _loader;
        readonly SignalBus _signalBus;

        SceneType _nextSceneType;
        public GameSceneControl(SceneLoader loader, SignalBus signalBus)
        {
            _loader = loader;
            _signalBus = signalBus;
        }

        public void Initialize() => Subscribe();
        public void NextSceneType(SceneType type) => _nextSceneType = type;

        void Load()
        {
            Unsubscribe();
            _loader.LoadSceneAsync(_nextSceneType);
        }

        void Subscribe()
        {
            _signalBus.Subscribe<EndSceneSignal>(Load);
        }

        void Unsubscribe()
        {
            _signalBus?.TryUnsubscribe<EndSceneSignal>(Load);
        }

    }
}
