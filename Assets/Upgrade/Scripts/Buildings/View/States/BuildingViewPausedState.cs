using Zenject;

namespace LittleMars.Buildings.View.States
{
    public class BuildingViewPausedState : IViewState
    {
        BuildingObjectViewComponents _view;

        public BuildingViewPausedState(BuildingObjectViewComponents view)
        {
            _view = view;
        }

        public void Dispose()
        {
        }

        public void SetView()
        {
            _view.Indicators.UpdateState(Common.BStates.paused);
            _view.Animations.UpdateState(Common.BStates.paused);
        }

        public void CloseView()
        {
        }

        public class Factory : PlaceholderFactory<BuildingViewPausedState>
        { }
    }
}