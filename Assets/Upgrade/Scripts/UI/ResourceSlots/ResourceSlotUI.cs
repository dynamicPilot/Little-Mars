using TMPro;
using UnityEngine;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceSlotUI : SlotUI
    {
        [SerializeField] private TextMeshProUGUI _counter;

        string _prefix = "";
        private void OnValidate()
        {
            _sign.enabled = false;
        }

        public override void SetSlot(Sprite icon)
        {
            base.SetSlot(icon);
            _counter.text = "0";
        }

        public void SetPrefix(string prefix)
        {
            _prefix = prefix;
        }

        public virtual void UpdateSlot(float number)
        {
            _counter.text = string.Format($"{_prefix}{number.ToString("F0")}");
        }
    }
}

