using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.States
{
    public class BuildingState: IInitializable
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
            _timetable = new Dictionary<Period, ProductionState>
            {
                [Period.day] = ProductionState.on,
                [Period.night] = ProductionState.on
            };
        }

        public void Initialize()
        {
            ChangeState(ProductionState.off, OperationMode.forcedAuto);
        }

        public void OnClickPerformed()
        {
            _buildingState.OnClickPerformed();
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


    }


}
