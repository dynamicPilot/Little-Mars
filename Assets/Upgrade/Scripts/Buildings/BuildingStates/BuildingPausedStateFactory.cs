using LittleMars.Common;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class BuildingPausedStateFactory : IFactory<BuildingPausedState>
    {
        BuildingType _type;
        BuildingPausedState.Factory _factory;
        DomePausedState.Factory _domeFactory;

        public BuildingPausedStateFactory(BuildingType type, BuildingPausedState.Factory factory, 
            DomePausedState.Factory domeFactory)
        {
            _type = type;
            _factory = factory;
            _domeFactory = domeFactory;
        }

        public BuildingPausedState Create()
        {
            if (_type == BuildingType.dome) return _domeFactory.Create();
            else return _factory.Create();
        }
    }


}
