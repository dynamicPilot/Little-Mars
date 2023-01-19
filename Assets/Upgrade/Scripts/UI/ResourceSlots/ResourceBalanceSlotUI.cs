using TMPro;
using UnityEngine;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceBalanceSlotUI : SlotUI
    {
        [SerializeField] private TextMeshProUGUI _plusCounter;
        [SerializeField] private TextMeshProUGUI _minusCounter;
        [SerializeField] private string _plusPrefix = "+";
        [SerializeField] private string _minusPrefix = "-";

        protected string _format = "F0";
        public override void SetSlot(Sprite icon)
        {
            base.SetSlot(icon);
            UpdateSlot(0f ,0f);
        }

        public virtual void UpdateSlot(float plusNumber, float minusNumber)
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
    }
}
