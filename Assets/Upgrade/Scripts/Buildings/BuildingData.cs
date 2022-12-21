using LittleMars.Common;
using System.Collections.Generic;
using System.Linq;

namespace LittleMars.Buildings
{
    public class BuildingData
    {
        readonly BuildingType[] _connections;
        List<Indexes> _mapSlots;

        public IEnumerable<Indexes> MapSlotIndexes { get => _mapSlots; set => _mapSlots = value.ToList<Indexes>(); }
        public BuildingType[] Connections => _connections;

        public BuildingData(BuildingObject buildingObject)
        {
            _connections = buildingObject.Operation.Connections;
            _mapSlots = new List<Indexes>();
        }

        public void OnStart()
        {
            _mapSlots.Clear();
        }
        
    }
}
