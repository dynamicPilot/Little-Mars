using LittleMars.Common;
using Zenject;

namespace LittleMars.Slots.States
{
    public class ViewSlotState : IInitializable
    {
        ViewSlotStateManager _manager;
        ISlotState _state;

        public ViewSlotState(ViewSlotStateManager manager)
        {
            _manager = manager;
        }

        public void Initialize()
        {
            ChangeState(SlotStates.empty);
        }

        public void ChangeState(SlotStates state)
        {
            _state = _manager.State(state);
            _state.SetView();
        }

        public void OnDrop(BuildingObject buildingObject)
        {
            _state.OnDrop(buildingObject);
        } 
    }
}

