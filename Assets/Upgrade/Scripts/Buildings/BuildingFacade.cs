using LittleMars.Buildings.Parts;
using LittleMars.Buildings.States;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings
{
    public class BuildingFacade : MonoBehaviour, IBuildingFacade
    {
        BuildingType _type;
        Size _size;
        BuildingState _state;
        BuildingOperation _operation;

        [Inject]
        public void Constructor(BuildingState state, BuildingOperation operation,
            BuildingType type, Size size)
        {
            _state = state;
            _operation = operation;
            _type = type;
            _size = size;
        }

        public Priority Priority() => Common.Priority.ultimate;
        public OperationMode OperationMode() => _state.OperationMode;
        public ProductionState State() => _state.State;
        public Dictionary<Resource, Dictionary<Period, float>> Production() => _operation.Production;
        public ResourceUnit<float>[] Needs() => _operation.Needs();

        public void ChangeState(ProductionState state, OperationMode mode)
        {
            _state.ChangeState(state, mode);
        }

        public bool HasAllConnections()
        {
            return true;
        }

        public bool HasNeedForThisResource(Resource resource, Period period)
        {
            return _operation.HasNeedForThisResource(resource, period);
        }

        public ProductionState StateForPeriod(Period period)
        {
            return _state.StateForPeriod(period);
        }

        public WithSizeUnit<BuildingType, Size> Info()
        {
            return new WithSizeUnit<BuildingType, Size>() { Type = _type, Size = _size};
        }

        public class Factory : PlaceholderFactory<BuildingType, Size, BuildingFacade>
        {

        }
    }
}
