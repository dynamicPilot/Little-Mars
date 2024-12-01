using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapUnit
{
    [SerializeField] private bool isBlock = false;
    public bool IsBlock { get { return isBlock; } }

    [SerializeField] private Inventory.R_TYPE[] isResources;
    public Inventory.R_TYPE[] IsResources { get{ return isResources; } }


    [SerializeField] private Inventory.B_TYPE[] isBuildings;
    public Inventory.B_TYPE[] IsBuildings { get { return isBuildings; } }
}

[System.Serializable]
public class MapUnitLine
{
    [SerializeField] private MapUnit[] line;
    public MapUnit[] Line { get { return line; } }
}
