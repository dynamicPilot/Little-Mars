using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using System;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
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
            throw new NotImplementedException();
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

        public void SetView()
        {
            _view.OnView();
        }

        public class Factory : PlaceholderFactory<BuildingOnState>
        {
        }
    }


}
