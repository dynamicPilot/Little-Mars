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
}
