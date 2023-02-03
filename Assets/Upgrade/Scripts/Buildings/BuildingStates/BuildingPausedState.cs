using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class BuildingPausedState : IBuildingState
    {
        readonly BuildingView _view;
        readonly BuildingFacade _building;
        readonly IModelFacade _model;

        public BuildingPausedState(BuildingView view, BuildingFacade building, IModelFacade model)
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

        public void OnRemove()
        {
            _view.SetViewActiveState(false);
        }

        public void OnStart()
        {
            _view.SetViewActiveState(true);
        }

        public virtual void SetView()
        {
            _view.PausedView();
        }

        public class Factory : PlaceholderFactory<BuildingPausedState>
        {
        }
    }


}
