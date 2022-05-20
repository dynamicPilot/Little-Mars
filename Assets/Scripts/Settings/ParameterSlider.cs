using UnityEngine;
using UnityEngine.UI;

public class ParameterSlider : MonoBehaviour
{
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;

    private Slider slider;
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.minValue = minValue;
        slider.maxValue = maxValue;
    }

    public void SetMinAndMax(float newMinValue, float newMaxValue)
    {
        minValue = newMinValue;
        maxValue = newMaxValue;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
    }

    public virtual void SetSlider(float value)
    {
        slider.value = value;
    }

    public virtual float GetValue()
    {
        return slider.value;
    }

    public void IsInteractableSlider(bool isIntercatable)
    {
        slider.interactable = isIntercatable;
    }
}
