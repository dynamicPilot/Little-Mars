using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.States
{
    public class BuildingState
    {
        ProductionState _state;
        OperationMode _mode;
        Dictionary<Period, ProductionState> _timetable;

        BuildingStateManager _manager;
        IBuildingState _buildingState;

        public ProductionState State { get => _state; }
        public OperationMode OperationMode { get => _mode; }
        public Dictionary<Period, ProductionState> ProductionTimetable { get => _timetable; }
        public ProductionState StateForPeriod(Period period) => _timetable[period];

        public BuildingState(BuildingStateManager manager)
        {
            _manager = manager;            
        }

        public void OnStart()
        {            
            ChangeState(ProductionState.off, OperationMode.forcedAuto);
            ResetTimetable();
            _buildingState.OnStart();
        }

        public void OnClickPerformed()
        {
            _buildingState.OnClickPerformed();
        }

        public void OnRemove()
        {            
            _buildingState.OnRemove();
        }

        public void ChangeStateForPeriod(Period period)
        {
            var newState = (_timetable[period] == ProductionState.on) ?
                ProductionState.off : ProductionState.on;
            _timetable[period] = newState;
            Debug.Log("New period timetable " + _timetable[period]);
        }

        public void ChangeState(ProductionState state, OperationMode mode)
        {
            if (_state == state) return;

            _state = state;
            _mode = mode;

            _buildingState = _manager.CreateState(state);

            Debug.Log($"BuildingState: state was changed to {_state}");
            _buildingState.SetView();
        }

        private void ResetTimetable()
        {
            _timetable = new Dictionary<Period, ProductionState>
            {
                [Period.day] = ProductionState.on,
                [Period.night] = ProductionState.on
            };
        }

    }


}
