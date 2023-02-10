using Zenject;

namespace LittleMars.Buildings.View.States
{
    public class BuildingViewOffState : IViewState
    {
        BuildingObjectViewComponents _view;

        public BuildingViewOffState(BuildingObjectViewComponents view)
        {
            _view = view;
        }

        public void CloseView()
        {

        }

        public void Dispose()
        {
        }

        public void SetView()
        {
            _view.Indicators.UpdateState(Common.BStates.off);
            _view.Animations.UpdateState(Common.BStates.off);
        }

        public class Factory : PlaceholderFactory<BuildingViewOffState>
        { }
    }
}