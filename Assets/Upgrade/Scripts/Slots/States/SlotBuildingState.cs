
using LittleMars.Common;
using LittleMars.Slots.UI;
using LittleMars.Slots.Views;
using Zenject;

namespace LittleMars.Slots.States
{

    public class SlotBuildingState : ISlotState
    {
        readonly ViewSlotView _view;
        //ViewSlotUI _viewUI;
        public SlotBuildingState(ViewSlotView view)
        {
            _view = view;
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
        }

        public class Factory : PlaceholderFactory<SlotBuildingState>
        {

        }
    }
}
