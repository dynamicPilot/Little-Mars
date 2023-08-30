using LittleMars.Animations;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.Tooltip;
using UnityEngine;

namespace LittleMars.UI.GoalDisplays
{
    public class GoalDisplayUI : MonoBehaviour
    {
        [SerializeField] BuildingSlotUI _buildingPart;
        [SerializeField] ResourceAndGoalTypeSlotUI _resourcePart;
        [SerializeField] ResourceSlotUI _timerPart;
        [SerializeField] GoalToolipInfo _tooltipInfo;

        public virtual void SetSlot(IGoalDisplayStrategy strategy, int goalIndex = -1)
        {
            strategy.SetBuildingPart(_buildingPart);
            strategy.SetResourcePart(_resourcePart);
            strategy.SetTimerPart(_timerPart);
            strategy.SetTooltip(_tooltipInfo, goalIndex);
        }

    }
}
