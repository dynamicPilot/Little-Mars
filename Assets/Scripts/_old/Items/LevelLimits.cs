using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelLimits
{
    [Header("Building")]
    [SerializeField] private bool isBuildingNumber;
    public bool IsBuildingNumber { get { return isBuildingNumber; } }
    [SerializeField] private BuildingUnit[] buildings;
    public BuildingUnit[] Buildings {get { return buildings; } }

    [SerializeField] private int timerInHours;
    public int TimerInHoursAfterReaching { get { return timerInHours; } }

    //public int hoursSum;

    [SerializeField] private FullStoryForDescription isBuildingNumberLimitFullDescription;
    public FullStoryForDescription IsBuildingNumberLimitFullDescription { get { return isBuildingNumberLimitFullDescription; } }

    [SerializeField] private FullStoryForDescription isBuildingNumberTimeLimitFullDescription;
    public FullStoryForDescription IsBuildingNumberTimeLimitFullDescription { get { return isBuildingNumberTimeLimitFullDescription; } }

    [Header("Resources")]
    [SerializeField] private bool isResourcesNumber;
    public bool IsResourcesNumber { get { return isResourcesNumber; } }
    [SerializeField] private ProductionUnit[] resources;
    public ProductionUnit[] Resources { get { return resources; } }

    [SerializeField] private FullStoryForDescription isResourcesNumberLimitFullDescription;
    public FullStoryForDescription IsResourcesNumberLimitFullDescription { get { return isResourcesNumberLimitFullDescription; } }

    [Header("Production")]
    [SerializeField] private bool isProductionNumber;
    public bool IsProductionNumber { get { return isProductionNumber; } }
    [SerializeField] private ProductionUnit[] production;
    public ProductionUnit[] Production { get { return production; } }

    [SerializeField] private FullStoryForDescription isProductionNumberLimitFullDescription;
    public FullStoryForDescription IsProductionNumberLimitFullDescription { get { return isProductionNumberLimitFullDescription; } }

    [Header("Production In Sum")]
    [SerializeField] private bool isProductionInSumNumber;
    public bool IsProductionInSumNumber { get { return isProductionInSumNumber; } }
    [SerializeField] private ProductionUnit[] productionInSum;
    public ProductionUnit[] ProductionInSum { get { return productionInSum; } }

    [SerializeField] private FullStoryForDescription isProductionInSumNumberLimitFullDescription;
    public FullStoryForDescription IsProductionInSumNumberLimitFullDescription { get { return isProductionInSumNumberLimitFullDescription; } }

    [Header("Map Slots")]
    [SerializeField] private bool isMapSlotNumber;
    public bool IsMapSlotNumber { get { return isMapSlotNumber; } }
    [SerializeField] private int slots;
    public int Slots { get { return slots; } }

    [SerializeField] private FullStoryForDescription isMapSlotNumberLimitFullDescription;
    public FullStoryForDescription IsMapSlotNumberLimitFullDescription { get { return isMapSlotNumberLimitFullDescription; } }

    [Header("Days")]
    [SerializeField] private bool isDayNumber;
    public bool IsDayNumber { get { return isDayNumber; } }
    [SerializeField] private int days;
    public int Days { get { return days; } }

    [SerializeField] private FullStoryForDescription isDayNumberLimitFullDescription;
    public FullStoryForDescription IsDayNumberLimitFullDescription { get { return isDayNumberLimitFullDescription; } }

    //private List<string> allDescriptions = new List<string>();
    //public List<string> AllDescriptions { get { return allDescriptions; } }

    //public LevelLimits()
    //{
    //    allDescriptions.Add(isBuildingNumberLimitDescriptions);
    //    allDescriptions.Add(isBuildingNumberTimeLimitDescriptions);
    //    allDescriptions.Add(isResourcesNumberLimitDescriptions);
    //    allDescriptions.Add(isProductionNumberLimitDescriptions);
    //    allDescriptions.Add(isMapSlotNumberLimitDescriptions);
    //    allDescriptions.Add(isDayNumberLimitDescriptions);
    //}

    //public void UpdateAllDescriptions()
    //{
    //    allDescriptions.Clear();
    //    allDescriptions.Add(isBuildingNumberLimitDescriptions);
    //    allDescriptions.Add(isBuildingNumberTimeLimitDescriptions);
    //    allDescriptions.Add(isResourcesNumberLimitDescriptions);
    //    allDescriptions.Add(isProductionNumberLimitDescriptions);
    //    allDescriptions.Add(isMapSlotNumberLimitDescriptions);
    //    allDescriptions.Add(isDayNumberLimitDescriptions);
    //}
}
