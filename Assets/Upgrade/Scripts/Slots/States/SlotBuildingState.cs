
using LittleMars.Common;
using LittleMars.Connections.View;
using LittleMars.Slots.UI;
using LittleMars.Slots.Views;
using Zenject;

namespace LittleMars.Slots.States
{

    public class SlotBuildingState : ISlotState
    {
        readonly ViewSlotView _view;
        readonly ConnectionIndicators _indicators;
        public SlotBuildingState(ViewSlotView view, ConnectionIndicators indicators)
        {
            _view = view;
            _indicators = indicators;
        }

        public void Dispose()
        {
        }

        public void OnDrop(BuildingObject buildingObject)
        {
            return;
        }

        public void SetView()
        {
            _view.BuildingState();
            _indicators.ChangeCanShowState(true);
        }

        public class Factory : PlaceholderFactory<SlotBuildingState>
        {

        }
    }
}
