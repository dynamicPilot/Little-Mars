using LittleMars.Common;
using LittleMars.UI;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceSlotUIFactory
    {
        readonly IconsCatalogue _catalogue;
        readonly ResourceSlotUI.Factory _factory;
        readonly GameUI _gameUI;

        public ResourceSlotUIFactory(IconsCatalogue catalogue, ResourceSlotUI.Factory factory, GameUI gameUI)
        {
            _catalogue = catalogue;
            _factory = factory;
            _gameUI = gameUI;
        }

        public ResourceSlotUI CreateSlot(Resource type)
        {
            var slot = _factory.Create();
            slot.transform.SetParent(_gameUI.ResourceSlotParent);
            slot.SetSlot(_catalogue.ResourceIcon(type));
            return slot;
        }
    }
}
