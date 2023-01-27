using LittleMars.Common;

namespace LittleMars.UI.SlotUIFactories
{
    public class TimerSlotUISetter : ISetSlot
    {
        readonly IconsCatalogue _catalogue;

        public TimerSlotUISetter(IconsCatalogue catalogue)
        {
            _catalogue = catalogue;
        }

        public void SetSlot(SlotUI slot, int type = 0)
        {
            slot.SetSlot(_catalogue.TimerTypeIcon());
        }
    }
}
