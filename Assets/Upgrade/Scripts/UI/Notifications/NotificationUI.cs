using LittleMars.Common;
using LittleMars.Common.Catalogues;
using LittleMars.Common.Signals;
using LittleMars.Notifications;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.Notifications
{
    public class NotificationUI : MenuUI
    {
        [SerializeField] Image _sign;

        SignalBus _signalBus;
        IconsCatalogue _catalogue;
        WaitForSeconds _timer;

        [Inject]
        public void Constructor(IconsCatalogue catalogue, LevelNotificationManager.Settings settings,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _catalogue = catalogue;
            _timer = new WaitForSeconds(settings.NotDuration);

        }

        void SetSign(Sprite sign)
        {
            _sign.sprite = sign;
        }

        public void ResourceNotification(int resourceIndex)
        {
            var icon = _catalogue.ResourceIcon((Resource) resourceIndex);
            SetSign(icon);
            MakeNotification();
        }

        public void RouteNotification()
        {
            var icon = _catalogue.RouteErrorIcon();
            SetSign(icon);
            MakeNotification();
        }

        void MakeNotification()
        {
            if (_isOpen) Restart();
            else StartCoroutine(ShowingNotification());
        }

        void Restart()
        {
            StopAllCoroutines();
            StartCoroutine(ShowingNotification());
        }

        IEnumerator ShowingNotification()
        {
            Open();
            yield return _timer;
            Close();
        }

    }
}
