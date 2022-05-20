using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Gradient gradientForDay;
    [SerializeField] private Gradient gradientForNight;

    private int nightStartHour;
    private int nightEndHour;
    private int startHour;


    private void Awake()
    {
        slider.value = 0;
    }

    public void SetDayTimeSettings(int newNightStartHour, int newNightEndHour, int newStartHour = 0)
    {
        nightStartHour = newNightStartHour;
        nightEndHour = newNightEndHour;
        startHour = newStartHour;
        SetValue(startHour);
    }

    public void SetValue(int hour, GameTime.PERIOD period = GameTime.PERIOD.day)
    {
        slider.value = (float) hour / (nightEndHour - startHour);

        if (period == GameTime.PERIOD.day)
        {
            sliderFill.color = gradientForDay.Evaluate((float) hour / (nightStartHour - startHour));
        }
        else
        {
            sliderFill.color = gradientForNight.Evaluate((float) (hour - nightStartHour) / (nightEndHour - nightStartHour));
        }
        
    }
}
