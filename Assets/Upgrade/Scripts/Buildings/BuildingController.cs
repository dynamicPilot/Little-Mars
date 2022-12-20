using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Models;
using LittleMars.UI;
using Zenject;

namespace LittleMars.Buildings
{
    public class BuildingController
    {
        IBuildingFacade _building;
        readonly OperationManager _operation;
        readonly BuildingManager _manager;

        public BuildingController(OperationManager operation, BuildingManager manager)
        {
            _operation = operation;
            _manager = manager;
        }

        public void StartController(IBuildingFacade building)
        {
            if (_building != null) EndController();
            _building = building;
        }

        public void TryChangeState()
        {
            var newState = (_building.State() == ProductionState.on) ? ProductionState.off : ProductionState.on;
            _operation.TryChangeBuildingState(_building, newState, OperationMode.manual);
        }

        public void ChangeTimetable(Period period)
        {
            // change timetable in building
            _building.ChangeStateForPeriod(period);
            _operation.OnBuildingTimetableChange(_building);
        } 

        public void Remove()
        {
            _manager.RemoveBuilding(_building);
            EndController();
        }

        public void EndController()
        {
            _building = null;
        }

    }
}
