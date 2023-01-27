using LittleMars.UI.ResourceSlots;
using System;

namespace LittleMars.UI.GoalDisplays
{
    public interface IGoalDisplayStrategy : IDisposable
    {
        void SetBuildingPart(BuildingSlotUI part);
        void SetResourcePart(ResourceAndGoalTypeSlotUI part);
        void SetTimerPart(ResourceSlotUI part);

    }
}
