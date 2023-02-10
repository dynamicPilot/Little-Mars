using Zenject;

namespace LittleMars.Buildings.View.States
{
    public class BuildingViewEffectedState : IViewState
    {
        readonly BuildingObjectViewComponents _components;

        public BuildingViewEffectedState(BuildingObjectViewComponents components)
        {
            _components = components;
        }

        public void Dispose()
        {
        }

        public void SetView()
        {
            _components.Indicators.UpdateState(Common.BStates.effected);
            _components.Animations.UpdateState(Common.BStates.effected);
            _components.MakeEffect();

        }

        public void CloseView()
        {
        }

        public class Factory : PlaceholderFactory<BuildingViewEffectedState>
        { }
    }
}