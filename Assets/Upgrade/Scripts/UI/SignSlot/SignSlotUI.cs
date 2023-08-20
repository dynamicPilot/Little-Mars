using TMPro;
using UnityEngine;

namespace LittleMars.UI.SignSlot
{

    public class SignSlotUI : SlotUI
    {
        [SerializeField] TextMeshProUGUI _text;

        private void OnValidate()
        {
            _sign.enabled = false;
        }

        public override void SetSlot(Sprite icon)
        {
            base.SetSlot(icon);
            _text.text = "";
        }

        public virtual void SetText(string text)
        {
            _text.text = text;
        }

    }
}
