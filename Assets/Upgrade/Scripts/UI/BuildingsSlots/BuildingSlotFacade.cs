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
        [SerializeField] private BuildingSlotView _view;

        public void SetActiveState(States state)
        {
            Debug.Log("Set active state : " + (state == States.on));
            gameObject.SetActive(state == States.on);
        }

        public void UpdateSlotAmount(int amount)
        {
            _view.UpdateAmount(amount);
        }
        public class Factory : PlaceholderFactory<BuildingType, Size, BuildingSlotFacade>
        {

        }
    }
}
