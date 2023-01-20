using LittleMars.Common;
using LittleMars.UI.BuildingsSlots;
using LittleMars.UI.SlotUIFactories;
using Zenject;

namespace LittleMars.UI.Effects
{
    public class FieldsListUI : IInitializable
    {
        readonly SlotUIFactory<SlotUI> _factory;
        readonly BuildingSlotGameUI _gameUI;
        readonly BuildingObject _building;

        public FieldsListUI(SlotUIFactory<SlotUI> factory,
            BuildingSlotGameUI gameUI, BuildingObject building)
        {
            _factory = factory;
            _gameUI = gameUI;
            _building = building;
        }

        public void Initialize()
        {
            CreateSlots();
        }

        private void CreateSlots()
        {
            for (int i = 0; i < _building.Construction.ResourcesInMap.Length; i++)
            {
                var type = _building.Construction.ResourcesInMap[i];
                _factory.CreateSlot((int)type, _gameUI.ConnectionsSlotParent, i + 1);
            }
        }
    }
}
