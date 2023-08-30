using LittleMars.TooltipSystem;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    /// <summary>
    /// Class to get text for the goal tooltip.
    /// </summary>
    public class GoalTooltipControllerTextUI : TooltipInfo
    {
        [SerializeField] int _index;

        GoalTooltipTextGetter _textForGoal;

        [Inject]
        public void Constructor(GoalTooltipTextGetter textForGoal)
        {
            _textForGoal = textForGoal;
        }

        protected override void SetText()
        {
            //_text = _textForGoal.GetText(_index);
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

    }
}
