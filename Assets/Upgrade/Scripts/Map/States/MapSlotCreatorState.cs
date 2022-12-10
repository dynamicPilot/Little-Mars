using LittleMars.Common;
using LittleMars.Map.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Map.States
{
    public class MapSlotCreatorState
    {
        protected MapManager.Settings _mapSettings;
        FieldManager _fieldManager;

        public MapSlotCreatorState(MapManager.Settings mapSettings, FieldManager fieldManager)
        {
            _mapSettings = mapSettings;
            _fieldManager = fieldManager;
        }

        public virtual List<List<MapSlotExtended>> Create()
        {
            CreateSlots(out List<List<MapSlotExtended>> slots);

            // set neighbors
            SetNeighbors(slots);

            // set fields
            SetFields(slots);

            return slots;
        }

        protected void CreateSlots(out List<List<MapSlotExtended>> slots)
        {
            slots = new List<List<MapSlotExtended>>();

            for (int i = 0; i < _mapSettings.Rows; i++)
            {
                List<MapSlotExtended> row = new List<MapSlotExtended>();
                for (int j = 0; j < _mapSettings.Columns; j++)
                {
                    var slot = new MapSlotExtended(false,
                        MapFactoryStaticHelper.GetBuildingTypesForSlot(_mapSettings.BuildingTypes));
                    slot.SetIndexes(i, j);
                    row.Add(slot);
                }
                slots.Add(row);
            }
        }

        protected void SetFields(List<List<MapSlotExtended>> slots)
        {
            var fields = _fieldManager.Fields(slots);
            //Debug.Log($"Get resources field");

            if (fields == null) return;

            for (int i = 0; i < fields.Count; i++)
            {
                foreach (MapSlotExtended slot in fields[i].Values)
                {
                    //Debug.Log($"Resources for slot: {fields[i].Resource}");
                    slot.AddResource(fields[i].Resource);
                }
            }
        }

        protected void SetNeighbors(List<List<MapSlotExtended>> slots)
        {
            for (int i = 0; i < _mapSettings.Rows; i++)
            {
                for (int j = 0; j < _mapSettings.Columns; j++)
                {
                    if (i > 0)
                    {
                        if (!slots[i - 1][j].IsBlocked)
                            slots[i][j].AddNeighbor(Direction.up, slots[i - 1][j]);
                    }

                    if (i < _mapSettings.Rows - 1)
                    {
                        if (!slots[i + 1][j].IsBlocked)
                            slots[i][j].AddNeighbor(Direction.down, slots[i + 1][j]);
                    }

                    if (j > 0)
                    {
                        if (!slots[i][j - 1].IsBlocked)
                            slots[i][j].AddNeighbor(Direction.left, slots[i][j - 1]);
                    }

                    if (j < _mapSettings.Columns - 1)
                    {
                        if (!slots[i][j + 1].IsBlocked)
                            slots[i][j].AddNeighbor(Direction.right, slots[i][j + 1]);
                    }
                }
            }
        }
    }
}