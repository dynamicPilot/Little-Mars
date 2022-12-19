using LittleMars.Buildings.Parts;
using LittleMars.Buildings.States;
using LittleMars.Buildings.View;
using LittleMars.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Buildings
{
    public class Building
    {
        readonly BuildingType[] _connections;
        IEnumerable<Indexes> _mapSlots;

        public IEnumerable<Indexes> MapSlotIndexes { get => _mapSlots; set => _mapSlots = value; }
        public BuildingType[] Connections => _connections;

        public Building(BuildingObject buildingObject)
        {
            _connections = buildingObject.Operation.Connections;
            _mapSlots = new List<Indexes>();
        }

        
    }
}
