using TMPro;
using UnityEngine;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceSlotUI : SlotUI
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private void OnValidate()
        {
            _sign.enabled = false;
        }

        public override void SetSlot(Sprite icon)
        {
            base.SetSlot(icon);
            _counter.text = "0";
        }

        public virtual void UpdateSlot(float number)
        {
            _counter.text = number.ToString("F0");
        }
    }
}

