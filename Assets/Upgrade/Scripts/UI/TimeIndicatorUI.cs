using LittleMars.Common.Signals;
using LittleMars.Model.TimeUpdate;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public class TimeIndicatorUI : MonoBehaviour
    {
        [SerializeField] private Slider _indicatorSlider;

        float _minValue = 0f;
        float _step = 0f;

        SignalBus _signalBus;

        [Inject]
        public void Constructor(TimeManager.Settings settings, SignalBus signalBus)
        {
            _step = 1f / settings.NightEndHour;
            _signalBus = signalBus;
            _signalBus.Subscribe<HourlySignal>(UpdateIndicator);
        }

        private void Start()
        {
            _indicatorSlider.value = _minValue;
        }

        private void OnDisable()
        {
            _signalBus.TryUnsubscribe<HourlySignal>(UpdateIndicator);
        }

        private void UpdateIndicator()
        {
            Debug.Log("Update indicator " + _step);
            _indicatorSlider.value += _step;
        }
    }
}

