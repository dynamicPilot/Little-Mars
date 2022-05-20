using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapSlotDropEventSystem : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] private MapSlot slot;

    [Header("Sounds")]
    [SerializeField] private bool needDropSound = true;
    [SerializeField] private int dropSoundIndex = 5;
    [SerializeField] private int turnOnSoundIndex = 7;
    [SerializeField] private int turnOffSoundIndex = 8;

    [Header("Scripts")]
    [SerializeField] private MapChecking mapChecking;
    [SerializeField] private BuildingPlacementMenu buildingPlacementMenu;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ProductionControl productionControl;
    [SerializeField] private GameTime gameTime;

    private AudioControl audioControl;
    List<MapSlot> slots;
    List<int> buildingImageIndexes;
    BuildingItem item;
    PointerEventData eventDataBasic;

    private int clickCounter = 0;

    public void SetScriptLinks(MapChecking newMapChecking, BuildingPlacementMenu newBuildingPlacementMenu, Inventory newInventory,
        ProductionControl newProductionControl, IconStorage newIconStorage, GameTime newGameTime)
    {
        mapChecking = newMapChecking;
        buildingPlacementMenu = newBuildingPlacementMenu;
        inventory = newInventory;
        productionControl = newProductionControl;
        gameTime = newGameTime;
        slot.SetScriptLinks(newIconStorage, gameTime);

        audioControl = AudioControl.Instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<BasicImageDragEventSystem>() != null)
            {
                item = eventData.pointerDrag.GetComponent<BasicImageDragEventSystem>().GetItem();
                slots = null;
                buildingImageIndexes = null;
                eventDataBasic = eventData;
                
                // check for adding building according to resources and available amount
                if (item == null)
                {
                    Debug.Log("BasicSlotDropEventSystem: no item");
                    audioControl.PlayCanNotDo();
                    return; 
                }

                if (!inventory.CheckForResourcesBeforeBuilding(item))
                {
                    Debug.Log("BasicSlotDropEventSystem: no resources");
                    audioControl.PlayCanNotDo();
                    return;
                }

                if (needDropSound) audioControl.PlayClickSound(dropSoundIndex);
                TryToAddBuilding();

            }
            else
            {
                return;
            }
        }
    }

    void TryToAddBuilding(int newRotationIndex = 0)
    {
        KeyValuePair<List<MapSlot>, List<int>> checkingResult = mapChecking.CheckForBuildingWays(item.Ways, slot, item.Type, item.ResourceField, newRotationIndex);

        if (checkingResult.Key == null || checkingResult.Value == null)
        {
            if (slot != null && buildingImageIndexes != null)
            {
                ShowBuildingPlacement();
            }
            audioControl.PlayCanNotDo();
            return;
        }

        // set slots
        //Debug.Log("BasicSlotDropEventSystem: show pre-building state...");
        slots = checkingResult.Key;
        buildingImageIndexes = checkingResult.Value;
        ShowBuildingPlacement();
    }

    void ShowBuildingPlacement()
    {
        foreach (MapSlot mapSlot in slots)
        {
            mapSlot.SlotEffects.ShowBuildingPlacement();
        }

        buildingPlacementMenu.SetMenu(eventDataBasic.pointerCurrentRaycast.screenPosition, this);
    }

    void HideBuildingPlacement()
    {
        foreach (MapSlot mapSlot in slots)
        {
            mapSlot.SlotEffects.ReturnToDefault();
        }
    }

    public void AcceptBuilding()
    {
        HideBuildingPlacement();
        //Debug.Log("BasicSlotDropEventSystem: build building");

        // create class instance
        BasicBuilding newBuilding = new BasicBuilding(item);
        newBuilding.rotationIndex = buildingImageIndexes[buildingImageIndexes.Count - 2];
        newBuilding.wayIndex = buildingImageIndexes[buildingImageIndexes.Count - 1];
        newBuilding.slots = new List<MapSlot> (slots);

        // set Icon Parts And Animator
        newBuilding.SetIconPartsAndAnimator();

        // set slots and images
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].AddBuilding(newBuilding, buildingImageIndexes[i]);
        }
        // add to inventory
        inventory.AddBuilding(newBuilding, item);

    }

    public void RotateBuilding()
    {
        HideBuildingPlacement();
        int newRotationIndex = (buildingImageIndexes[buildingImageIndexes.Count - 2] + 1) % 4;
        TryToAddBuilding(newRotationIndex);
    }

    public void RemoveBuilding()
    {
        HideBuildingPlacement();
        return;
    }

    public void DestroyBuilding()
    {
        //Debug.Log("BasicSlotDropEventSystem: destroy building");

        // set slots and images
        BasicBuilding buildingToDestroy = slot.GetBuilding();

        //Debug.Log("BasicSlotDropEventSystem: slots count " + buildingToDestroy.slots.Count);
        //Debug.Log("BasicSlotDropEventSystem: current slot " + slot.listIndex);
        for (int i = 0; i < buildingToDestroy.slots.Count; i++)
        {
            //if (buildingToDestroy.slots[i] == slot)
            //{
            //    continue;
            //}

            try
            {
                buildingToDestroy.slots[i].MakeEmpty();
            }
            catch (NullReferenceException)
            {
                //Debug.Log("MapSlotEventSystem: slot count " + buildingToDestroy.slots.Count + " current slot " + buildingToDestroy.slots[i].listIndex);
            }
        }

        inventory.RemoveBuilding(buildingToDestroy);
    }

    public void TurnOnOffBuilding()
    {
        //Debug.Log("BasicSlotDropEventSystem: switch building production mode");
        BasicBuilding building = slot.GetBuilding();

        if (building.IsOn)
        {
            //Debug.Log("BasicSlotDropEventSystem: TurnBuildingOffManually");
            productionControl.TurnBuildingOffManually(building);
            audioControl.PlayClickSound(turnOffSoundIndex);
        }
        else if (!building.IsOn && !building.isTurnOffManually)
        {
            //Debug.Log("BasicSlotDropEventSystem: TurnBuildingOffManually becouse not turn off manually");

            if (building.OperatingMode[(int)gameTime.Period])
            {
                productionControl.TurnBuildingOnManually(building);
            }
            else
            {
                productionControl.TurnBuildingOffManually(building);
            }           
            audioControl.PlayClickSound(turnOffSoundIndex);
        }
        else
        {
            //Debug.Log("BasicSlotDropEventSystem: TurnBuildingOnManually");
            productionControl.TurnBuildingOnManually(building);
            audioControl.PlayClickSound(turnOnSoundIndex);
        }
    }

    public void DayNightOperatingModeSwitcher(GameTime.PERIOD period, bool newMode)
    {
        BasicBuilding building = slot.GetBuilding();
        if (!building.IsNightSwitchAvailable)
        {
            return;
        }

        if (period == GameTime.PERIOD.day)
        {
            building.OperatingMode[0] = newMode;
        }
        else
        {
            building.OperatingMode[1] = newMode;
        }

        //buildingPlacementMenu.UpdateDayNightModeToggles(building.IsNightSwitchAvailable, building.OperatingMode);
        CheckForTuenOnOffAfterOperatingModeChange();
    }

    void CheckForTuenOnOffAfterOperatingModeChange()
    {
        BasicBuilding building = slot.GetBuilding();
        GameTime.PERIOD period = gameTime.Period;
        if (period == GameTime.PERIOD.day)
        {
            if (building.OperatingMode[0] && !building.IsOn)
            {
                productionControl.TurnBuildingOn(building, true);
            }                
            else if (!building.OperatingMode[0] && building.IsOn)
            {
                productionControl.TurnBuildingOff(building, true);
            }
        }
        else if (period == GameTime.PERIOD.night)
        {
            if (building.OperatingMode[1] && !building.IsOn)
            {
                productionControl.TurnBuildingOn(building, true);
            }
            else if (!building.OperatingMode[1] && building.IsOn)
            {
                productionControl.TurnBuildingOff(building, true);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("CLICK");
        audioControl.PlayClickSound(0);
        if (slot.GetBuilding() != null && slot.GetBuilding().ItemName != "" && clickCounter != 1)
        {
            //Debug.Log("MapSlotDropEventSystem: first click");
            buildingPlacementMenu.SetMenu(eventData.pointerCurrentRaycast.screenPosition, this, true, slot.GetBuilding().IsOn, slot.GetBuilding().IsNightSwitchAvailable, slot.GetBuilding().OperatingMode);
            clickCounter = 1;
        }
        else if (clickCounter == 1 && slot.GetBuilding() != null && slot.GetBuilding().ItemName != "")
        {
            //Debug.Log("MapSlotDropEventSystem: double click");
            //clickCounter = 0;
            buildingPlacementMenu.CloseChangeOrDestroyMenu();
        }
    }

    public void ResetClickCounter()
    {
        try
        {
            //Debug.Log("MapSlotDropEventSystem: reset counter for " + slot.GetBuilding().ItemName);
        }
        catch
        {

        }
        clickCounter = 0;
    }

    public BasicBuilding GetBuilding()
    {
        return slot.GetBuilding();
    }

    public void SetInitialImage(Sprite initialImage = null)
    {
        if (initialImage == null)
        {
            //image.gameObject.SetActive(false);
        }
        else
        {
            //image.gameObject.SetActive(true);
            //image.sprite = initialImage;
        }
    }
}
