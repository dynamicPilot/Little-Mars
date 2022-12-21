using LittleMars.Common;
using LittleMars.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.BuildingSlots
{
    /// <summary>
    /// Class to control all Building slot components.
    /// </summary>
    public class BuildingSlotFacade : MonoBehaviour, IBuildingSlotFacade
    {
        public void SetActiveState(ProductionState state)
        {
            Debug.Log("Set active state : " + (state == ProductionState.on));
            gameObject.SetActive(state == ProductionState.on);
        }

        public class Factory : PlaceholderFactory<BuildingType, Size, BuildingSlotFacade>
        {

        }
    }
}
