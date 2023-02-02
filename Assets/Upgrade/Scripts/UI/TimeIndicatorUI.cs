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

        SignalBus _signalBus;

        float _minValue = 0f;
        float _step = 0f;

        [Inject]
        public void Constructor(TimeManager.Settings settings, SignalBus signalBus)
        {
            _step = 1f / settings.NightEndHour;
            _signalBus = signalBus;

            Init();
        }

        private void Init()
        {
            _signalBus.Subscribe<HourlySignal>(UpdateIndicator);
            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanged);
        }

        private void Start()
        {
            ResetIndicator();
        }

        private void OnDisable()
        {
            _signalBus?.TryUnsubscribe<HourlySignal>(UpdateIndicator);
            _signalBus?.TryUnsubscribe<PeriodChangeSignal>(OnPeriodChanged);
        }

        private void UpdateIndicator()
        {            
            _indicatorSlider.value += _step;
            //Debug.Log("Update indicator " + _indicatorSlider.value);
        }

        private void ResetIndicator()
        {
            _indicatorSlider.value = _minValue;
        }

        private void OnPeriodChanged(PeriodChangeSignal args)
        {
            if (args.Period == Common.Period.day)
                ResetIndicator();
        }
    }
}

