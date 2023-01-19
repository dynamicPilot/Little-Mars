using LittleMars.UI.Effects;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class BuildingGoalSlotUI : GoalSlotUI, ISetSize
    {
        [SerializeField] private SizeUIEffect _size;

        public void SetSize(int index)
        {
            _size.SetSize(index);
        }

        public class Factory : PlaceholderFactory<BuildingGoalSlotUI>
        {
        }
    }
}
