using LittleMars.Slots.States;

namespace LittleMars.Slots.States
{
    public class ViewSlotStateManager
    {
        SlotStateFactory _stateFactory;

        ISlotState _emptyState = null;
        ISlotState _buildingState = null;
        ISlotState _waitingState = null;
        ISlotState _placingState = null;
        public ViewSlotStateManager(SlotStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public ISlotState State(SlotStates state)
        {
            if (state == SlotStates.empty)
            {
                _emptyState ??= _stateFactory.CreateState(state);
                return _emptyState;
            }
            else if (state == SlotStates.waiting)
            {
                _waitingState ??= _stateFactory.CreateState(state);
                return _waitingState;
            }
            else if (state == SlotStates.placing)
            {
                _placingState ??= _stateFactory.CreateState(state);
                return _placingState;
            }
            else if (state == SlotStates.building)
            {
                _buildingState ??= _stateFactory.CreateState(state);
                return _buildingState;
            }

            return null;
        }
    }
}

