using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using System;
using UnityEngine.EventSystems;
using Zenject;

namespace LittleMars.Buildings.States
{
    public class BuildingOffState : IBuildingState
    {
        readonly BuildingView _view;
        readonly BuildingFacade _building;
        readonly IModelFacade _model;
        public BuildingOffState(BuildingView view, IModelFacade model, BuildingFacade building)
        {
            _view = view;
            _model = model;
            _building = building;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnClickPerformed()
        {
            _model.StartBuildingControl(_building);
        }

        public void SetView()
        {
            _view.OffView();
        }

        public void OnRemove()
        {

        }


        public class Factory : PlaceholderFactory<BuildingOffState>
        {
        }
    }


}
