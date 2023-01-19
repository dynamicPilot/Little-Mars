using LittleMars.Common.Signals;
using LittleMars.UI.Effects;
using LittleMars.UI.ResourceSlots;
using TMPro;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class GoalSlotUI : ResourceSlotUI, IGoalSlot
    {
        // icon, current number - resource slot
        [SerializeField] private TextMeshProUGUI _targetCounter;
        [SerializeField] private ProgressIndicatorUI _indicator;
        // indicator
        float _targetValue;
        string _format = "F0";

        public void SetTargetValue(float targetValue)
        {
            _targetValue = targetValue;
            _targetCounter.text = _targetValue.ToString(_format);
            // update indicator
            _indicator.SetTargetValue(targetValue);
        }

        public void UpdateSlot(GoalUpdatedSignal args)
        {
            if (args.Values.Length == 0) return;

            base.UpdateSlot(args.Values[0]);
            _indicator.UpdateIndicator(args.Values[0]);
        }

        //public class Factory : PlaceholderFactory<GoalSlotUI>
        //{
        //}
    }

    
}


