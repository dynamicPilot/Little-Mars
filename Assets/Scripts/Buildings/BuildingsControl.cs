using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsControl : MonoBehaviour
{
    [Header("Building Samples")]
    [SerializeField] private BuildingItem[] buildingSamples;
    public BuildingItem[] BuildingSamples { get { return buildingSamples; } }

    [Header("UI")]
    [SerializeField] private BuildingsMenuUI buildingsMenuUI;

    [Header("Scripts")]
    [SerializeField] private Inventory inventory;

    public delegate void BuildingsChange();
    public event BuildingsChange OnBuildingsChange;

    public void SetBuildingForLevel(BuildingUnit[] levelBuildings)
    {
        //Debug.Log("BuildingsControl: set buildings for new level");
        foreach (BuildingItem item in buildingSamples)
        {
            item.IsAvailable = false;
            item.AvailableAmount = 0;
        }

        foreach(BuildingUnit unit in levelBuildings)
        {
            int index = Array.IndexOf(buildingSamples, unit.Item);

            if (index > -1)
            {
                buildingSamples[index].IsAvailable = true;
                buildingSamples[index].AvailableAmount = unit.Amount;
                buildingSamples[index].Multiplier = unit.Multiplier;
            }
        }

        buildingsMenuUI.CreateBuldingMenuForLevel();

    }

    public void ActivateAnimatorsForCosmodromes(int hour)
    {
        if (inventory.Cosmodromes == null)
        {
            return;
        }
        else if (inventory.Cosmodromes.Count != 0)
        {
            foreach (BasicBuilding cosmodrome in inventory.Cosmodromes)
            {
                if (cosmodrome.RocketArriveHour == hour)
                {
                    //Debug.Log("BuildingControl: start rocket arriving animation");
                    SetSlotsForCosmodrome(cosmodrome, 0f);
                }
            }
        }
    }

    public List<ProductionUnit> IsAnyRocketStartForThisHour(int hour)
    {
        List<ProductionUnit> unitsCanBeTradeByRockets = new List<ProductionUnit>();

        if (inventory.Cosmodromes == null)
        {
            return null;
        }
        else if (inventory.Cosmodromes.Count != 0)
        {
            foreach (BasicBuilding cosmodrome in inventory.Cosmodromes)
            {
                if (cosmodrome.HoursToActivateTradeNeeds.Contains(hour) && cosmodrome.rocketIsOnMars && cosmodrome.IsOn)
                {
                    //Debug.Log("BuildingControl: has rocket for this hour");

                    // add possible trades
                    foreach(ProductionUnit unit in cosmodrome.TradeNeeds)
                    {
                        unitsCanBeTradeByRockets.Add(unit);
                    }

                    // start rocket
                    SetSlotsForCosmodrome(cosmodrome, 1f);
                }
            }

            return unitsCanBeTradeByRockets;
        }
        else
        {
            return null;
        }
    }

    void SetSlotsForCosmodrome(BasicBuilding cosmodrome, float rocketMode)
    {
        if (cosmodrome.slots != null)
        {
            if (cosmodrome.slots.Count > 0)
            {
                foreach (MapSlot slot in cosmodrome.slots)
                {
                    if (slot != null)
                    {
                        slot.CosmodromeControl(rocketMode);
                    }
                }
            }
        }
    }

    public void MakeBuilding(BuildingItem item)
    {
        item.AvailableAmount -= 1;

        if (item.AvailableAmount == 0)
        {
            item.IsAvailable = false;
        }

        if (OnBuildingsChange != null)
        {
            OnBuildingsChange.Invoke();
        }
    }

    public void DestroyBuilding(BasicBuilding building)
    {
        foreach(BuildingItem item in buildingSamples)
        {
            if (item.ItemName == building.ItemName && item.Type == building.Type)
            {
                //Debug.Log("BuildingsControl: add to amount for " + item.name);
                item.AvailableAmount += 1;

                if (item.AvailableAmount > 0)
                {
                    item.IsAvailable = true;
                }

                if (OnBuildingsChange != null)
                {
                    OnBuildingsChange.Invoke();
                }
                return;
            }
        }

    }
}
