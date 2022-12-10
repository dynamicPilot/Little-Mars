using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Slots
{
    public interface IDragTarget
    { 
    
    }

    internal interface IDropTarget
    {
        void OnDrop(BuildingObject buildingObject);
    }
}
