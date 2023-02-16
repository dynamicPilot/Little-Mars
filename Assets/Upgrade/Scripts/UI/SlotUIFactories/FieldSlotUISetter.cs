using LittleMars.Common;
using LittleMars.Common.Catalogues;

namespace LittleMars.UI.SlotUIFactories
{
    public class FieldSlotUISetter : ISetSlot
    {
        readonly IconsCatalogue _catalogue;

        public FieldSlotUISetter(IconsCatalogue catalogue)
        {
            _catalogue = catalogue;
        }

        public void SetSlot(SlotUI slot, int typeIndex)
        {
            slot.SetSlot(_catalogue.FieldTypeIcon((Resource)typeIndex));
        }
    }
}
