using LittleMars.UI.Effects;
using TMPro;
using UnityEngine;

namespace LittleMars.UI.SignSlot
{
    public class SignSlotUI : SlotUI
    {
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] SizeUIEffect _size;

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

        public void SetSize(int index)
        {
            _size.SetSize(index);
        }

        public void HideSize()
        {
            _size.SetSize(0);
        }

    }
}
