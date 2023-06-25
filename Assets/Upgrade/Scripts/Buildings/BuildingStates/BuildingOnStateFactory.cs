using LittleMars.Common;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class BuildingOnStateFactory : IFactory<BuildingOnState>
    {
        BuildingType _type;
        BuildingOnState.Factory _factory;
        DomeOnState.Factory _domeFactory;

        public BuildingOnStateFactory(BuildingType type, BuildingOnState.Factory factory, 
            DomeOnState.Factory domeFactory)
        {
            _type = type;
            _factory = factory;
            _domeFactory = domeFactory;
        }

        public BuildingOnState Create()
        {
            if (_type == BuildingType.dome) return _domeFactory.Create();
            else return _factory.Create();
        }
    }


}
