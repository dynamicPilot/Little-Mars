using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconStorage : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private Sprite money;
    [SerializeField] private Sprite metalls;
    [SerializeField] private Sprite food;
    [SerializeField] private Sprite machines;
    [SerializeField] private Sprite energy;
    [SerializeField] private Sprite goods;
    [SerializeField] private Sprite supplyUnits;

    [Header("Buildings")]
    [SerializeField] private Sprite dome;
    [SerializeField] private Sprite powerPlant;
    [SerializeField] private Sprite supplyPlant;
    [SerializeField] private Sprite farm;
    [SerializeField] private Sprite mine;
    [SerializeField] private Sprite factory;

    [Header("Blocked")]
    [SerializeField] private Sprite blocked;

    private Dictionary<Inventory.R_TYPE, Sprite> resources = new Dictionary<Inventory.R_TYPE, Sprite>();
    private Dictionary<Inventory.B_TYPE, Sprite> buildings = new Dictionary<Inventory.B_TYPE, Sprite>();
    private void Awake()
    {
        resources[Inventory.R_TYPE.money] = money;
        resources[Inventory.R_TYPE.metalls] = metalls;
        resources[Inventory.R_TYPE.food] = food;
        resources[Inventory.R_TYPE.machines] = machines;
        resources[Inventory.R_TYPE.energy] = energy;
        resources[Inventory.R_TYPE.goods] = goods;
        resources[Inventory.R_TYPE.supply_units] = supplyUnits;

        buildings[Inventory.B_TYPE.dome] = dome;
        buildings[Inventory.B_TYPE.power_plant] = powerPlant;
        buildings[Inventory.B_TYPE.supply_plant] = supplyPlant;
        buildings[Inventory.B_TYPE.farm] = farm;
        buildings[Inventory.B_TYPE.mine] = mine;
        buildings[Inventory.B_TYPE.factory] = factory;

    }

    public Sprite GetResourceIcon(Inventory.R_TYPE type)
    {
        if (resources.ContainsKey(type))
            return resources[type];
        else
            return null;
    }

    public Sprite GetBuildingIcon(Inventory.B_TYPE type)
    {
        if (buildings.ContainsKey(type))
            return buildings[type];
        else
            return null;
    }

    public Sprite GetBlockedSlotIcon()
    {
        return blocked;
    }
}
