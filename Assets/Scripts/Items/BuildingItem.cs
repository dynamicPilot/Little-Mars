using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Item", menuName = "Items/New Building Item")]
public class BuildingItem : BasicItem
{
    [Header ("Building")]
    [SerializeField] private Inventory.B_TYPE type;
    public Inventory.B_TYPE Type { get { return type; } }

    [SerializeField] private Inventory.R_TYPE resourceField = Inventory.R_TYPE.none;
    public Inventory.R_TYPE ResourceField { get { return resourceField; } }

    [SerializeField] private ItemIcons[] iconParts;
    public ItemIcons[] IconParts { get { return iconParts; } }

    [Header("Production Connections")]
    [SerializeField] private Inventory.B_TYPE[] connectionTypes;
    public Inventory.B_TYPE[] ConnectionTypes { get { return connectionTypes; } }

    [Header("Step Way")]
    //[SerializeField] private BuildingWayStep[] way;
    //public BuildingWayStep[] Way { get { return way; } }

    [SerializeField] private BuildingWay[] ways;
    public BuildingWay[] Ways { get { return ways; } }


    [Header("Feature")]
    [SerializeField] private bool isNightSwitchAvailable;
    public bool IsNightSwitchAvailable { get { return isNightSwitchAvailable; } }

    [SerializeField] private List<ProductionUnit> production;
    public List<ProductionUnit> Production { get { return production; } }

    [SerializeField] private List<ProductionUnit> hourlyNeeds;
    public List<ProductionUnit> HourlyNeeds { get { return hourlyNeeds; } }
    
    [SerializeField] private List<int> hoursToActivateTradeNeeds;
    public List<int> HoursToActivateTradeNeeds { get { return hoursToActivateTradeNeeds; } }

    [SerializeField] private List<ProductionUnit> tradeNeeds;
    public List<ProductionUnit> TradeNeeds { get { return tradeNeeds; } }

    [Header("For Building Menu")]
    [SerializeField] private Sprite iconWhenDrag;
    public Sprite IconWhenDrag { get { return iconWhenDrag; } }
}
