using LittleMars.Buildings.Timers;
using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class DomePausedState : BuildingPausedState
    {
        readonly BuildingTimer _timer;

        public DomePausedState(BuildingView view, IModelFacade model,
            BuildingFacade building, BuildingTimer timer) : base(view, building, model)
        {
            _timer = timer;
        }

        public override void SetView()
        {
            base.SetView();
            _timer.StartTimer();
        }

        public new class Factory : PlaceholderFactory<DomePausedState>
        { }
    }
}
