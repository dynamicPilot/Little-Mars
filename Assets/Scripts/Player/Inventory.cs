using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum B_TYPE { dome, power_plant, mine, farm, factory, supply_plant, workshop, cosmodrome, all, none }
    public enum R_TYPE { money, metalls, food, machines, energy, goods, supply_units, all, none }

    [SerializeField] private List<BasicBuilding> buildings;
    public List<BasicBuilding> Buildings { get { return buildings; } }

    [Header("Cosmodromes")]
    [SerializeField] private List<BasicBuilding> cosmodromes;
    public List<BasicBuilding> Cosmodromes { get { return cosmodromes; } }

    [Header("Scripts")]
    [SerializeField] private BuildingsControl buildingsControl;

    private State state;
    private ProductionControl productionControl;

    private void Awake()
    {
        buildings = new List<BasicBuilding>();
        cosmodromes = new List<BasicBuilding>();
        state = GetComponent<State>();
        productionControl = buildingsControl.GetComponent<ProductionControl>();
        
    }

    public void AddBuilding(BasicBuilding newBuilding, BuildingItem buildingItem)
    {
        buildings.Add(newBuilding);

        if (buildingItem.Type == B_TYPE.cosmodrome)
        {
            cosmodromes.Add(newBuilding);
        }

        // calculate costs
        state.SpendResourcesForBuilding(newBuilding);
        state.ChangeResourcesToDailyNeedsForBuilding(newBuilding);

        buildingsControl.MakeBuilding(buildingItem);

        // check production
        productionControl.TurnBuildingOn(newBuilding);
    }

    public void RemoveBuilding(BasicBuilding newBuilding)
    {
        // turn off
        productionControl.TurnBuildingOff(newBuilding, true, true);

        // calculate daily resources
        state.ChangeResourcesToDailyNeedsForBuilding(newBuilding, -1);

        // correct bulding amount
        buildingsControl.DestroyBuilding(newBuilding);

        // remove
        buildings.Remove(newBuilding);

        if (newBuilding.Type == B_TYPE.cosmodrome)
        {
            cosmodromes.Remove(newBuilding);
        }
    }

    public bool CheckForResourcesBeforeBuilding(BuildingItem buildingItem)
    {
        // check amount
        if (buildingItem.AvailableAmount < 0 || !buildingItem.IsAvailable)
        {
            return false;
        }

        // check resources
        return state.CheckResourcesForBuilding(buildingItem);
    }

    public int GetNumberOfBuildingOfType(B_TYPE type)
    {
        int counter = 0;

        foreach (BasicBuilding building in buildings)
        {
            if (building.Type == type && building.hasBeenOnEver && type != B_TYPE.dome)
            {
                counter++;
            }
            else if (building.Type == type && building.IsOn && type == B_TYPE.dome)
            {
                counter++;
            }
        }

        return counter;
    }
}
