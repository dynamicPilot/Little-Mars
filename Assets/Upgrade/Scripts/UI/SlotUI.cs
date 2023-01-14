using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] protected Image _sign;

        private void OnValidate()
        {
            _sign.enabled = false;
        }

        public virtual void SetSlot(Sprite icon)
        {
            _sign.enabled = true;
            _sign.sprite = icon;
        }

    }
}
