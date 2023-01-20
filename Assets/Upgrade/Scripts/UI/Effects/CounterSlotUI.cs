using TMPro;
using UnityEngine;

namespace LittleMars.UI.Effects
{
    /// <summary>
    /// Component for TMP Counter Text.
    /// </summary>
    public class CounterSlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        public void UpdateCounter(int value)
        {
            _counter.text = value.ToString();
        }
    }
}
