using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{

    public class OnStateFactory : PlaceholderFactory<BuildingOnState>
    { }

    public class OffStateFactory : PlaceholderFactory<BuildingOffState>
    { }

    public class PausedStateFactory : PlaceholderFactory<BuildingPausedState>
    { }

    public class BuildingOnState : IBuildingState
    {
        readonly BuildingView _view;
        readonly BuildingFacade _building;
        readonly IModelFacade _model;
        public BuildingOnState(BuildingView view, IModelFacade model, 
            BuildingFacade building, BuildingState state)
        {
            _view = view;
            _building = building;
            _model = model;
        }

        public void Dispose()
        {
        }
        public void OnClickPerformed()
        {
            _model.CallBuildingController(_building);
        }

        public void OnStart()
        {
            _view.SetViewActiveState(true);
        }

        public virtual void OnRemove()
        {
            _view.SetViewActiveState(false);
        }

        public virtual void SetView()
        {
            _view.TransitToState(Common.BStates.on);
        }

        public class Factory : PlaceholderFactory<BuildingOnState>
        {
        }
    }


}
