using UnityEngine;
using Zenject;

namespace LittleMars.UI.SlotUIFactories
{
    public class SlotUIFactory<T> where T : SlotUI
    {
        readonly PlaceholderFactory<T> _factory;
        readonly ISetSlot _setter;
        public SlotUIFactory(PlaceholderFactory<T> factory, ISetSlot setter)
        {
            _factory = factory;
            _setter = setter;
        }

        public T CreateSlot(int type, RectTransform container, int siblingIndex = 1)
        {
            var slot = _factory.Create();
            slot.transform.SetParent(container);
            slot.transform.SetSiblingIndex(siblingIndex);
            slot.transform.localScale = new Vector3(1f, 1f, 1f);

            _setter.SetSlot(slot, type);
            return slot;
        }
    }
}

