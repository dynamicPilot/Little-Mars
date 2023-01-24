using LittleMars.Common;
using Zenject;

namespace LittleMars.Connections
{
    public class Connection
    {
        public BuildingType Type { get; private set; }
        public ProductionState State { get; private set; }

        public Connection(BuildingType type, ProductionState state = ProductionState.off)
        {
            Type = type;
            State = state;
        }

        public void UpdateState(ProductionState state)
        {
            State = state;
        }
    }
}
