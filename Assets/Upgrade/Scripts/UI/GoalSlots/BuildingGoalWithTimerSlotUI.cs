using LittleMars.UI.Effects;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class BuildingGoalWithTimerSlotUI : GoalWithTimerSlotUI, ISetSize
    {
        [SerializeField] private SizeUIEffect _size;

        public void SetSize(int index)
        {
            _size.SetSize(index);
        }

        public class Factory : PlaceholderFactory<BuildingGoalWithTimerSlotUI>
        { }
    }
}
