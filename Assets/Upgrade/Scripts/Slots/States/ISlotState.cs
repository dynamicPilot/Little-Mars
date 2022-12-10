using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Slots.States
{
    public interface ISlotState : IDisposable
    { 
        //void OnClickPerformed();
        void SetView();
        void OnDrop(BuildingObject buildingObject);
    }

}
