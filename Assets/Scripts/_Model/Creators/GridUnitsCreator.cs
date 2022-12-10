using LittleMars.Models.Common;
using System.Collections.Generic;

namespace LittleMars.Models.Creators
{
    public class GridUnitsCreator : IGridUnitsCreator
    {
        List<Inventory.B_TYPE> _initialBuildingTypes;
        IRowsAndColumnsCalculator _rowsAndColumnsCalculator;
        
        public GridUnitsCreator(LevelInfo levelInfo, IRowsAndColumnsCalculator rowsAndColumnsCalculator)
        {
            _initialBuildingTypes = levelInfo.DefaultBuildingTypes;
            _rowsAndColumnsCalculator = rowsAndColumnsCalculator;
        }

        public List<List<GridUnit>> CreateGridUnits()
        {
            int rows, columns;
            _rowsAndColumnsCalculator.CalculateRowsAndColumns(out rows, out columns);

            List<List<GridUnit>> units = new List<List<GridUnit>>();
            for (int i = 0; i < rows; i++)
            {
                List<GridUnit> row = new List<GridUnit>();
                for (int j = 0; j < columns; j++)
                {
                    var slot = new GridUnit(false, 
                        CustomFunctions.MakeFullCopyOfListOfBuildingsType(_initialBuildingTypes));

                    row.Add(slot);
                }
                units.Add(row);
            }
            return units;
        }
    }


}
