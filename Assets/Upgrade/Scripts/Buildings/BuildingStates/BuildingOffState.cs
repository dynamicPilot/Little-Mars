using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using UnityEngine.EventSystems;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
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
        }

        public void OnClickPerformed()
        {
            _model.CallBuildingController(_building);
        }

        public void SetView()
        {
            _view.OffView();
        }

        public void OnStart()
        {
            _view.SetViewActiveState(true);
        }


        public void OnRemove()
        {
            _view.SetViewActiveState(false);
        }


        public class Factory : PlaceholderFactory<BuildingOffState>
        {
        }
    }


}
