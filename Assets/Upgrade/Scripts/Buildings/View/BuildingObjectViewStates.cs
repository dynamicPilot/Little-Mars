using LittleMars.Buildings.View.States;
using LittleMars.Common;

namespace LittleMars.Buildings.View
{
    public class BuildingObjectViewStates
    {
        BuildingViewStatesManager _stateManager;

        BStates _state;
        IViewState _viewState;

        public BuildingObjectViewStates(BuildingViewStatesManager stateManager)
        {
            _stateManager = stateManager;
            _viewState = null;
        }

        public void TransitToState(BStates state)
        {
            if (_state == state) return;
            _state = state;

            ChangeState();
        }

        private void ChangeState()
        {
            _viewState?.CloseView();
            _viewState = _stateManager.CreateState(_state);
            _viewState.SetView();
        }

        public void EffectIsOver()
        {
            if (_state == BStates.effected)
                TransitToState(BStates.on);
        }
    }
}
