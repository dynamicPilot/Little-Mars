using LittleMars.Buildings.Timers;
using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class DomeOnState : BuildingOnState
    {
        readonly BuildingTimer _timer;

        public DomeOnState(BuildingView view, IModelFacade model, BuildingFacade building, 
            BuildingState state, BuildingTimer timer) : base(view, model, building, state)
        {
            _timer = timer;
        }

        public override void SetView()
        {
            base.SetView();
            _timer.StopTimer();
        }

        public new class Factory : PlaceholderFactory<DomeOnState>
        { }
    }
}
