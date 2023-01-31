using System.Collections.Generic;


namespace LittleMars.Common.Interfaces
{
    /// <summary>
    /// Interface for Building Facade.
    /// </summary>
    public interface IBuildingFacade
    {
        void OnRemove();
        void OnStart();
        WithSizeUnit<BuildingType, Size> Info();
        OperationMode OperationMode();
        Priority Priority();
        States State();
        Dictionary<Resource, Dictionary<Period, float>> Production();
        ResourceUnit<float>[] Needs();
        void ChangeState(States state, OperationMode mode);
        bool HasNeedForThisResource(Resource resource);
        States StateForPeriod(Period period);
        void ChangeStateForPeriod(Period period);
        IEnumerable<Indexes> MapSlotIndexes();
        void SetMapSlotIndexes(IEnumerable<Indexes> indexes);
        BuildingType[] Connections();
        void Rotate(float angle);
    }


}

