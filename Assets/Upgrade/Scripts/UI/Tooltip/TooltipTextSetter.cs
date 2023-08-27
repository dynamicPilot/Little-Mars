using UnityEngine;

namespace LittleMars.UI.Tooltip
{
    public class TooltipTextSetter : MonoBehaviour
    {
        [SerializeField] TooltipUI _tooltipUI;

        public void SetText(string text)
        {
            _tooltipUI.SetText(text);
        }

    }
}
