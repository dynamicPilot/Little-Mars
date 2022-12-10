using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Models.Creators
{
    public class SlotCoordinatesCreator : ISlotCoordinatesCreator
    {
        IRowsAndColumnsCalculator _rowsAndColumnsCalculator;       
        float _size;

        public SlotCoordinatesCreator(IRowsAndColumnsCalculator rowsAndColumnsCalculator)
        {
            _rowsAndColumnsCalculator = rowsAndColumnsCalculator;
            _size = 0.22f;
        }

        public List<List<Vector2>> CreateSlotCoordinates()
        {
            int rows, columns;
            _rowsAndColumnsCalculator.CalculateRowsAndColumns(out rows, out columns);

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
                    row.Add(new Vector2(x,y));
                    x += _size;
                }
                coordinates.Add(row);
                y -= _size;
            }

            return coordinates;
        }

        private Vector2 GetFirstSlotPosition(int rows, int columns)
        {
            float x, y;
            x =  - ((columns / 2) - 0.5f) * _size;
            y = ((rows / 2) - 0.5f) * _size;

            return new Vector2(x, y);

        }
    }

}
