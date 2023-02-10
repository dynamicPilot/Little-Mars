using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace LittleMars.Buildings.BuildingStates
{
    public interface IBuildingState : IDisposable
    {
        void OnClickPerformed();
        void SetView();
        void OnStart();
        void OnRemove();
    }
}