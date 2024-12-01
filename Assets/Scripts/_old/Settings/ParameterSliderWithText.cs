using UnityEngine;
using UnityEngine.UI;

public class ParameterSliderWithText : ParameterSlider
{
    [SerializeField] private Text text;
    [SerializeField] private float correctionValue = 10f;

    public override void SetSlider(float value)
    {
        base.SetSlider(value * correctionValue);
        text.text = GetValue().ToString("f1");
    }

    public override float GetValue()
    {
        return (base.GetValue() / correctionValue);
    }

    public void OnAirTextControl(float value)
    {
        text.text = (value / correctionValue).ToString("f1");
    }
}
