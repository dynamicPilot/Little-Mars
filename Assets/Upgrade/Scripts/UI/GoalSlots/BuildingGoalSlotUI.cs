using LittleMars.UI.Effects;
using LittleMars.UI.Tooltip;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class BuildingGoalSlotUI : GoalSlotUI, ISetSize
    {
        [SerializeField] SizeUIEffect _size;
        //[SerializeField] GoalToolipInfo _tooltip;

        public void SetSize(int index)
        {
            _size.SetSize(index);
        }

        public class Factory : PlaceholderFactory<BuildingGoalSlotUI>
        {
        }
    }
}
