using LittleMars.Models.Common;
using System.Collections.Generic;

namespace LittleMars.Models.Creators
{
    public class CustomMapGridUnitsCreator : IGridUnitsCreator
    {
        CustomMap _customMap;
        LevelInfo _levelInfo;
        IRowsAndColumnsCalculator _rowsAndColumnsCalculator;

        public CustomMapGridUnitsCreator(LevelInfo levelInfo, IRowsAndColumnsCalculator rowsAndColumnsCalculator)
        {
            _customMap = levelInfo.CustomeAutoMap;
            _levelInfo = levelInfo;
            _rowsAndColumnsCalculator = rowsAndColumnsCalculator;
        }

        public List<List<GridUnit>> CreateGridUnits()
        {
            int rows = _customMap.RowCounter;
            int columns = _customMap.ColumnCounter;
            GridUnitsCreator creator = new GridUnitsCreator(_levelInfo,_rowsAndColumnsCalculator);

            List<List<GridUnit>> units = creator.CreateGridUnits();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    units[i][j].IsBlocked = _customMap.MapByLines[i].Line[j].IsBlock;

                    if (_customMap.MapByLines[i].Line[j].IsResources != null)
                    {
                        // resources
                        foreach (Inventory.R_TYPE type in _customMap.MapByLines[i].Line[j].IsResources)
                            units[i][j].AddResourceType(type);
                    }

                    if (_customMap.MapByLines[i].Line[j].IsBuildings != null)
                    {
                        // buildings
                        foreach (Inventory.B_TYPE type in _customMap.MapByLines[i].Line[j].IsBuildings)
                            units[i][j].AddBuildingType(type);
                    }
                }
            }
            return units;
        }
    }


}
