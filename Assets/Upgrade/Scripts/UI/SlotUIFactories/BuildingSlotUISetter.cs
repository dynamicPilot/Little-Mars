using LittleMars.Common;
using UnityEngine;

namespace LittleMars.UI.SlotUIFactories
{
    public class BuildingSlotUISetter : ISetSlot
    {
        readonly IconsCatalogue _catalogue;

        public BuildingSlotUISetter(IconsCatalogue catalogue)
        {
            _catalogue = catalogue;
        }

        public void SetSlot(SlotUI slot, int typeIndex)
        {
            slot.SetSlot(_catalogue.BuildingIcon((BuildingType)typeIndex));
        }
    }
}
