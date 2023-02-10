using LittleMars.Common;
using LittleMars.Common.Signals;
using System.Linq;
using Zenject;

namespace LittleMars.Buildings.View.States
{
    public class BuildingViewOnState : IViewState
    {
        readonly BuildingObjectViewComponents _components;

        public BuildingViewOnState(BuildingObjectViewComponents components)
        {
            _components = components;
        }

        public void Dispose()
        {
        }

        public void SetView()
        {
            _components.Indicators.UpdateState(Common.BStates.on);
            _components.Animations.UpdateState(Common.BStates.on);
        }

        public void CloseView()
        {

        }

        public class Factory : PlaceholderFactory<BuildingViewOnState>
        { }
    }
}