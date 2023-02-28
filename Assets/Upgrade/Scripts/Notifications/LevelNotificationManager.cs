using LittleMars.AudioSystems;
using LittleMars.Common.Signals;
using System;
using Zenject;

namespace LittleMars.Notifications
{
    public class LevelNotificationManager : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly UI.Notifications.NotificationUI _notUI;
        readonly NotSoundSystem _soundSystem;

        public LevelNotificationManager(SignalBus signalBus, UI.Notifications.NotificationUI notUI, 
            NotSoundSystem soundSystem)
        {
            _signalBus = signalBus;
            _notUI = notUI;
            _soundSystem = soundSystem;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<NeedResourceNotSignal>(OnNeedResourceNot);
            _signalBus.Subscribe<NeedRouteErrorNotSignal>(OnRouteErrorNot);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<NeedResourceNotSignal>(OnNeedResourceNot);
            _signalBus?.TryUnsubscribe<NeedRouteErrorNotSignal>(OnRouteErrorNot);
        }

        void OnNeedResourceNot(NeedResourceNotSignal args)
        {
            _notUI.ResourceNotification(args.Index);
            MakeNotification();
        }

        void OnRouteErrorNot()
        {
            _notUI.RouteNotification();
            MakeNotification();
        }

        void MakeNotification()
        {
            _soundSystem.OnCanNotDo();
        }


        [Serializable]
        public class Settings
        {
            public float NotDuration;
        }
    }
}
