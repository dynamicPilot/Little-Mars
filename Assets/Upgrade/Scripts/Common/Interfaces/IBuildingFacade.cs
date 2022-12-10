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
        bool HasAllConnections();
        bool HasNeedForThisResource(Resource resource, Period period);
        ProductionState StateForPeriod(Period period);
    }


}

