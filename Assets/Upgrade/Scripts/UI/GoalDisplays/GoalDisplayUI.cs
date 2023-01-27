using LittleMars.Animations;
using LittleMars.UI.ResourceSlots;
using UnityEngine;

namespace LittleMars.UI.GoalDisplays
{
    public class GoalDisplayUI : MonoBehaviour
    {
        [SerializeField] BuildingSlotUI _buildingPart;
        [SerializeField] ResourceAndGoalTypeSlotUI _resourcePart;
        [SerializeField] ResourceSlotUI _timerPart;

        [Header("Effects")]
        [SerializeField] private SliderFillingAnimation _sliderAnimation;

        public void SetSlot(IGoalDisplayStrategy strategy)
        {
            strategy.SetBuildingPart(_buildingPart);
            strategy.SetResourcePart(_resourcePart);
            strategy.SetTimerPart(_timerPart);
            _sliderAnimation.StartAnimation();
        }

    }
}
