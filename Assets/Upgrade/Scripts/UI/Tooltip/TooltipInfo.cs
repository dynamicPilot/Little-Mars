using UnityEngine;

namespace LittleMars.UI.Tooltip
{
    /// <summary>
    /// Base MonoBehavior class for TooltipController text ui.s
    /// </summary>
    public class TooltipInfo : MonoBehaviour
    {
        protected string _text = "";
        public string GetText()
        {
            SetText();
            return _text;
        }

        protected virtual void SetText()
        {
            // must be override for every implementation
            // set _text variable here
        }
    }
}
