using System.Collections.Generic;
using UnityEngine;

public class ProductionControl : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private State state;
    [SerializeField] private BuildingTurnOffTimer buildingTurnOffTimer;
    [SerializeField] private GameTime gameTime;
    [SerializeField] private BuildingPlacementMenu buildingPlacementMenu;

    private Inventory inventory;

    public delegate void BuildingsTurnOnOff();
    public event BuildingsTurnOnOff OnBuildingsTurnOnOff;

    private void Awake()
    {
        inventory = state.GetComponent<Inventory>();
        gameTime.OnPeriodToNewDay += CheckBuildingByPeriodChange;
        gameTime.OnPeriodToNewNight += CheckBuildingByPeriodChange;
    }

    void CheckBuildingByPeriodChange()
    {
        //Debug.Log("ProductionControl: period change!");
        AutoTurnOnForAllBuilding();

        foreach (BasicBuilding building in inventory.Buildings)
        {
            building.SetAnimatorMode(true);
        }
    }

    public bool CheckBuildingForCanBeOn(BasicBuilding building)
    {
        List<MapSlot> checkedSlots = new List<MapSlot> ();
        List<Inventory.B_TYPE> currentConnections = new List<Inventory.B_TYPE>();

        //if (building.hasBeenOnEver)
        //{
        //    return true;
        //}

        //Debug.Log("ProductionControl: start check for " + building.ItemName);
        foreach (MapSlot slot in building.slots)
        {
            if (slot == null)
            {
                continue;
            }
            foreach(MapSlot nSlot in slot.Neighbors)
            {
                if (nSlot == null)
                {
                    continue;
                }

                //Debug.Log("ProductionControl: find neighbor");
                if (!checkedSlots.Contains(nSlot))
                {
                    //Debug.Log("ProductionControl: find neighbor");
                    checkedSlots.Add(nSlot);

                    BasicBuilding nBuilding = nSlot.GetBuilding();

                    if (nBuilding != null)
                    {
                        //Debug.Log("ProductionControl: find neighbor building with type " + nBuilding.Type + " isOn " + nBuilding.IsOn);
                        if (nBuilding != building)// && nBuilding.IsOn)
                        {
                            //Debug.Log("ProductionControl: find connection with type " + nBuilding.Type);
                            currentConnections.Add(nBuilding.Type);
                        }
                    }
                }
            }
        }

        foreach(Inventory.B_TYPE type in building.ConnectionTypes)
        {
            if (!currentConnections.Contains(type))
            {
                //Debug.Log("ProductionControl: can not be on, do not have " + type);
                return false;
            }
        }

        return true;
    }

    public void TurnBuildingOn(BasicBuilding building, bool needCheckAfter = true)
    {
        //Debug.Log("ProductionControl: try turn on building " + building.ItemName);
        building.CanBeOn = CheckBuildingForCanBeOn(building);

        if (building.isTurnOffManually)
        {
            //Debug.Log("ProductionControl: can be turn on --> turn off by hands");
            return;
        }

        if (!building.CanBeOn)
        {
            //Debug.Log("ProductionControl: is turn off because can not be on");
            if (building.IsOn)
            {
                TurnBuildingOff(building, needCheckAfter);
            }
            return;
        }

        if (!building.OperatingMode[(int)gameTime.Period])
        {
            //Debug.Log("ProductionControl: is turn off by operating mode");
            if (building.IsOn)
            {
                TurnBuildingOff(building, needCheckAfter);
            }
            return;
        }

        if (building.IsOn)
        {            
            return;
        }

        //check resources for hour
        if (!state.CheckHourlyNeedsForBuilding(building, gameTime.Period))
        {
            //Debug.Log("ProductionControl: can not be on, not enoght hourly needs");
            return;
        }

        // turn on
        //Debug.Log("ProductionControl: turn on " + building.ItemName);
        building.IsOn = true;
        building.hasBeenOnEver = true;
        building.isTurnOffManually = false;
        building.SetAnimatorMode();
        // daily and hourly needs
        //state.ChangeResourcesToDailyNeedsForBuilding(building);
        state.ChangeResourcesToHourlyNeedsForBuilding(building);

        // production
        state.ChangeResourcesToProductionForBuilding(building);

        // upadte building placement menu
        buildingPlacementMenu.UpdateChangeOrDestroyMenu(building);

        if (OnBuildingsTurnOnOff != null)
        {
            OnBuildingsTurnOnOff.Invoke();
        }

        if (needCheckAfter)
            AutoTurnOnForAllBuilding(building);
    }

    public void AutoTurnOnForAllBuilding(BasicBuilding exceptBuilding = null)
    {
        foreach(BasicBuilding building in inventory.Buildings)
        {
            if (exceptBuilding != null)
            {
                if (building == exceptBuilding)
                {
                    continue;
                }
            }
            //Debug.Log("ProductionControl: do auto check " + building.ItemName);
            TurnBuildingOn(building, false);
        }
    }

    public void TurnBuildingOff(BasicBuilding building, bool needCheckAfter = true, bool isDestroy = false)
    {
       
        if (!building.IsOn)
        {
            return;
        }

        //Debug.Log("ProductionControl: try turn off building " + building.ItemName);
        building.IsOn = false;
        building.SetAnimatorMode();

        // check resources for hour
        state.ChangeResourcesToHourlyNeedsForBuilding(building, -1);
        state.ChangeResourcesToProductionForBuilding(building, -1);

        // upadte building placement menu
        buildingPlacementMenu.UpdateChangeOrDestroyMenu(building);

        if (building.Priority == State.PRIORITY.very_high && building.hasBeenOnEver && !isDestroy)
        {
            buildingTurnOffTimer.StartTimer(building);
        }

        if (OnBuildingsTurnOnOff != null)
        {
            OnBuildingsTurnOnOff.Invoke();
        }

        if (needCheckAfter)
        {
            //Debug.Log("ProductionControl: do auto turn on for all buildings");
            AutoTurnOnForAllBuilding(building);
        }
            
    }

    public void TurnBuildingOffManually(BasicBuilding building, bool needCheckAfter = true)
    {
        building.isTurnOffManually = true;
        TurnBuildingOff(building, needCheckAfter);
    }

    public void TurnBuildingOnManually(BasicBuilding building, bool needCheckAfter = true)
    {
        building.isTurnOffManually = false;
        TurnBuildingOn(building, needCheckAfter);
    }

    public void SwitchBuildingByResourceAndPriority(Inventory.R_TYPE resource, float delta, GameTime.PERIOD period, int hour)
    {
        //Time.timeScale = 0f;
        //Debug.Log("ProductionControl: start searching for suitable building with  " + resource);
        float currentDelta = delta;
        Dictionary<State.PRIORITY, List<BasicBuilding>> buildingWithNeeds = new Dictionary<State.PRIORITY, List<BasicBuilding>>();
        Dictionary<State.PRIORITY, List<ProductionUnit>> buildingNeedsUnits = new Dictionary<State.PRIORITY, List<ProductionUnit>>();
        for (int i = 3; i >= 0; i--)
        {
            //Debug.Log("ProductionControl: i index for priority is " + i);
            buildingWithNeeds[(State.PRIORITY)i] = null;
        }
        //List<ProductionUnit> needsUnits = new List<ProductionUnit>();


        foreach (BasicBuilding building in inventory.Buildings)
        {
            if (!building.IsOn)
            {
                continue;
            }

            foreach (ProductionUnit unit in building.HourlyNeeds)
            {
                if (unit.ResourseType == resource)
                {
                    if (buildingWithNeeds[building.Priority] == null)
                    {
                        buildingWithNeeds[building.Priority] = new List<BasicBuilding>();
                        buildingNeedsUnits[building.Priority] = new List<ProductionUnit>();
                    }

                    buildingWithNeeds[building.Priority].Add(building);
                    buildingNeedsUnits[building.Priority].Add(unit);
                }
            }
        }

        // check for every priority
        //State.PRIORITY[] priorities = new State.PRIORITY[4] { State.PRIORITY.low, State.PRIORITY.medium, State.PRIORITY.high, State.PRIORITY.very_high };
        for (int i = 3; i >= 0; i--)
        {
            //Debug.Log("ProductionControl: priority index is " + i);
            if (buildingWithNeeds[(State.PRIORITY) i] != null)
            {
                if (buildingWithNeeds[(State.PRIORITY)i].Count > 0)
                {
                    for (int j = 0; j < buildingWithNeeds[(State.PRIORITY)i].Count; j++)
                    {
                        //Debug.Log("ProductionControl: j index is " + j +  " of " + buildingWithNeeds[(State.PRIORITY)i].Count + " building " + buildingWithNeeds[(State.PRIORITY)i][j].ItemName);

                        if (!buildingWithNeeds[(State.PRIORITY)i][j].IsOn)
                        {
                            continue;
                        }

                        //Debug.Log("ProductionControl: turn off " + buildingWithNeeds[(State.PRIORITY)i][j].ItemName + " current delta " + currentDelta);
                        TurnBuildingOff(buildingWithNeeds[(State.PRIORITY)i][j]);

                        // needs
                        //if (period == GameTime.PERIOD.day)
                        //{
                        //    currentDelta -= buildingNeedsUnits[(State.PRIORITY)i][j].DayAmount;
                        //}
                        //else
                        //{
                        //    currentDelta -= buildingNeedsUnits[(State.PRIORITY)i][j].NightAmount;
                        //}

                        //

                        currentDelta = state.CalculateActualHourlyProductionAndNeedsByResource(period, resource);
                        //Debug.Log("ProductionControl: new current delta " + currentDelta);
                        if (currentDelta <= 0)
                        {
                            //Debug.Log("ProductionControl: return to state, current delta " + currentDelta);
                            //Time.timeScale = 1f;
                            state.CalculateHourlyProductionAndNeeds(period, hour);
                            return;
                        }
                        else
                        {
                            //Debug.Log("ProductionControl: continue");
                        }

                    }
                }
                
            }
        }
        //Time.timeScale = 1f;

    }
}
