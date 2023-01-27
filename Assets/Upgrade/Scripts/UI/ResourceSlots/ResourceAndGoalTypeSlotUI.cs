using LittleMars.UI.SlotUIFactories;
using UnityEngine;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceAndGoalTypeSlotUI : ResourceSlotUI
    {
        [SerializeField] private SlotUI _goalTypeSlot;
        public void SetGoalType(ISetSlot setter, int type)
        {
            setter.SetSlot(_goalTypeSlot, type);
        }
    }
}
