﻿using LittleMars.Common.Signals;
using LittleMars.UI.Effects;
using LittleMars.UI.ResourceSlots;
using TMPro;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class GoalWithTimerSlotUI : ResourceBalanceSlotUI, IGoalSlot
    {
        [SerializeField] private TextMeshProUGUI _firstTargetCounter;
        [SerializeField] private TextMeshProUGUI _secondTargetCounter;

        [Header("Indicator")]
        [SerializeField] private ProgressIndicatorUI _indicator;
        // indicator
        float _firstTargetValue;
        float _secondTargetValue;

        public void SetTargetValue(float firstValue, float secondValue)
        {
            _firstTargetValue = firstValue;
            _secondTargetValue = secondValue;

            _firstTargetCounter.text = _firstTargetValue.ToString(_format);
            _secondTargetCounter.text = _secondTargetValue.ToString(_format);

            // update indicator
            _indicator.SetTargetValue(_firstTargetValue + _secondTargetValue);
        }

        public void UpdateSlot(GoalUpdatedSignal args)
        {
            if (args.Values.Length < 2) return;
            base.UpdateSlot(args.Values[0], args.Values[1]);
            _indicator.UpdateIndicator(args.Values[0] + args.Values[1]);
        }

        //public class Factory : PlaceholderFactory<GoalWithTimerSlotUI>
        //{
        //}
    }
}
