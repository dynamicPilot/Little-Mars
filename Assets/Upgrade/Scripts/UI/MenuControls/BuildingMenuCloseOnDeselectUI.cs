using UnityEngine;

namespace LittleMars.UI.MenuControls
{
    public class BuildingMenuCloseOnDeselectUI : CloseOnDeselectUI
    {
        [SerializeField] BuildingControllerUI _buildingController;

        private void Awake()
        {
            _menuToClose = _buildingController;
        }
    }
}
