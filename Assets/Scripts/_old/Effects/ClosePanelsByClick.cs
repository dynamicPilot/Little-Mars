using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePanelsByClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject[] panelsToClose;
    [SerializeField] private BuildingPlacementMenu buildingPlacementMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panelsToClose == null)
        {
            return;
        }

        if (panelsToClose.Length == 0 && buildingPlacementMenu == null)
        {
            return;
        }

        foreach(GameObject panel in panelsToClose)
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
            }
                
        }

        if (buildingPlacementMenu != null) buildingPlacementMenu.CloseChangeOrDestroyMenu();

    }
}
