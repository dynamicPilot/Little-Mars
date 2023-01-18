using LittleMars.Common.Signals;
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

        // indicator
        float _firstTargetValue;
        float _secondTargetValue;
        string _format = "F0";

        public void SetTargetValue(float firstValue, float secondValue)
        {
            _firstTargetValue = firstValue;
            _secondTargetValue = secondValue;

            _firstTargetCounter.text = _firstTargetValue.ToString(_format);
            _secondTargetCounter.text = _secondTargetValue.ToString(_format);
            // update indicator
        }

        public override void UpdateSlot(float plusNumber, float minusNumber)
        {
            base.UpdateSlot(plusNumber, minusNumber);

        }

        public void UpdateSlot(GoalUpdatedSignal args)
        {
            if (args.Values.Length < 2) return;
            UpdateSlot(args.Values[0], args.Values[1]);
        }

        public class Factory : PlaceholderFactory<GoalWithTimerSlotUI>
        {
        }
    }
}
