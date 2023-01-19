using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.Effects
{
    [RequireComponent(typeof(Slider))]
    public class ProgressIndicatorUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        float _targetValue = 1f;
        private void OnValidate()
        {
            _slider.minValue = 0f;
            _slider.maxValue = 1f;
        }

        public void SetTargetValue(float targetValue)
        {
            _targetValue = targetValue;
        }

        public void UpdateIndicator(float value = 0f)
        {
            var scaledValue = value / _targetValue;

            if (scaledValue < _slider.minValue) scaledValue = _slider.minValue;
            else if (scaledValue > _slider.maxValue) scaledValue = _slider.maxValue;

            _slider.value = scaledValue;
        }
    }
}
