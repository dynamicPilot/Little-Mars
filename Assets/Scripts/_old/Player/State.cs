using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public enum MODE { resources, production, hourly_needs}
    public enum PRIORITY { very_high, high, medium, low }

    [Header("Options")]
    [SerializeField] private int betweenHourlyNeedsCalculationTime;
    public int BetweenHourlyNeedsCalculationTime { get { return betweenHourlyNeedsCalculationTime; } }

    [Header("Scripts")]
    [SerializeField] private ProductionControl productionControl;
    [SerializeField] private BuildingsControl buildingsControl;

    private Inventory inventory;
    // current operating state
    private Dictionary<Inventory.R_TYPE, ProductionUnit> resources;
    public Dictionary<Inventory.R_TYPE, ProductionUnit> Resources { get { return resources; } }

    private Dictionary<Inventory.R_TYPE, ProductionUnit> production;
    public Dictionary<Inventory.R_TYPE, ProductionUnit> Production { get { return production; } }

    private Dictionary<Inventory.R_TYPE, ProductionUnit> hourlyNeeds;
    public Dictionary<Inventory.R_TYPE, ProductionUnit> HourlyNeeds { get { return hourlyNeeds; } }

    private Dictionary<Inventory.R_TYPE, ProductionUnit> productionInSum;
    public Dictionary<Inventory.R_TYPE, ProductionUnit> ProductionInSum { get { return productionInSum; } }

    // daily needs
    //private Dictionary<Inventory.R_TYPE, ProductionUnit> dailyNeeds;
    private Dictionary<int, Dictionary<Inventory.R_TYPE, ProductionUnit>> tradeNeedsByHours;
    private Dictionary<Inventory.R_TYPE, ProductionUnit> tradePrices;

    public delegate void ResourcesChange();
    public event ResourcesChange OnResourcesChange;

    public delegate void ProductionChange();
    public event ProductionChange OnProductionChange;

    public delegate void HourlyNeedsChange();
    public event HourlyNeedsChange OnHourlyNeedsChange;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public void SetStartResourcesForLevel(List<ProductionUnit> startResources, List<ProductionUnit> newTradePrices)
    {
        resources = new Dictionary<Inventory.R_TYPE, ProductionUnit>();
        foreach (ProductionUnit unit in startResources)
        {
            resources[unit.ResourseType] = new ProductionUnit(unit.ResourseType, unit.DayAmount);
        }

        tradePrices = new Dictionary<Inventory.R_TYPE, ProductionUnit>();
        tradeNeedsByHours = new Dictionary<int, Dictionary<Inventory.R_TYPE, ProductionUnit>>();

        if (tradePrices != null)
        {
            foreach (ProductionUnit unit in newTradePrices)
            {
                tradePrices[unit.ResourseType] = new ProductionUnit(unit.ResourseType, unit.DayAmount);
            }
        }

        SetStartDailyHourlyNeedsAndProduction();

        if (OnResourcesChange != null)
        {
            OnResourcesChange.Invoke();
        }

        if (OnProductionChange != null)
        {
            OnProductionChange.Invoke();
        }

        if (OnHourlyNeedsChange != null)
        {
            OnHourlyNeedsChange.Invoke();
        }
    }

    public void SetStartDailyHourlyNeedsAndProduction()
    {
        hourlyNeeds = new Dictionary<Inventory.R_TYPE, ProductionUnit>();
        production = new Dictionary<Inventory.R_TYPE, ProductionUnit>();
        productionInSum = new Dictionary<Inventory.R_TYPE, ProductionUnit>();

        foreach (Inventory.R_TYPE resource in resources.Keys)
        {
            productionInSum[resource] = new ProductionUnit(resource, 0f);
            hourlyNeeds[resource] = new ProductionUnit(resource, 0f, 0f, 1f);
            production[resource] = new ProductionUnit(resource, 0f, 0f, 1f);
        }
    }

    public float CalculateActualHourlyProductionAndNeedsByResource(GameTime.PERIOD period, Inventory.R_TYPE resource)
    {
        if (period == GameTime.PERIOD.day)
        {
            return resources[resource].DayAmount - (production[resource].DayAmount - hourlyNeeds[resource].DayAmount);
        }
        else if (period == GameTime.PERIOD.night)
        {
            return resources[resource].DayAmount - (production[resource].NightAmount - hourlyNeeds[resource].NightAmount);
        }
        else
        {
            return 0f;
        }
    }

    public void CalculateHourlyProductionAndNeeds(GameTime.PERIOD period, int hour = -1)
    {
        // Debug.Log("State: NEEDS CALCULATION!");
        
        foreach (Inventory.R_TYPE resource in resources.Keys)
        {
            if (period == GameTime.PERIOD.day)
            {
                if (resources[resource].DayAmount + (production[resource].DayAmount - hourlyNeeds[resource].DayAmount) < 0)
                {
                    // switch by priority
                    //Debug.Log("State: resource warning " + resource);
                    productionControl.SwitchBuildingByResourceAndPriority(resource, resources[resource].DayAmount - (production[resource].DayAmount - hourlyNeeds[resource].DayAmount), period, hour);
                    
                    return;
                }
                else
                {
                    //Debug.Log("State: produce " + resource);
                    resources[resource].ChangeDayAmount(production[resource].DayAmount - hourlyNeeds[resource].DayAmount);
                    productionInSum[resource].ChangeDayAmount(production[resource].DayAmount);
                }   
            }
            else if (period == GameTime.PERIOD.night)
            {
                if (resources[resource].DayAmount + (production[resource].NightAmount - hourlyNeeds[resource].NightAmount) < 0)
                {
                    // switch by priority
                    //Debug.Log("State: resource shortage for " + resource + " current delta is " + (production[resource].NightAmount - hourlyNeeds[resource].NightAmount).ToString() + 
                        //" current amount " + resources[resource].DayAmount);
                    productionControl.SwitchBuildingByResourceAndPriority(resource, resources[resource].DayAmount - (production[resource].NightAmount - hourlyNeeds[resource].NightAmount), period, hour);
                    return;
                }
                else
                {
                    resources[resource].ChangeDayAmount(production[resource].NightAmount - hourlyNeeds[resource].NightAmount);
                    productionInSum[resource].ChangeDayAmount(production[resource].NightAmount);
                }
                
            } 
        }

        List<ProductionUnit> unitsCanBtTradeByRockets = buildingsControl.IsAnyRocketStartForThisHour(hour);

        if (unitsCanBtTradeByRockets != null)
        {
            if (unitsCanBtTradeByRockets.Count != 0)
            {
                //Debug.Log("State: have trade needs for this hour");

                float amount = 0f;
                foreach (ProductionUnit unit in unitsCanBtTradeByRockets)
                {
                    //Debug.Log("State: have trade price for " + unit.ResourseType);
                    if (resources[unit.ResourseType].DayAmount - unit.DayAmount >= 0)
                    {
                        amount = unit.DayAmount;
                    }
                    else
                    {
                        amount = resources[unit.ResourseType].DayAmount;
                    }

                    resources[unit.ResourseType].ChangeDayAmount(-amount);

                    if (tradePrices.ContainsKey(unit.ResourseType))
                    {
                        resources[Inventory.R_TYPE.money].ChangeDayAmount(amount * tradePrices[unit.ResourseType].DayAmount);
                        productionInSum[Inventory.R_TYPE.money].ChangeDayAmount(amount * tradePrices[unit.ResourseType].DayAmount);
                    }                       
                    else
                    {
                        //Debug.Log("State: do not have trade price for " + unit.ResourseType);
                    }
                }
            }
        }

        if (OnResourcesChange != null)
        {
            OnResourcesChange.Invoke();
        }
    }

    public void SpendResourcesForBuilding(BasicBuilding building)
    {
        // spend resources
        foreach(ProductionUnit unit in building.BuildingNeeds)
        {
            resources[unit.ResourseType].ChangeDayAmount (-unit.DayAmount);
        }

        if (OnResourcesChange != null)
        {
            OnResourcesChange.Invoke();
        }
    }

    public void ChangeResourcesToDailyNeedsForBuilding(BasicBuilding building, int multiplier = 1)
    {
        // add to daily needs
        //foreach (ProductionUnit unit in building.DailyNeeds)
        //{
        //    //dailyNeeds[unit.ResourseType].ChangeDayAmount(multiplier * unit.DayAmount);
        //}

        // check hours, add hours if needed
        //private Dictionary<Inventory.R_TYPE, ProductionUnit> dailyNeeds = building.D
        foreach(int hour in building.HoursToActivateTradeNeeds)
        {
            if (tradeNeedsByHours.ContainsKey(hour))
            {
                //add to daily needs
                foreach (ProductionUnit unit in building.TradeNeeds)
                {
                    if (tradeNeedsByHours[hour].ContainsKey(unit.ResourseType))
                    {
                        tradeNeedsByHours[hour][unit.ResourseType].ChangeDayAmount(multiplier * unit.DayAmount);
                    }
                    else
                    {
                        tradeNeedsByHours[hour].Add(unit.ResourseType, new ProductionUnit(unit.ResourseType, unit.DayAmount));
                    }
                }
            }
            else
            {
                tradeNeedsByHours.Add(hour, new Dictionary<Inventory.R_TYPE, ProductionUnit>());
                foreach (ProductionUnit unit in building.TradeNeeds)
                {
                    tradeNeedsByHours[hour].Add(unit.ResourseType, new ProductionUnit(unit.ResourseType, unit.DayAmount));
                }
            }
        }
    }

    public void ChangeResourcesToHourlyNeedsForBuilding(BasicBuilding building, int multiplier = 1, bool needEventAction = true)
    {
        // add to daily needs
        foreach (ProductionUnit unit in building.HourlyNeeds)
        {
            hourlyNeeds[unit.ResourseType].ChangeDayAmount(unit.DayAmount * multiplier);
            hourlyNeeds[unit.ResourseType].ChangeNightAmount(unit.NightAmount * multiplier);
        }

        if (OnHourlyNeedsChange != null && needEventAction)
        {
            OnHourlyNeedsChange.Invoke();
        }
    }

    public void ChangeResourcesToProductionForBuilding(BasicBuilding building, int multiplier = 1)
    {
        // add to daily needs
        foreach (ProductionUnit unit in building.Production)
        {
            //Debug.Log("State: change production of " + unit.ResourseType + " for amount " + unit.DayAmount + " multiplier " + multiplier);
            production[unit.ResourseType].ChangeDayAmount(unit.DayAmount * multiplier);
            production[unit.ResourseType].ChangeNightAmount(unit.NightAmount * multiplier);
        }

        if (OnProductionChange != null)
        {
            OnProductionChange.Invoke();
        }
    }

    public bool CheckResourcesForBuilding(BuildingItem item)
    {
        foreach (ProductionUnit unit in item.BuildingNeeds)
        {
            if (resources[unit.ResourseType].DayAmount - unit.DayAmount < 0)
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckHourlyNeedsForBuilding(BasicBuilding building, GameTime.PERIOD period)
    {
        // need to add night check

        foreach (ProductionUnit unit in building.HourlyNeeds)
        {
            if (period == GameTime.PERIOD.day)
            {
                if (resources[unit.ResourseType].DayAmount + production[unit.ResourseType].DayAmount - hourlyNeeds[unit.ResourseType].DayAmount - unit.DayAmount < 0)
                {
                    if (building.Type == Inventory.B_TYPE.dome && unit.ResourseType == Inventory.R_TYPE.food && !building.hasBeenOnEver)
                    {
                        //Debug.Log("State: CheckHourlyNeedsForBuilding, special case for dome and food");
                    }
                    else
                    {
                        //Debug.Log("State: recource " + unit.ResourseType + " is not enought for "  + building.ItemName + " recouce number is " + (resources[unit.ResourseType].DayAmount + production[unit.ResourseType].DayAmount - hourlyNeeds[unit.ResourseType].DayAmount - unit.DayAmount));
                        return false;
                    }
                        
                }
            }
            else if (period == GameTime.PERIOD.night)
            {
                if (resources[unit.ResourseType].DayAmount + production[unit.ResourseType].NightAmount - hourlyNeeds[unit.ResourseType].NightAmount - unit.NightAmount < 0)
                {
                    if (building.Type == Inventory.B_TYPE.dome && unit.ResourseType == Inventory.R_TYPE.food && !building.hasBeenOnEver)
                    {
                        //Debug.Log("State: CheckHourlyNeedsForBuilding, special case for dome and food");
                        //return false;
                    }
                    else 
                    { 
                        return false; 
                    }
                }
            }

        }

        return true;
    }

}
