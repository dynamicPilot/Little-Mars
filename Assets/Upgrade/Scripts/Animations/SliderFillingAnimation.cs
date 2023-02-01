using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations
{
    public class SliderFillingAnimation : TweenAnimation
    {
        [SerializeField] private Slider _slider;

        [Header("Animation Settings")]
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _startValue = 0;
        [SerializeField] private float _endValue = 1f;

        public void StartAnimation()
        {
            CheckValues();
            _slider.value = _startValue;
            FillingIndicator();
        }

        void FillingIndicator()
        {
            _slider.DOValue(_endValue, _duration);
        }

        void CheckValues()
        {
            if (_endValue > _slider.maxValue) _endValue = _slider.maxValue;
            if (_startValue < _slider.minValue) _startValue = _slider.minValue;
        }
    }
}
