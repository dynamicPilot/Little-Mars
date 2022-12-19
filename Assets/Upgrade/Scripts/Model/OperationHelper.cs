using LittleMars.Common.Interfaces;
using LittleMars.Map;
using System;
using System.Linq;

namespace LittleMars.Model
{
    /// <summary>
    /// Helper class for OperationManager.
    /// </summary>
    public class OperationHelper
    {
        readonly MapManager _mapManager;

        public OperationHelper(MapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public bool HasAllConnections(IBuildingFacade building)
        {
            var indexes = building.MapSlotIndexes();
            var connections = building.Connections();

            var mapConnections = _mapManager.GetConnectionsForSlots(indexes);

            for (int i = 0; i < connections.Length; i++)
            {
                if (!mapConnections.Contains(connections[i])) return false;
            }

            return true;
        }
    }
}
