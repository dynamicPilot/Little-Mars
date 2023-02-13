using LittleMars.Buildings.Parts;
using LittleMars.Buildings.BuildingStates;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using LittleMars.Buildings.View;

namespace LittleMars.Buildings
{
    public class BuildingFacade : MonoBehaviour, IBuildingFacade
    {
        [SerializeField] private BuildingView _view;

        BuildingType _type;
        Size _size;
        BuildingState _state;
        BuildingOperation _operation;
        BuildingData _data;

        WithSizeUnit<BuildingType, Size> _info;

        [Inject]
        public void Constructor(BuildingState state, BuildingOperation operation, BuildingData data,
            BuildingType type, Size size)
        {
            _state = state;
            _operation = operation;
            _type = type;
            _size = size;
            _data = data;

            _info = new WithSizeUnit<BuildingType, Size>() { Type = _type, Size = _size };

        }

        public void OnStart()
        {
            _state.OnStart();
            _data.OnStart();
        }

        public void OnRemove()
        {
            _state.OnRemove();
            _view.ResetView();
        }
        
        public Priority Priority() => Common.Priority.ultimate;
        public OperationMode OperationMode() => _state.OperationMode;
        public States State() => _state.State;
        public Dictionary<Resource, Dictionary<Period, float>> Production() => _operation.Production;
        public ResourceUnit<float>[] Needs() => _operation.Needs();
        public IEnumerable<Indexes> MapSlotIndexes() => _data.MapSlotIndexes;
        public void SetMapSlotIndexes(IEnumerable<Indexes> indexes) => _data.MapSlotIndexes = indexes;
        public BuildingType[] Connections() => _data.Connections;
        public void ChangeState(States state, OperationMode mode) => _state.ChangeState(state, mode);
        public bool HasNeedForThisResource(Resource resource) => _operation.HasNeedForThisResource(resource);
        public States StateForPeriod(Period period) => _state.StateForPeriod(period);
        public void ChangeStateForPeriod(Period period) => _state.ChangeStateForPeriod(period);
        public WithSizeUnit<BuildingType, Size> Info() => _info;
        public void Rotate(float angle) => _view.Rotate(angle);
        public void OnStartViewEffect() => _view.OnStartViewEffect();
        public void OnEndViewEffect() => _view.OnEndViewEffect();

        public class Factory : PlaceholderFactory<BuildingType, Size, BuildingFacade>
        {

        }
    }
}
