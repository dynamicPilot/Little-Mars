namespace LittleMars.Common.Interfaces
{
    public interface IBuildingSlotFacade
    {
        void SetActiveState(ProductionState state);
        void UpdateSlotAmount(int amount);
    }


}

