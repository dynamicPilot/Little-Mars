using LittleMars.Common;
using LittleMars.Common.Catalogues;

namespace LittleMars.UI.SlotUIFactories
{
    public class GoalTypeUISetter : ISetSlot
    {
        readonly IconsCatalogue _catalogue;

        public GoalTypeUISetter(IconsCatalogue catalogue)
        {
            _catalogue = catalogue;
        }

        public void SetSlot(SlotUI slot, int type)
        {
            slot.SetSlot(_catalogue.GoalTypeIcon((GoalType)type));
        }
    }
}
