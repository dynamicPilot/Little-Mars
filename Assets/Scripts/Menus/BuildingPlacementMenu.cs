using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacementMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject panelForChangeOrDestroy;
    [SerializeField] private Canvas mainCanvas;

    [Header("UI Small Elements")]
    [SerializeField] private Toggle dayModeToggle;
    [SerializeField] private Toggle nightModeToggle;

    [Header("UI Element Scripts")]
    [SerializeField] private ButtonEffects turnOnOffButtonEffects;

    private float offset;
    private MapSlotDropEventSystem mapSlotDropEventSystem;
    private bool isPrepairState = false;
    private void Awake()
    {
        isPrepairState = false;
        panel.SetActive(false);
    }

    public void UpdateChangeOrDestroyMenu(BasicBuilding building)
    {
        if (!panelForChangeOrDestroy.activeSelf)
        {
            return;
        }

        if (mapSlotDropEventSystem != null)
        {
            if (mapSlotDropEventSystem.GetBuilding() == building)
            {
                //Debug.Log("BuildingPlacementMenu: panelForChangeOrDestroy... Update for " + building.ItemName);
                isPrepairState = true;
                if (building.IsOn)
                {
                    turnOnOffButtonEffects.SetColorByIndex(1);
                }
                else
                {
                    turnOnOffButtonEffects.SetColorByIndex(0);
                }
                UpdateDayNightModeToggles(building.Type != Inventory.B_TYPE.dome, building.OperatingMode);
               // Debug.Log("BuildingPlacementMenu: panelForChangeOrDestroy..." + panelForChangeOrDestroy.activeSelf);
                isPrepairState = false;
            }
        }
    }

    public void SetMenu(Vector2 position, MapSlotDropEventSystem newDropSystem, bool isChangeOrDistroy = false, bool buildingIsOn = false, bool isModeSwitchingAvailable = true, bool[] modes = null)
    {
        //Debug.Log("BuildingPlacementMenu: start set menu...");
        if (panel.activeSelf && isChangeOrDistroy)
        {
            newDropSystem.ResetClickCounter();
            return;
        }
        else if (panelForChangeOrDestroy.activeSelf && !isChangeOrDistroy)
        {
            // close change panel
            panelForChangeOrDestroy.SetActive(false);
            // reset
        }

        if (mapSlotDropEventSystem != null && mapSlotDropEventSystem != newDropSystem)
        {
            mapSlotDropEventSystem.ResetClickCounter();
        }

        mapSlotDropEventSystem = newDropSystem;

        if (isChangeOrDistroy)
        {
            //Debug.Log("BuildingPlacementMenu: panelForChangeOrDestroy..." + panelForChangeOrDestroy.activeSelf);
            panelForChangeOrDestroy.SetActive(true);
            isPrepairState = true;
            if (buildingIsOn)
            {
                turnOnOffButtonEffects.SetColorByIndex(1);
            }
            else
            {
                turnOnOffButtonEffects.SetColorByIndex(0);
            }
            UpdateDayNightModeToggles(isModeSwitchingAvailable, modes);
            //Debug.Log("BuildingPlacementMenu: panelForChangeOrDestroy..." + panelForChangeOrDestroy.activeSelf);
            isPrepairState = false;
        }
        else
        {
            //Debug.Log("BuildingPlacementMenu: panel...");
            panel.SetActive(true);
        }
             
        //panel.GetComponent<RectTransform>().anchoredPosition = CalculateElementPosition(panel, position);
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        CloseChangeOrDestroyMenu();
    }

    public void CloseChangeOrDestroyMenu()
    {
        panelForChangeOrDestroy.SetActive(false);
        if (mapSlotDropEventSystem != null)
        {
            mapSlotDropEventSystem.ResetClickCounter();
        }
    }

    public void AcceptAction()
    {
        CloseMenu();
        mapSlotDropEventSystem.AcceptBuilding();
    }

    public void RotateAction()
    {
        mapSlotDropEventSystem.RotateBuilding();
    }

    public void RemoveAction()
    {
        CloseMenu();
        mapSlotDropEventSystem.RemoveBuilding();
    }

    public void DestroyAction()
    {
        CloseMenu();
        mapSlotDropEventSystem.DestroyBuilding();
    }

    public void TurnOnOffAction()
    {
        CloseMenu();
        mapSlotDropEventSystem.TurnOnOffBuilding();
    }

    public void MakeButtonEffect(string buttonCode)
    {
        if (buttonCode == "turnOnOff")
        {
            turnOnOffButtonEffects.SwitchColor();
        }
    }

    public void DayOperatingModeSwitch(bool isToggleOn)
    {
        if (!isPrepairState) CloseMenu();
        mapSlotDropEventSystem.DayNightOperatingModeSwitcher(GameTime.PERIOD.day, isToggleOn);
    }

    public void NightOperatingModeSwitch(bool isToggleOn)
    {
        if (!isPrepairState) CloseMenu();
        mapSlotDropEventSystem.DayNightOperatingModeSwitcher(GameTime.PERIOD.night, isToggleOn);
    }

    public void UpdateDayNightModeToggles(bool isAvailable, bool[] modes)
    {
        //Debug.Log("BuildingPlacementMenu: panelForChangeOrDestroy... start toggle" + panelForChangeOrDestroy.activeSelf);
        dayModeToggle.gameObject.SetActive(isAvailable);
        nightModeToggle.gameObject.SetActive(isAvailable);

        dayModeToggle.isOn = modes[0];
        nightModeToggle.isOn = modes[1];
        //Debug.Log("BuildingPlacementMenu: panelForChangeOrDestroy..." + panelForChangeOrDestroy.activeSelf);
    }

    Vector2 CalculateElementPosition(GameObject infoWindow, Vector2 mousePosition)
    {

        //Debug.Log("Calculate window position");
        float screenW = mainCanvas.GetComponent<CanvasScaler>().referenceResolution.x;
        float screenH = mainCanvas.GetComponent<CanvasScaler>().referenceResolution.y;
        float scaleX = mainCanvas.GetComponent<RectTransform>().localScale.x;
        float scaleY = mainCanvas.GetComponent<RectTransform>().localScale.y;
        //Debug.Log("Screen params " +  screenW + " " + screenH);
        offset = screenW * 0.01f;
        float windiwW = infoWindow.GetComponent<RectTransform>().rect.width;
        float windiwH = infoWindow.GetComponent<RectTransform>().rect.height;
        //Debug.Log("Window params " + windiwW + " " + windiwH + " offset for screen is " + offset);
        //Debug.Log("Mouse position " + mousePosition.x + " " + mousePosition.y);
        float x = mousePosition.x / scaleX + windiwW / 2 + offset;
        float y = mousePosition.y / scaleY - windiwH / 2 - offset;
        //Debug.Log("Initial position " + x + " " + y);

        if (mousePosition.y / scaleY - windiwH - offset * 2 < windiwH / 2)
        {
            // Need pull it up
            //Debug.Log("Need pull it up");
            y = offset * 2 + windiwH / 2;
        }

        if (mousePosition.x / scaleX + windiwW + offset * 2 > screenW - windiwW / 2)
        {
            // Need pull it left
            //Debug.Log("Need pull it left");
            x = screenW - offset * 2 - windiwW;
        }

        return new Vector2(mousePosition.x / scaleX, mousePosition.y / scaleY);
    }
}
