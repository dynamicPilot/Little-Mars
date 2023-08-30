using LittleMars.Common;
using LittleMars.TooltipSystem;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    /// <summary>
    /// Class to get text for the goal tooltip.
    /// </summary>
    public class GoalToolipInfo : TooltipInfo
    {
        int _index;
        bool _withTimer;
        GoalTooltipTextGetter _textForGoal;

        [Inject]
        public void Constructor(GoalTooltipTextGetter textForGoal)
        {
            _textForGoal = textForGoal;
        }

        public void SetTooltipInfo(int index, bool withTimer)
        {
            _withTimer = withTimer;
            _index = index;
        }

        protected override void SetText()
        {
            _text = _textForGoal.GetText(_index, _withTimer);
        }
    }
}
