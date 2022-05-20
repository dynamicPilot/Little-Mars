using UnityEngine;
using UnityEngine.UI;

public class DayTimeIndicator : MonoBehaviour
{
    [SerializeField] private Image indicatorFill;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;

    //private int nightStartHour;
    private int endHour;
    private int startHour;

    private void Awake()
    {
        indicatorFill.fillAmount = 0f;
    }

    public void SetDayTimeSettings(int newEndHour = 24, int newStartHour = 0)
    {
        //nightStartHour = newNightStartHour;
        endHour = newEndHour;
        startHour = newStartHour;
        SetValue(startHour);
    }

    public void SetValue(int hour, GameTime.PERIOD period = GameTime.PERIOD.day)
    {
        indicatorFill.fillAmount = (float)hour / (endHour - startHour);

        if (period == GameTime.PERIOD.day)
        {
            indicatorFill.color = dayColor;
        }
        else
        {
            indicatorFill.color = nightColor;
        }

    }
}
