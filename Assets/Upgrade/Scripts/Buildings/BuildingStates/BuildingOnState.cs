using LittleMars.Buildings.View;
using LittleMars.Common;
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
    public class BuildingOnStateFactory : IFactory<BuildingOnState>
    {
        BuildingType _type;
        BuildingOnState.Factory _factory;
        DomeOnState.Factory _domeFactory;

        public BuildingOnStateFactory(BuildingType type, BuildingOnState.Factory factory, 
            DomeOnState.Factory domeFactory)
        {
            _type = type;
            _factory = factory;
            _domeFactory = domeFactory;
        }

        public BuildingOnState Create()
        {
            if (_type == BuildingType.dome) return _domeFactory.Create();
            else return _factory.Create();
        }
    }

    public class BuildingOffStateFactory : IFactory<BuildingOffState>
    {
        BuildingType _type;
        BuildingOffState.Factory _factory;
        DomeOffState.Factory _domeFactory;

        public BuildingOffStateFactory(BuildingType type, BuildingOffState.Factory factory, 
            DomeOffState.Factory domeFactory)
        {
            _type = type;
            _factory = factory;
            _domeFactory = domeFactory;
        }

        public BuildingOffState Create()
        {
            if (_type == BuildingType.dome) return _domeFactory.Create();
            else return _factory.Create();
        }
    }

    public class BuildingPausedStateFactory : IFactory<BuildingPausedState>
    {
        BuildingType _type;
        BuildingPausedState.Factory _factory;
        DomePausedState.Factory _domeFactory;

        public BuildingPausedStateFactory(BuildingType type, BuildingPausedState.Factory factory, 
            DomePausedState.Factory domeFactory)
        {
            _type = type;
            _factory = factory;
            _domeFactory = domeFactory;
        }

        public BuildingPausedState Create()
        {
            if (_type == BuildingType.dome) return _domeFactory.Create();
            else return _factory.Create();
        }
    }



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

        public void OnRemove()
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
