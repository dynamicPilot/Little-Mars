using LittleMars.Common;
using LittleMars.Map.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using static LittleMars.Settings.LevelSettings;

namespace LittleMars.Map.States
{
    public class CustomMapSlotCreatorState : MapSlotCreatorState, IMapSlotCreatorState
    {
        public CustomMapSlotCreatorState(MapManager.Settings mapSettings, 
            FieldManager fieldManager) : base(mapSettings, fieldManager)
        {
        }

        public override List<List<MapSlotExtended>> Create()
        {
            List<List<MapSlotExtended>> slots;

            CreateSlots(out slots);
            ApplyCustomMap(slots);
            SetNeighbors(slots);
            SetFields(slots);

            return slots;
        }

        public void Dispose()
        {
        }

        private void ApplyCustomMap(List<List<MapSlotExtended>> slots)
        {
            int rows = _mapSettings.CustomMap.Rows;
            int columns = _mapSettings.CustomMap.Columns;

            for (int i = 0; i < rows; i++)
            {
                MapLine line = _mapSettings.CustomMap.Lines[i];
                for (int j = 0; j < columns; j++)
                {
                    if (line.Slots[j].IsBlocked) slots[i][j].AddIsBlocked();

                    var types = MapFactoryStaticHelper.GetBuildingTypesForSlot(line.Slots[j].Buildings.ToArray());
                    if (types == null || types.Count > 0) types = _mapSettings.BuildingTypes.ToList();

                    foreach (BuildingType building in types) slots[i][j].AddBuilding(building);

                    var resources = MapFactoryStaticHelper.GetResourcesForSlot(line.Slots[j].Resources.ToArray());
                    if (resources != null && resources.Count > 0)
                    {
                        foreach (Resource resource in resources) slots[i][j].AddResource(resource);
                    }
                }
            }
        }

        public class Factory : PlaceholderFactory<CustomMapSlotCreatorState>
        {

        }
    }
}
