using LittleMars.Common;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class SlotUIFactory<T> where T : SlotUI
    {
        readonly IconsCatalogue _catalogue;
        readonly PlaceholderFactory<T> _factory;

        public SlotUIFactory(IconsCatalogue catalogue,
            PlaceholderFactory<T> factory)
        {
            _catalogue = catalogue;
            _factory = factory;
        }

        public T CreateSlot(Resource type, RectTransform container)
        {
            var slot = _factory.Create();
            slot.transform.SetParent(container);
            slot.transform.SetSiblingIndex(1);
            slot.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
            slot.SetSlot(_catalogue.ResourceIcon(type));
            return slot;
        }
    }

    public interface ISetSlot
    {
        void SetSlot(SlotUI slot, int type);
    }

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

    public class BuildingSlotUISetter : ISetSlot
    {
        readonly IconsCatalogue _catalogue;

        public BuildingSlotUISetter(IconsCatalogue catalogue)
        {
            _catalogue = catalogue;
        }

        public void SetSlot(SlotUI slot, int typeIndex)
        {
            //slot.SetSlot(_catalogue.ResourceIcon((Resource)typeIndex));
        }
    }
}
