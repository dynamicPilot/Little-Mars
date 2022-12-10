using LittleMars.Models.Common;
using System.Collections.Generic;

namespace LittleMars.Models.Creators
{
    public class GridUnitsNeighborsCalculator : IGridUnitsNeighborsCalculator
    {
        IRowsAndColumnsCalculator _rowsAndColumnsCalculator;
        

        public GridUnitsNeighborsCalculator(IRowsAndColumnsCalculator rowsAndColumnsCalculator)
        {
            _rowsAndColumnsCalculator = rowsAndColumnsCalculator;
        }

        public void CalculateGridUnitsNeighbors(List<List<GridUnit>> units)
        {
            int rows, columns;
            _rowsAndColumnsCalculator.CalculateRowsAndColumns(out rows, out columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    List<GridUnit> neighbors = new List<GridUnit>();

                    if (i > 0)
                    {
                        if (!units[i - 1][j].IsBlocked) neighbors.Add(units[i - 1][j]);
                    }

                    if (i < rows - 1)
                    {
                        if (!units[i + 1][j].IsBlocked) neighbors.Add(units[i + 1][j]);
                    }

                    if (j > 0)
                    {
                        if (!units[i][j - 1].IsBlocked) neighbors.Add(units[i][j - 1]);
                    }

                    if (j < columns - 1)
                    {
                        if (!units[i][j + 1].IsBlocked) neighbors.Add(units[i][j + 1]);
                    }

                    units[i][j].SetNeighbors(neighbors.ToArray());
                }
            }
        }

    }

}
