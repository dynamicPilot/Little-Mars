using System.Collections;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public enum PERIOD { day, night }

    [Header("Options")]
    [SerializeField] private int gameMinutesPerRealSecond = 20;
    public int GameMinutesPerRealSecond { get { return gameMinutesPerRealSecond; } }
    [SerializeField] private int secondBetweenTimeUpdate = 5;

    [Header("Day & Night Settings")]
    [SerializeField] private int nightStartHour = 18;
    public int NightStartHour { get { return nightStartHour; } }

    [SerializeField] private int nightEndHour = 24;
    public int NightEndHour { get { return nightEndHour; } }

    [Header("Current State")]
    [SerializeField] private int daysCounter = 0;
    public int DaysCounter { get { return daysCounter; } }
    [SerializeField] private PERIOD period;
    public PERIOD Period { get { return period; } }

    [Header("UI")]
    //[SerializeField] private DayTimeSlider dayTimeSlider;
    [SerializeField] private DayTimeIndicator dayTimeIndicator;

    [Header("Script")]
    [SerializeField] private State state;
    [SerializeField] private LevelControl levelControl;
    [SerializeField] private DayNightTransition dayNightTransition;
    [SerializeField] private BuildingsControl buildingsControl;

    [SerializeField] private bool gameIsPaused = false;
    [SerializeField] private float timePlayedOnDay = 0f;

    [SerializeField] private int hour;
    [SerializeField] private int prevHour;
    [SerializeField] private int minutes;

    public delegate void PeriodToNewDay();
    public PeriodToNewDay OnPeriodToNewDay;

    public delegate void PeriodToNewNight();
    public PeriodToNewNight OnPeriodToNewNight;

    private void Awake()
    {
        gameIsPaused = true;
    }

    public void StartLevel()
    {
        gameIsPaused = true;
        daysCounter = -1;

        dayTimeIndicator.SetDayTimeSettings(nightEndHour, 0);

        StartNewDay();
    }

    public void StartNewDay()
    {
        try
        {
            StopCoroutine(TimeCounter());
        }
        catch
        {
            StopAllCoroutines();
        }

        //Debug.Log("GameTime: Starting new day!");
        gameIsPaused = false;
        daysCounter++;
        period = PERIOD.day;

        prevHour = 0;
        hour = 0;
        minutes = 0;
        timePlayedOnDay = 0f;
        dayTimeIndicator.SetValue(hour, period);

        if (OnPeriodToNewDay != null)
        {
            OnPeriodToNewDay.Invoke();
        }

        StartCoroutine(TimeCounter());
    }

    public void StartNewNight()
    {
        StopCoroutine(TimeCounter());
        //Debug.Log("GameTime: Starting night day!");
        period = PERIOD.night;
        dayTimeIndicator.SetValue(hour, period);

        if (OnPeriodToNewNight != null)
        {
            OnPeriodToNewNight.Invoke();
        }

        StartCoroutine(TimeCounter());
    }

    private void Update()
    {
        if (gameIsPaused)
        {
            return;
        }

        timePlayedOnDay += Time.deltaTime;

        if (hour - prevHour == state.BetweenHourlyNeedsCalculationTime)
        {
            //Debug.Log("NEEDS CALCULATION!");
            prevHour = hour;
            state.CalculateHourlyProductionAndNeeds(period, hour);
            levelControl.AddHourToLevelLimitsTimers();
            dayNightTransition.CheckForMakeTransfer(hour);
            buildingsControl.ActivateAnimatorsForCosmodromes(hour);
            dayTimeIndicator.SetValue(hour, period);
        }
    }

    IEnumerator TimeCounter()
    {
        hour = Mathf.FloorToInt(timePlayedOnDay * gameMinutesPerRealSecond / 60f);
        minutes = Mathf.FloorToInt(timePlayedOnDay * gameMinutesPerRealSecond % 60f);

        if (hour >= nightStartHour && period != PERIOD.night)
        {
            StartNewNight();
        }
        else if (hour >= nightEndHour && period == PERIOD.night)
        {
            StartNewDay();
        }

        yield return new WaitForSeconds(secondBetweenTimeUpdate);

        StartCoroutine(TimeCounter());
    }
}
