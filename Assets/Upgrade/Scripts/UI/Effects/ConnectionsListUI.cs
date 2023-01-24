using LittleMars.Common;
using LittleMars.UI.BuildingsSlots;
using LittleMars.UI.SlotUIFactories;
using Zenject;

namespace LittleMars.UI.Effects
{
    public class ConnectionsListUI : IInitializable
    {
        readonly SlotUIFactory<SlotUI> _factory;
        readonly BuildingSlotGameUI _gameUI;
        readonly BuildingObject _building;

        public ConnectionsListUI(SlotUIFactory<SlotUI> factory,
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
            _gameUI.ConnectionsSlotParent
                .gameObject.SetActive(_building.Operation.Connections.Length > 0);

            for (int i = 0; i < _building.Operation.Connections.Length; i++)
            {
                var type = _building.Operation.Connections[i];
                _factory.CreateSlot((int)type, _gameUI.ConnectionsSlotParent, i + 1);
            }
        }
    }
}
