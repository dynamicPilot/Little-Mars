using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceSlotUI : MonoBehaviour
    {
        [SerializeField] private Image _sign;
        [SerializeField] private TextMeshProUGUI _counter;

        private void OnValidate()
        {
            _sign.enabled = false;
        }

        public void SetSlot(Sprite icon)
        {
            _sign.enabled = true;
            _sign.sprite = icon;
            _counter.text = "0";
        }

        public void UpdateSlot(string number)
        {
            _counter.text = number;
        }

        public class Factory : PlaceholderFactory<ResourceSlotUI>
        {

        }
    }
}

