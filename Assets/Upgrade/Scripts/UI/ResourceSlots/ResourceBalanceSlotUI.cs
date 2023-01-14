using TMPro;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceBalanceSlotUI : SlotUI
    {
        [SerializeField] private TextMeshProUGUI _plusCounter;
        [SerializeField] private TextMeshProUGUI _minusCounter;

        public override void SetSlot(Sprite icon)
        {
            base.SetSlot(icon);
            _plusCounter.text = "+0";
            _minusCounter.text = "-0";
        }

        public void UpdateSlot(float plusNumber, float minusNumber)
        {
            _plusCounter.text = string.Format("+{0}", plusNumber.ToString("F0"));
            _minusCounter.text = string.Format("+{0}", minusNumber.ToString("F0"));
        }

        public class Factory : PlaceholderFactory<ResourceBalanceSlotUI>
        {

        }
    }
}
