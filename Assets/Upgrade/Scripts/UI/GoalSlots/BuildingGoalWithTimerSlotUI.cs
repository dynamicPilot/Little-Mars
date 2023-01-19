using LittleMars.UI.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
