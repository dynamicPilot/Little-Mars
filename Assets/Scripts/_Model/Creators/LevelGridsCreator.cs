using LittleMars.Models.Common;
using LittleMars.Models.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Models.Creators
{
    public interface IRowsAndColumnsCalculator
    {
        void CalculateRowsAndColumns(out int rows, out int columns);
    }
    public interface IGridUnitsCreator
    {
        List<List<GridUnit>> CreateGridUnits();
    }

    public interface IGridUnitsNeighborsCalculator
    {
        void CalculateGridUnitsNeighbors(List<List<GridUnit>> units);
    }

    public interface IGridUnitsFieldsCreator
    {
        void CreateGridUnitsFields(List<List<GridUnit>> units);
    }

    public interface ISlotCoordinatesCreator
    {
        List<List<Vector2>> CreateSlotCoordinates();
    }

    public class LevelGridsCreator : IFactory<LevelGrids>
    {
        IRowsAndColumnsCalculator _rowsAndColumnsCalculator;
        IGridUnitsCreator _unitsCreator;
        IGridUnitsNeighborsCalculator _neighborsCalculator;
        IGridUnitsFieldsCreator _fieldsCreator;
        ISlotCoordinatesCreator _coordinatesCreator;

        public LevelGridsCreator(IRowsAndColumnsCalculator rowsAndColumnsCalculator, 
            IGridUnitsCreator unitsCreator, IGridUnitsNeighborsCalculator neighborsCalculator,
            IGridUnitsFieldsCreator fieldsCreator, ISlotCoordinatesCreator coordinatesCreator)
        {
            _rowsAndColumnsCalculator = rowsAndColumnsCalculator;
            _unitsCreator = unitsCreator;
            _neighborsCalculator = neighborsCalculator;
            _fieldsCreator = fieldsCreator;
            _coordinatesCreator = coordinatesCreator;
        }

        public LevelGrids Create()
        {
            int rows, columns;
            _rowsAndColumnsCalculator.CalculateRowsAndColumns(out rows, out columns);
            List<List<GridUnit>> units = _unitsCreator.CreateGridUnits();
            _neighborsCalculator.CalculateGridUnitsNeighbors(units);
            _fieldsCreator.CreateGridUnitsFields(units);
            List<List<Vector2>> coordinates = _coordinatesCreator.CreateSlotCoordinates();

            return new LevelGrids(units, coordinates, columns, rows, Vector2.zero);
        }     
    }

}
