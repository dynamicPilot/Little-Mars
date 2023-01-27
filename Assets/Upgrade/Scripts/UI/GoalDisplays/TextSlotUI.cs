using TMPro;
using UnityEngine;

namespace LittleMars.UI.GoalDisplays
{
    public class TextSlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        string _format = "F0";
        public void SetSlot()
        {
            _counter.text = "0";
        }

        public void UpdateSlot(float number)
        {
            _counter.text = number.ToString(_format);
        }
    }
}
