using LittleMars.Common;
using Zenject;

namespace LittleMars.Connections
{
    public class Connection
    {
        public BuildingType Type { get; private set; }
        public States State { get; private set; }

        public Connection(BuildingType type, States state = States.off)
        {
            Type = type;
            State = state;
        }

        public void UpdateState(States state)
        {
            State = state;
        }
    }
}
