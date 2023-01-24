using LittleMars.Common;
using LittleMars.Map;
using System.Collections.Generic;
using System.Linq;

namespace LittleMars.Connections
{
    public class SlotConnectonsFactory
    {
        readonly MapManager _mapManager;
        readonly SlotConnections.Factory _factory;

        public SlotConnectonsFactory(MapManager mapManager, SlotConnections.Factory factory)
        {
            _mapManager = mapManager;
            _factory = factory;
        }

        public List<List<SlotConnections>> CreateSlotConnections()
        {
            var slots = new List<List<SlotConnections>>();
            var mapSlots = _mapManager.Slots;
            int rows = mapSlots.Count();
            int columns = mapSlots[0].Count();

            for (int i = 0; i < rows; i++)
            {
                List<SlotConnections> row = new();

                for (int j = 0; j < columns; j++)
                {
                    var slot = _factory.Create();
                    slot.SetIndexes(i, j);
                    row.Add(slot);
                }
                slots.Add(row);
            }

            SetNeighbors(mapSlots, slots);
            return slots;
        }


        private void SetNeighbors(List<List<MapSlotExtended>> slots,
            List<List<SlotConnections>> connections)
        {
            int rows = slots.Count();
            int columns = slots[0].Count();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i > 0)
                    {
                        if (!slots[i - 1][j].IsBlocked)
                            connections[i][j].AddNeighbor(Direction.up, connections[i - 1][j]);
                    }

                    if (i < rows - 1)
                    {
                        if (!slots[i + 1][j].IsBlocked)
                            connections[i][j].AddNeighbor(Direction.down, connections[i + 1][j]);
                    }

                    if (j > 0)
                    {
                        if (!slots[i][j - 1].IsBlocked)
                            connections[i][j].AddNeighbor(Direction.left, connections[i][j - 1]);
                    }

                    if (j < columns - 1)
                    {
                        if (!slots[i][j + 1].IsBlocked)
                            connections[i][j].AddNeighbor(Direction.right, connections[i][j + 1]);
                    }
                }
            }
        }
    }
}
