using LittleMars.TooltipSystem;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    /// <summary>
    /// Class to get text for the goal tooltip.
    /// </summary>
    public class GoalTooltipControllerTextUI : TooltipControllerTextUI
    {
        [SerializeField] int _index;

        TooltipControllerTextForGoal _textForGoal;

        [Inject]
        public void Constructor(TooltipControllerTextForGoal textForGoal)
        {
            _textForGoal = textForGoal;
        }

        protected override void SetText()
        {
            _text = _textForGoal.GetText(_index);
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

    }
}
