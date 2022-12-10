using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace LittleMars.Buildings.States
{
    public class BuildingOnState : IBuildingState
    {
        readonly BuildingView _view;
        readonly BuildingFacade _building;
        readonly IModelFacade _model;
        public BuildingOnState(BuildingView view, IModelFacade model, BuildingFacade building)
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
            _model.TryChangeBuildingState(_building, Common.ProductionState.off);
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
