using LittleMars.Models.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LittleMars.Models.Grid
{
    [System.Serializable]
    public class LevelGrids
    {
        public List<List<GridUnit>> GridUnits { get; private set; }
        public List<List<Vector2>> Coordinates { get; private set; }

        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public Vector2 OriginPoint { get; private set; }

        public LevelGrids(List<List<GridUnit>> gridUnits, List<List<Vector2>> coordinates, int columns, int rows, Vector2 originPoint)
        {
            GridUnits = gridUnits;
            Coordinates = coordinates;
            Columns = columns;
            Rows = rows;
            OriginPoint = originPoint;
        }

    }
}

