using LittleMars.Models.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LittleMars.View.Game.Map
{
    public class SlotUnit: MonoBehaviour
    {
        [SerializeField] private int _rowIndex;
        [SerializeField] private int _columnIndex;
        private GridUnit _gridUnit;

        public int RowIndex { get; private set; }
        public int ColumnIndex { get; private set; }

        public void SetSlotUnit(int rowIndex, int colIndex, GridUnit gridUnit)
        {
            _rowIndex = rowIndex;
            _columnIndex = colIndex;
            _gridUnit = gridUnit;
        }
    }
}

