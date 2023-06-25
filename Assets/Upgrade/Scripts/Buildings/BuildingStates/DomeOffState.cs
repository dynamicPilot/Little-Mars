using LittleMars.Buildings.Timers;
using LittleMars.Buildings.View;
using LittleMars.Common.Interfaces;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class DomeOffState : BuildingOffState
    {
        readonly BuildingTimer _timer;

        public DomeOffState(BuildingView view, IModelFacade model, 
            BuildingFacade building, BuildingTimer timer) : base(view, model, building)
        {
            _timer = timer;
        }

        public override void SetView()
        {
            base.SetView();
            _timer.StartTimer();
        }

        public override void OnRemove()
        {
            _timer.ResetTimer();
            base.OnRemove();
        }

        public new class Factory : PlaceholderFactory<DomeOffState>
        { }
    }
}
