﻿using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Models;
using Zenject;

namespace LittleMars.Buildings
{
    public class BuildingController
    {
        readonly OperationManager _operation;
        readonly BuildingManager _manager;
        readonly SignalBus _signalBus;
        public BuildingController(OperationManager operation, BuildingManager manager, SignalBus signalBus)
        {
            _operation = operation;
            _manager = manager;
            _signalBus = signalBus;
        }

        public void CallController(IBuildingFacade building)
        {
            if (building == null) return;
            _signalBus.Fire(new BuildingControllerOpenSignal { BuildingFacade = building });
        }

        public void TryChangeState(IBuildingFacade building)
        {
            var newState = (building.State() == States.on) ? States.off : States.on;
            _operation.TryChangeBuildingState(building, newState, OperationMode.manual);
        }

        public void ChangeTimetable(IBuildingFacade building, Period period)
        {
            // change timetable in building
            building.ChangeStateForPeriod(period);
            _operation.OnBuildingTimetableChanged(building);
        }

        public void Remove(IBuildingFacade building)
        {
            _manager.RemoveBuilding(building);
        }
    }
}
