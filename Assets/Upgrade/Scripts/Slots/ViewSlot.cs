using LittleMars.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleMars.Slots.UI;

namespace LittleMars.Slots
{
    /// <summary>
    /// View slot data class.
    /// </summary>
    public class ViewSlot
    {
        Indexes _indexes;
        public Indexes Indexes { get => _indexes; }
        public void SetIndexes(int row, int columns)
        {
            _indexes = new Indexes(row, columns);
        }
    }
}
