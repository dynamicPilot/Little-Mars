using LittleMars.AudioSystems;
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
        UISoundSystem _audioSystem;

        [Inject]
        public void Constructor(SignalBus signalBus, TimeSpeedManager manager, UISoundSystem audioSystem)
        {
            _controller = manager;
            _signalBus = signalBus;
            _audioSystem = audioSystem;

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

        void ChangeSpeed()
        {
            _audioSystem.PlayUISound(Common.UISoundType.clickThird);
            _controller.ChangeSpeed();
        }

        void OnTimeSpeedChanged()
        {
            int index = _controller.GetSpeed();
            _button.interactable = (index != 0);
            _signUI.UpdateSign(index);
        }
    }
}
