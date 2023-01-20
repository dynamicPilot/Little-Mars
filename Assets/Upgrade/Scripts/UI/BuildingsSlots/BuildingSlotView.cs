using LittleMars.Common;
using LittleMars.UI.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.BuildingSlots
{
    public class BuildingSlotView : MonoBehaviour
    {
        [SerializeField] private CounterSlotUI _amount;

        public void UpdateAmount(int amount)
        {
            _amount.UpdateCounter(amount);
        }
    }
}
