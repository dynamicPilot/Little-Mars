using LittleMars.Common;
using LittleMars.Common.Interfaces;

namespace LittleMars.UI.BuildingSlots
{
    public class BuildingSlotFactory
    {
        readonly BuildingSlotFacade.Factory _factory;
        readonly GameUI _gameUI;
        public BuildingSlotFactory(BuildingSlotFacade.Factory factory,
            GameUI gameUI)
        {
            _factory = factory;
            _gameUI = gameUI;
        }

        public IBuildingSlotFacade CreateSlot(BuildingType type, Size size)
        {
            var slot = _factory.Create(type, size);
            slot.transform.SetParent(_gameUI.BuildingSlotParent());
            slot.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
            return slot;
        }
    }
}
