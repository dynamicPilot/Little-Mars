using TMPro;
using UnityEngine;

namespace LittleMars.UI.Tooltip
{
    public class TooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.SetText(text);
        }
    }
}
