﻿using LittleMars.Common;
using LittleMars.Common.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Model.TimeUpdate
{
    public class TimeManager
    {
        int _dayCounter;
        Period _period;
        int _hour;

        Settings _settings;
        SignalBus _signalBus;
        HourlySignal _hourlySignal;

        public TimeManager(Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            _signalBus = signalBus;

            _hour = 0;
            _dayCounter = 0;
            _period = Period.day;

            _hourlySignal = new HourlySignal { Hour = _hour };

        }

        public void UpdateTime(float totalSeconds)
        {            
            var hour = Mathf.FloorToInt(totalSeconds * _settings.GameMinutesPerSecond / 60f);
            var minutes = Mathf.FloorToInt(totalSeconds * _settings.GameMinutesPerSecond % 60f);

            if (hour - _hour > 0)
            {
                _hour = hour;

                OnHourChanged();
                CheckPeriodChange();
            }                       
        }

        void NewDay()
        {
            _dayCounter++;
            _hour = 0;
            Debug.Log("New day " + _dayCounter);
        }

        void CheckPeriodChange()
        {
            if (_hour >= _settings.NightStartHour && _period != Period.night)
                NewPeriod(Period.night);
            else if (_hour >= _settings.NightEndHour && _period != Period.day)
            {
                NewPeriod(Period.day);
                NewDay();
            }       
        }

        void NewPeriod(Period period)
        {
            _period = period;
            _signalBus.TryFire(new PeriodChangeSignal { Period = period });
            if (period == Period.day) _dayCounter++;
        }

        void OnHourChanged()
        {
            _hourlySignal.Hour = _hour;
            _signalBus.Fire(_hourlySignal);
        }

        [Serializable]
        public class Settings
        {
            public int GameMinutesPerSecond;
            public int NightStartHour;
            public int NightEndHour;
        }
    }
}
