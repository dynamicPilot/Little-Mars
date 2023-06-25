using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class BuildingState
    {
        readonly BuildingStateManager _manager;

        States _state;
        OperationMode _mode;
        IBuildingState _buildingState;

        Dictionary<Period, States> _timetable;

        public States State { get => _state; }
        public OperationMode OperationMode { get => _mode; }
        public Dictionary<Period, States> ProductionTimetable { get => _timetable; }
        public States StateForPeriod(Period period) => _timetable[period];

        public BuildingState(BuildingStateManager manager)
        {
            _manager = manager;            
        }

        public void OnStart()
        {
            ChangeState(States.off, OperationMode.forcedAuto);
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
            var newState = (_timetable[period] == States.on) ?
                States.off : States.on;
            _timetable[period] = newState;
            //Debug.Log("New period timetable " + _timetable[period]);
        }

        public void ChangeState(States state, OperationMode mode)
        {
            if (_state == state && _mode != OperationMode.auto) return;
            else if (_state == state && state == States.on) return;

            _state = state;
            _mode = mode;

            _buildingState = _manager.CreateState(GetBState());

            //Debug.Log($"BuildingState: state was changed to {_state}");
            _buildingState.SetView();
        }

        private void ResetTimetable()
        {
            _timetable = new Dictionary<Period, States>
            {
                [Period.day] = States.on,
                [Period.night] = States.on
            };
        }

        private BStates GetBState()
        {
            return (_mode == OperationMode.auto && _state == States.off) ?
                BStates.paused :
                (BStates)((int)_state);
        }

    }
}
