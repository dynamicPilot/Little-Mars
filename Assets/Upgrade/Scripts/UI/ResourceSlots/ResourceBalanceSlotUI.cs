using TMPro;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceBalanceSlotUI : SlotUI
    {
        [SerializeField] private TextMeshProUGUI _plusCounter;
        [SerializeField] private TextMeshProUGUI _minusCounter;
        [SerializeField] private string _plusPrefix = "+";
        [SerializeField] private string _minusPrefix = "-";

        string _format = "F0";
        public override void SetSlot(Sprite icon)
        {
            base.SetSlot(icon);
            _plusCounter.text = "+0";
            _minusCounter.text = "-0";
        }

        public void UpdateSlot(float plusNumber, float minusNumber)
        {
            UpdatePlusValue(plusNumber);
            UpdateMinusValue(minusNumber);
        }

        public void UpdatePlusValue(float number)
        {
            _plusCounter.text = string.Format($"{_plusPrefix}{number.ToString(_format)}");
        }

        public void UpdateMinusValue(float number)
        {
            _minusCounter.text = string.Format($"{_minusPrefix}{number.ToString(_format)}");
        }

        public class Factory : PlaceholderFactory<ResourceBalanceSlotUI>
        {

        }
    }
}
