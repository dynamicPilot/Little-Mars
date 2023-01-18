using LittleMars.Common;
using LittleMars.Common.Signals;
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
        // indicator
        float _targetValue;
        string _format = "F0";

        public void SetTargetValue(float targetValue)
        {
            _targetValue = targetValue;
            _targetCounter.text = _targetValue.ToString(_format);
            // update indicator
        }

        public override void UpdateSlot(float number)
        {
            base.UpdateSlot(number);

        }

        public void UpdateSlot(GoalUpdatedSignal args)
        {
            if (args.Values.Length == 0) return;
            Debug.Log("UPDATE!!!");
            UpdateSlot(args.Values[0]);
        }

        public class Factory : PlaceholderFactory<GoalSlotUI>
        {
        }
    }

    
}


