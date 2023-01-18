using LittleMars.Common;

namespace LittleMars.UI.SlotUIFactories
{
    public class ResourceSlotUISetter : ISetSlot
    {
        readonly IconsCatalogue _catalogue;

        public ResourceSlotUISetter(IconsCatalogue catalogue)
        {
            _catalogue = catalogue;
        }

        public void SetSlot(SlotUI slot, int typeIndex)
        {
            slot.SetSlot(_catalogue.ResourceIcon((Resource)typeIndex));
        }

    }
}
