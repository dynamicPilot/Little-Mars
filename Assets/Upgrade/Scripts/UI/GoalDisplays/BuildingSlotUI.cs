using LittleMars.UI.Effects;
using LittleMars.UI.GoalSlots;
using LittleMars.UI.ResourceSlots;
using System;
using UnityEngine;

namespace LittleMars.UI.GoalDisplays
{
    public class BuildingSlotUI : ResourceSlotUI, ISetSize
    {
        [SerializeField] private SizeUIEffect _size;

        public void SetSize(int index)
        {
            _size.SetSize(index);
        }

        public void HideSize()
        {
            _size.SetSize(0);
        }
    }
}
