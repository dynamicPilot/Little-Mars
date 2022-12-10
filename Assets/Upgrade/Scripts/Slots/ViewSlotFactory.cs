using LittleMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LittleMars.Common.Interfaces;

namespace LittleMars.Slots
{
    public class ViewSlotFactory
    {
        MapManager _mapManager;
        Settings _settings;
        ViewSlotFacade.Factory _factory;

        public ViewSlotFactory(Settings settings, MapManager mapManager, 
            ViewSlotFacade.Factory factory)
        {
            _settings = settings;
            _mapManager = mapManager;
            _factory = factory;
        }

        public List<List<ViewSlotFacade>> Create()
        {
            List<List<ViewSlotFacade>> slots = new List<List<ViewSlotFacade>>();
            var mapSlots = _mapManager.Slots;
            int rows = mapSlots.Count();
            int columns = mapSlots[0].Count();

            List<List<Vector2>> coordinates = CalculateCoordinates(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                List<ViewSlotFacade> row = new();

                for (int j = 0; j < columns; j++)
                {
                    var slot = _factory.Create(coordinates[i][j]);
                    slot.Resources(mapSlots[i][j].Resources);
                    slot.Indexes(i, j);
                    row.Add(slot);
                }
                slots.Add(row);
            }

            return slots;
        }

        private List<List<Vector2>> CalculateCoordinates(int rows, int columns)
        {
            Vector2 firstSlotPosition = GetFirstSlotPosition(rows, columns);
            List<List<Vector2>> coordinates = new List<List<Vector2>>();

            var x = firstSlotPosition.x;
            var y = firstSlotPosition.y;

            for (int i = 0; i < rows; i++)
            {
                List<Vector2> row = new List<Vector2>();
                x = firstSlotPosition.x;

                for (int j = 0; j < columns; j++)
                {
                    row.Add(new Vector2(x, y));
                    x += _settings.Size;
                }
                coordinates.Add(row);
                y -= _settings.Size;
            }

            return coordinates;
        }

        private Vector2 GetFirstSlotPosition(int rows, int columns)
        {
            float x, y;
            x = -((columns / 2) - 0.5f) * _settings.Size;
            y = ((rows / 2) - 0.5f) * _settings.Size;

            return new Vector2(x, y);

        }


        [Serializable]
        public class Settings
        {
            public float Size;
        }
    }
}
