
using LittleMars.Common;
using LittleMars.Slots.UI;
using LittleMars.Slots.Views;
using Zenject;

namespace LittleMars.Slots.States
{
    public class SlotPlacingState : ISlotState
    {
        ViewSlotView _view;
        ViewSlotUI _viewUI;
        ViewSlotFacade _facade;
        public SlotPlacingState(ViewSlotView view, ViewSlotUI viewUI, ViewSlotFacade facade)
        {
            _view = view;
            _viewUI = viewUI;
            _facade = facade;
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
            _view.PlacingState();
            _viewUI.HideSigns();
        }

        public class Factory : PlaceholderFactory<SlotPlacingState>
        {
        }
    }
}
