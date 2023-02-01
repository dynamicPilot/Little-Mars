using LittleMars.Common.Signals;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public class TimeSpeedControllerUI : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SignByIndexUI _signUI;
        
        SignalBus _signalBus;
        TimeSpeedManager _controller;

        [Inject]
        public void Constructor(SignalBus signalBus, TimeSpeedManager manager)
        {
            _controller = manager;
            _signalBus = signalBus;

            Init();
        }

        private void Init()
        {
            _signalBus.Subscribe<TimeSpeedChangedSignal>(OnTimeSpeedChanged);
            _button.onClick.AddListener(ChangeSpeed);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<TimeSpeedChangedSignal>(OnTimeSpeedChanged);
            _button.onClick.RemoveAllListeners();
        }

        private void ChangeSpeed()
        {
            _controller.ChangeSpeed();
        }

        private void OnTimeSpeedChanged()
        {
            int index = _controller.GetSpeed();
            _button.interactable = (index != 0);
            _signUI.UpdateSign(index);
        }
    }
}
