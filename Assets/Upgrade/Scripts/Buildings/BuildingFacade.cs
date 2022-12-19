using LittleMars.Buildings.Parts;
using LittleMars.Buildings.States;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using System.Collections;
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
        Building _data;

        [Inject]
        public void Constructor(BuildingState state, BuildingOperation operation, Building data,
            BuildingType type, Size size)
        {
            _state = state;
            _operation = operation;
            _type = type;
            _size = size;
            _data = data;
        }

        public Priority Priority() => Common.Priority.ultimate;
        public OperationMode OperationMode() => _state.OperationMode;
        public ProductionState State() => _state.State;
        public Dictionary<Resource, Dictionary<Period, float>> Production() => _operation.Production;
        public ResourceUnit<float>[] Needs() => _operation.Needs();
        public IEnumerable<Indexes> MapSlotIndexes() => _data.MapSlotIndexes;
        public void SetMapSlotIndexes(IEnumerable<Indexes> indexes) => _data.MapSlotIndexes = indexes;
        public BuildingType[] Connections() => _data.Connections;

        public void ChangeState(ProductionState state, OperationMode mode)
        {
            _state.ChangeState(state, mode);
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
