using LittleMars.Common;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LittleMars.UI.BuildingsSlots
{
    public class ResourceListFactory
    {
        readonly SlotUIFactory<ResourceSlotUI> _factory;

        public ResourceListFactory(SlotUIFactory<ResourceSlotUI> factory)
        {
            _factory = factory;
        }

        public void CreateSlots(RectTransform container, ResourceUnit<float>[] resources, string prefix = "")
        {
            for (int i = 0; i < resources.Length; i++)
            {
                var unit = resources[i];
                var slot = _factory.CreateSlot((int)unit.Type, container, i + 1);
                slot.SetPrefix(prefix);
                slot.UpdateSlot(unit.Amount);
            }
        }
    }
}
