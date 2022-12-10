using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LittleMars.Map.Routers
{
    public interface IMapRouterCheck : IDisposable
    {
        bool Check(IEnumerable<MapSlotExtended> slots);
    }
}
