using LittleMars.Model.GoalDisplays;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalDisplays
{
    public class BuildingTimerStaffGoalDisplayStrategy : IGoalDisplayStrategy
    {
        BuildingSlotUISetter _buildingSetter;
        TimerSlotUISetter _timerSetter;
        IGoalInfo _goalInfo;

        public BuildingTimerStaffGoalDisplayStrategy(BuildingSlotUISetter buildingSetter, 
            TimerSlotUISetter timerSetter, IGoalInfo goalInfo)
        {
            _buildingSetter = buildingSetter;
            _timerSetter = timerSetter;
            _goalInfo = goalInfo;
        }

        public void Dispose()
        {
        }

        public void SetBuildingPart(BuildingSlotUI part)
        {
            ShowPart(part.gameObject);
            _buildingSetter.SetSlot(part, _goalInfo.GetIconIndex());
            part.HideSize(); // or it could be displayed

            part.UpdateSlot(_goalInfo.GetAmount()[0]);
        }

        public void SetResourcePart(ResourceAndGoalTypeSlotUI part)
        {
            HidePart(part.gameObject);
        }

        public void SetTimerPart(ResourceSlotUI part)
        {
            ShowPart(part.gameObject);
            _timerSetter.SetSlot(part);
            part.UpdateSlot(_goalInfo.GetAmount()[1]);
        }

        private void HidePart(GameObject part)
        {
            part.SetActive(false);
        }

        private void ShowPart(GameObject part)
        {
            part.SetActive(true);
        }

        public class Factory : PlaceholderFactory<IGoalInfo, BuildingTimerStaffGoalDisplayStrategy>
        {
        }
    }
}
