using LittleMars.Common.Interfaces;
using LittleMars.UI.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI
{
    //public interface 

    public class BuildingControllerUI : MonoBehaviour
    {
        [SerializeField] private ButtonWithStateView _stateButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private ButtonWithStateView _dayStateButton;
        [SerializeField] private ButtonWithStateView _nightStateButton;

        //IBuilding _building = null;
        private void OnDestroy()
        {

        }
        private void SetListeners()
        {
            //_stateButton.onClick.AddListener(ChangeState);
        }

        public void Open(IBuildingFacade building)
        {

        }

        public void Close()
        {

        }

        private void OnBuildingStateChange()
        {

        }
    }
}
