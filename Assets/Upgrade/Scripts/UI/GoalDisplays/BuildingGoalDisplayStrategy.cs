using LittleMars.Model.GoalDisplays;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalDisplays
{
    public class BuildingGoalDisplayStrategy : IGoalDisplayStrategy
    {
        BuildingSlotUISetter _buildingSetter;
        IGoalInfo _goalInfo;

        public BuildingGoalDisplayStrategy(BuildingSlotUISetter buildingSetter, IGoalInfo goalInfo)
        {
            _buildingSetter = buildingSetter;
            _goalInfo = goalInfo;
        }

        public void Dispose()
        {
        }

        public void SetBuildingPart(BuildingSlotUI part)
        {
            ShowPart(part.gameObject);
            _buildingSetter.SetSlot(part, _goalInfo.GetIconIndex());
            part.SetSize(_goalInfo.GetSize());

            part.UpdateSlot(_goalInfo.GetAmount()[0]);
        }

        public void SetResourcePart(ResourceAndGoalTypeSlotUI part)
        {
            HidePart(part.gameObject);
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

        public class Factory : PlaceholderFactory<IGoalInfo, BuildingGoalDisplayStrategy>
        { }
    }
}
