using Zenject;
using LittleMars.Common.Signals;

namespace LittleMars.UI
{
    public class LoadingUI : MenuUI
    {
        SignalBus _signalBus;

        bool _isSubscribed;
        [Inject]
        public void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _isSubscribed = false;
            Init();
        }

        void Init()
        {
            _signalBus.Subscribe<EndSceneSignal>(OnEndScene);
            _isSubscribed = true;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        void OnEndScene()
        {
            Unsubscribe();
            Open();
        }

        void Unsubscribe()
        {
            if (!_isSubscribed) return;

            _isSubscribed = false;
            _signalBus?.TryUnsubscribe<EndSceneSignal>(Open);
        }
    }
}
