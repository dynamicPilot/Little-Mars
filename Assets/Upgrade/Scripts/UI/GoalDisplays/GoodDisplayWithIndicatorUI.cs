using LittleMars.Animations;
using UnityEngine;

namespace LittleMars.UI.GoalDisplays
{
    public class GoodDisplayWithIndicatorUI : GoalDisplayUI
    {
        [Header("Effects")]
        [SerializeField] private SliderFillingAnimation _sliderAnimation;

        public override void SetSlot(IGoalDisplayStrategy strategy)
        {
            base.SetSlot(strategy);
            _sliderAnimation.StartAnimation();
        }
    }
}
