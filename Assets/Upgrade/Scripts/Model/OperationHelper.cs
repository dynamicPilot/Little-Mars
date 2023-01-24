using LittleMars.Common.Interfaces;
using LittleMars.Connections;
using System;
using System.Linq;

namespace LittleMars.Model
{
    /// <summary>
    /// Helper class for OperationManager.
    /// </summary>
    public class OperationHelper
    {
        readonly ConnectionsManager _manager;

        public OperationHelper(ConnectionsManager manager)
        {
            _manager = manager;
        }

        public bool HasAllConnections(IBuildingFacade building)
        {
            var indexes = building.MapSlotIndexes();
            var connections = building.Connections();

            var mapConnections = _manager.GetConnectionsForSlots(indexes);

            for (int i = 0; i < connections.Length; i++)
            {
                if (!mapConnections.Contains(connections[i])) return false;
            }

            return true;
        }
    }
}
