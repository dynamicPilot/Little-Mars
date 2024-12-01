using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapField
{
    [SerializeField] private Inventory.R_TYPE resourseType;
    [SerializeField] private Inventory.B_TYPE buildingType;
    [SerializeField] private BuildingWayStep[] way;
    //[SerializeField] ProductionUnit production;

    public Inventory.R_TYPE ResourseType { get { return resourseType; } }
    public Inventory.B_TYPE BuildingType { get { return buildingType; } }
    public BuildingWayStep[] Way {get { return way; } }
    //public ProductionUnit Production { get { return production; } }
}
