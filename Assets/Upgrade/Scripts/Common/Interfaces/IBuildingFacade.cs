using System.Collections.Generic;


namespace LittleMars.Common.Interfaces
{
    /// <summary>
    /// Interface for Building Facade.
    /// </summary>
    public interface IBuildingFacade
    {
        WithSizeUnit<BuildingType, Size> Info();
        OperationMode OperationMode();
        Priority Priority();
        ProductionState State();
        Dictionary<Resource, Dictionary<Period, float>> Production();
        ResourceUnit<float>[] Needs();
        void ChangeState(ProductionState state, OperationMode mode);
        bool HasNeedForThisResource(Resource resource);
        ProductionState StateForPeriod(Period period);
        void ChangeStateForPeriod(Period period);
        IEnumerable<Indexes> MapSlotIndexes();
        void SetMapSlotIndexes(IEnumerable<Indexes> indexes);
        BuildingType[] Connections();
    }


}

