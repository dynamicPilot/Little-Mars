using LittleMars.Model.GoalDisplays;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using LittleMars.UI.Tooltip;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalDisplays
{
    public class ResourceGoalDisplayStrategy : IGoalDisplayStrategy
    {
        ResourceSlotUISetter _resourceSlotUISetter;
        GoalTypeUISetter _goalTypeSetter;
        IGoalInfo _goalInfo;

        public ResourceGoalDisplayStrategy(ResourceSlotUISetter resourceSlotUISetter,
            GoalTypeUISetter goalTypeSetter,
            IGoalInfo goalInfo)
        {
            _resourceSlotUISetter = resourceSlotUISetter;
            _goalTypeSetter = goalTypeSetter;
            _goalInfo = goalInfo;
        }

        public void Dispose()
        {
            
        }

        public void SetBuildingPart(BuildingSlotUI part)
        {
            HidePart(part.gameObject);
        }

        public void SetResourcePart(ResourceAndGoalTypeSlotUI part)
        {
            ShowPart(part.gameObject);
            _resourceSlotUISetter.SetSlot(part, _goalInfo.GetIconIndex());
            part.SetGoalType(_goalTypeSetter, (int) _goalInfo.GetType());

            part.UpdateSlot(_goalInfo.GetAmount()[0]);
        }

        public void SetTimerPart(ResourceSlotUI part)
        {
            HidePart(part.gameObject);
        }

        private void HidePart(GameObject part)
        {
            part.SetActive(false);
        }

        private void ShowPart(GameObject part)
        {
            part.SetActive(true);
        }
        public void SetTooltip(GoalToolipInfo tooltipInfo, int goalIndex)
        {
            if (tooltipInfo == null) return;
            tooltipInfo.SetTooltipInfo(goalIndex, _goalInfo.WithTimer());
        }

        public class Factory : PlaceholderFactory<IGoalInfo, ResourceGoalDisplayStrategy>
        { }
    }
}
