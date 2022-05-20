using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicBuilding
{
    // Class for all buildings

    [Header("Basic Info")]
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }

    [SerializeField] private Sprite[] iconParts;
    public Sprite[] IconParts { get { return iconParts; } }

    [SerializeField] private RuntimeAnimatorController[] animator;
    public RuntimeAnimatorController[] Animator { get { return animator; } }

    [Header("Map Needs")]
    [SerializeField] private Inventory.B_TYPE type;
    public Inventory.B_TYPE Type { get { return type; } }

    [SerializeField] private Inventory.R_TYPE resourceField = Inventory.R_TYPE.all;
    public Inventory.R_TYPE ResourceField { get { return resourceField; } }

    [Header("Production Connections")]
    [SerializeField] private Inventory.B_TYPE[] connectionTypes;
    public Inventory.B_TYPE[] ConnectionTypes { get { return connectionTypes; } }

    [Header("Step Way")]
    //[SerializeField] private BuildingWayStep[] way;
    //public BuildingWayStep[] Way { get { return way; } }

    [SerializeField] private BuildingWay[] ways;
    public BuildingWay[] Ways { get { return ways; } }

    [Header("Feature")]
    [SerializeField] private List<ProductionUnit> production;
    public List<ProductionUnit> Production { get { return production; } }

    [SerializeField] private List<ProductionUnit> buildingNeeds;
    public List<ProductionUnit> BuildingNeeds { get { return buildingNeeds; } }

    [SerializeField] private List<int> hoursToActivateTradeNeeds;
    public List<int> HoursToActivateTradeNeeds { get { return hoursToActivateTradeNeeds; } }

    [SerializeField] private List<ProductionUnit> tradeNeeds;
    public List<ProductionUnit> TradeNeeds { get { return tradeNeeds; } }

    [SerializeField] private List<ProductionUnit> hourlyNeeds;
    public List<ProductionUnit> HourlyNeeds { get { return hourlyNeeds; } }

    [Header("Current State")]
    public List<MapSlot> slots;
    public int rotationIndex;
    public int wayIndex;
    public bool hasBeenOnEver = false;

    [SerializeField] private bool[] operatingMode;
    public bool[] OperatingMode { get { return operatingMode; } set { operatingMode = value; } }

    public bool isTurnOffManually;

    [SerializeField] private State.PRIORITY priority;
    public State.PRIORITY Priority { get { return priority; } set { priority = value; } }

    [SerializeField] private bool canBeOn;
    public bool CanBeOn { get { return canBeOn; } set { canBeOn = value; } }

    [SerializeField] private bool isOn;
    public bool IsOn { get { return isOn; } set { isOn = value; } }

    [SerializeField] private bool isNightSwitchAvailable;
    public bool IsNightSwitchAvailable { get { return isNightSwitchAvailable; }}

    [Header("Cosmodrome Options")]
    [SerializeField] private int rocketArriveHour;
    public int RocketArriveHour { get { return rocketArriveHour; } }

    public bool rocketIsOnMars;

    public BasicBuilding(BuildingItem buildingScriptable) 
    {
        //int index = Random.Range(0, buildingScriptable.IconParts.Length);
        itemName = buildingScriptable.ItemName;
        icon = buildingScriptable.Icon;
        iconParts = null;
        //way = buildingScriptable.Way;
        ways = buildingScriptable.Ways;
        production = CustomFunctions.MakeFullCopyOfListOfProductionUnits(buildingScriptable.Production);
        buildingNeeds = CustomFunctions.MakeFullCopyOfListOfProductionUnits(buildingScriptable.BuildingNeeds);
        tradeNeeds = CustomFunctions.MakeFullCopyOfListOfProductionUnits(buildingScriptable.TradeNeeds);
        hourlyNeeds = CustomFunctions.MakeFullCopyOfListOfProductionUnits(buildingScriptable.HourlyNeeds);
        hoursToActivateTradeNeeds = CustomFunctions.MakeFullCopyOfListOfInt(buildingScriptable.HoursToActivateTradeNeeds);
        type = buildingScriptable.Type;
        resourceField = buildingScriptable.ResourceField;
        connectionTypes = buildingScriptable.ConnectionTypes;
        priority = buildingScriptable.Priority;
        isNightSwitchAvailable = buildingScriptable.IsNightSwitchAvailable;
        animator = null;

        slots = new List<MapSlot>();
        rotationIndex = 0;
        wayIndex = 0;
        isOn = false;
        hasBeenOnEver = false;
        isTurnOffManually = false;

        // operating mode
        operatingMode = new bool[2] { true, true };

        if (buildingScriptable is CosmodromeItem)
        {
            CosmodromeItem tempItem = buildingScriptable as CosmodromeItem;
            rocketArriveHour = tempItem.RocketArriveHour;
        }
        else
        {
            rocketArriveHour = -1;
        }

        rocketIsOnMars = false;
    }


    public void SetAnimatorMode(bool forProductionChangeOnly = false)
    {
        if (slots == null && animator == null)
        {
            return;
        }

        int counter = 0;
        if (forProductionChangeOnly)
        {
            foreach(ProductionUnit unit in production)
            {
                if (unit.DayAmount != unit.NightAmount)
                {
                    counter++;
                    break;
                }
            }
            
            if (counter == 0)
            { 
                return;
            }
        }

        foreach(MapSlot slot in slots)
        {
            if (slot ==null)
            {
                continue;
            }

            slot.SetAnimatorMode();
        }
    }


    public void SetIconPartsAndAnimator()
    {
        int index = Random.Range(0, ways[wayIndex].IconParts.Length);
        iconParts = ways[wayIndex].IconParts[index].Icons;
        animator = ways[wayIndex].IconParts[index].Animator;
    }
    //List<ProductionUnit> MakeFullCopyOfList(List<ProductionUnit> originalList)
    //{
    //    List<ProductionUnit> newList = new List<ProductionUnit>();

    //    foreach (ProductionUnit unit in originalList)
    //    {
    //        newList.Add(new ProductionUnit(unit.ResourseType, unit.DayAmount, unit.NightAmount, unit.Multiplier));
    //    }

    //    return newList;
    //}
}
