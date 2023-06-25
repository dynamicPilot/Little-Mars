using LittleMars.Common;
using Zenject;

namespace LittleMars.Buildings.BuildingStates
{
    public class BuildingOffStateFactory : IFactory<BuildingOffState>
    {
        BuildingType _type;
        BuildingOffState.Factory _factory;
        DomeOffState.Factory _domeFactory;

        public BuildingOffStateFactory(BuildingType type, BuildingOffState.Factory factory, 
            DomeOffState.Factory domeFactory)
        {
            _type = type;
            _factory = factory;
            _domeFactory = domeFactory;
        }

        public BuildingOffState Create()
        {
            if (_type == BuildingType.dome) return _domeFactory.Create();
            else return _factory.Create();
        }
    }


}
