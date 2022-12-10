namespace LittleMars.Slots.States
{
    public enum SlotStates
    {
        empty,
        waiting,
        placing,
        building
    }

    public class SlotStateFactory
    {
        readonly SlotEmptyState.Factory _emptyStateFactory;
        readonly SlotBuildingState.Factory _buildingStateFactory;
        readonly SlotWaitingState.Factory _waitingStateFactory;
        readonly SlotPlacingState.Factory _placingStateFactory;

        public SlotStateFactory(SlotEmptyState.Factory emptyStateFactory, 
            SlotBuildingState.Factory buildingOnStateFactory,
            SlotWaitingState.Factory waitingStateFactory,
            SlotPlacingState.Factory placingStateFactory)
        {
            _emptyStateFactory = emptyStateFactory;
            _buildingStateFactory = buildingOnStateFactory;
            _waitingStateFactory = waitingStateFactory;
            _placingStateFactory = placingStateFactory;
        }

        public ISlotState CreateState(SlotStates state)
        {
            //Debug.Log("Create state for " + state);
            if (state == SlotStates.empty)
            {
                return _emptyStateFactory.Create();
            }
            else if (state == SlotStates.waiting)
            {
                return _waitingStateFactory.Create();
            }
            else if (state == SlotStates.placing)
            {
                return _placingStateFactory.Create();
            }
            else if (state == SlotStates.building)
            {
                return _buildingStateFactory.Create();
            }

            return null;
        }
    }
}
