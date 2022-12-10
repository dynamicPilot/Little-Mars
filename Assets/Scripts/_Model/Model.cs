using LittleMars.Models.Common;
using LittleMars.Models.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LittleMars.Models
{
    public class Model : IModel
    {
        private List<List<GridUnit>> _units;

        public Model(LevelGrids grids)
        {
            _units = grids.GridUnits;
        }
    }
}

