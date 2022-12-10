using LittleMars.Buildings.Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Map.Routers
{
    public class MapRouterCheck : IMapRouterCheck
    {
        public bool Check(IEnumerable<MapSlotExtended> slots)
        {
            return true;
        }

        public void Dispose()
        {
            Debug.Log("Dispose MapRouterCheck");
        }

        public class Factory : PlaceholderFactory<MapRouterCheck>
        {

        }
    }



}
