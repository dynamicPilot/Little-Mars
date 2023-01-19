using LittleMars.UI.SlotUIFactories;
using Zenject;
using UnityEngine;

namespace LittleMars.UI.GoalSlots
{
    public class ResourceGoalSlotUI : GoalSlotUI
    {
        [SerializeField] private SlotUI _goalTypeSlot;
        public void SetGoalType(ISetSlot setter, int type)
        {
            setter.SetSlot(_goalTypeSlot, type);
        }
        public class Factory : PlaceholderFactory<ResourceGoalSlotUI>
        {
        }
    }
}
