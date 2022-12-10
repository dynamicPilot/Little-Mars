using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace LittleMars.Buildings.View
{
    public class BuildingObjectView : MonoBehaviour
    {     

        public void TransitToOnState()
        {
            Debug.Log("Transit to on state");
        }


        public class Factory : PlaceholderFactory<BuildingObjectView>
        { 
        }

    }
}
