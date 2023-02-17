using LittleMars.Common.Signals;
using System;
using Zenject;

namespace LittleMars.Commands.MainMenu
{
    public class ToLevelCommand: IInitializable, IDisposable, ICommand
    {
        readonly SignalBus _signalBus;
        readonly MenuReceiver _receiver;

        bool _isSubscribed;
        int _levelIndex;
        public ToLevelCommand(SignalBus signalBus, MenuReceiver receiver)
        {
            _signalBus = signalBus;
            _receiver = receiver;
        }

        public void Initialize()
        {
            _isSubscribed = true;
            _signalBus.Subscribe<ToLevelSignal>(OnToLevelSignal);
        }

        public void Dispose() => Unsubscribe();
        public void Execute()
        {
            _receiver.ToLevel(_levelIndex);
        }
        void OnToLevelSignal(ToLevelSignal args)
        {
            Unsubscribe();
            _levelIndex = args.Index;
            Execute();
        }

        void Unsubscribe()
        {
            if (_isSubscribed) _signalBus?.TryUnsubscribe<ToLevelSignal>(OnToLevelSignal);
            _isSubscribed = false;
        }
    }
}
