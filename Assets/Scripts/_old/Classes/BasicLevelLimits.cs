using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicLevelLimits
{
    [Header("Building")]
    [SerializeField] private int isBuildingNumberIndex;
    public int IsBuildingNumberIndex { get { return isBuildingNumberIndex; } }

    [SerializeField] private bool isBuildingNumber;
    public bool IsBuildingNumber { get { return isBuildingNumber; } }

    [SerializeField] private BuildingUnit[] buildings;
    public BuildingUnit[] Buildings { get { return buildings; } }

    [SerializeField] private int timerInHours;
    public int TimerInHoursAfterReaching { get { return timerInHours; } }

    [SerializeField] private int isBuildingNumberTimerIndex;
    public int IsBuildingNumberTimerIndex { get { return isBuildingNumberTimerIndex; } }

    public int hoursSum;

    [TextArea] [SerializeField] private string isBuildingNumberLimitDescriptions;
    public string IsBuildingNumberLimitDescriptions { get { return isBuildingNumberLimitDescriptions; } }

    [TextArea] [SerializeField] private string isBuildingNumberTimeLimitDescriptions;
    public string IsBuildingNumberTimeLimitDescriptions { get { return isBuildingNumberTimeLimitDescriptions; } }

    [Header("Resources")]
    [SerializeField] private int isResourcesNumberIndex;
    public int IsResourcesNumberIndex { get { return isResourcesNumberIndex; } }

    [SerializeField] private bool isResourcesNumber;
    public bool IsResourcesNumber { get { return isResourcesNumber; } }
    [SerializeField] private ProductionUnit[] resources;
    public ProductionUnit[] Resources { get { return resources; } }

    [TextArea] [SerializeField] private string isResourcesNumberLimitDescriptions;
    public string IsResourcesNumberLimitDescriptions { get { return isResourcesNumberLimitDescriptions; } }

    [Header("Production")]
    [SerializeField] private int isProductionNumberIndex;
    public int IsProductionNumberIndex { get { return isProductionNumberIndex; } }

    [SerializeField] private bool isProductionNumber;
    public bool IsProductionNumber { get { return isProductionNumber; } }

    [SerializeField] private ProductionUnit[] production;
    public ProductionUnit[] Production { get { return production; } }

    [TextArea] [SerializeField] private string isProductionNumberLimitDescriptions;
    public string IsProductionNumberLimitDescriptions { get { return isProductionNumberLimitDescriptions; } }

    [Header("Production In Sum")]
    [SerializeField] private int isProductionInSumNumberIndex;
    public int IsProductionInSumNumberIndex { get { return isProductionInSumNumberIndex; } }

    [SerializeField] private bool isProductionInSumNumber;
    public bool IsProductionInSumNumber { get { return isProductionInSumNumber; } }
    [SerializeField] private ProductionUnit[] productionInSum;
    public ProductionUnit[] ProductionInSum { get { return productionInSum; } }

    [TextArea] [SerializeField] private string isProductionInSumNumberLimitDescriptions;
    public string IsProductionInSumNumberLimitDescriptions { get { return isProductionInSumNumberLimitDescriptions; } }

    [Header("Map Slots")]
    [SerializeField] private int isMapSlotNumberIndex;
    public int IsMapSlotNumberIndex { get { return isMapSlotNumberIndex; } }

    [SerializeField] private bool isMapSlotNumber;
    public bool IsMapSlotNumber { get { return isMapSlotNumber; } }
    [SerializeField] private int slots;
    public int Slots { get { return slots; } }

    [TextArea] [SerializeField] private string isMapSlotNumberLimitDescriptions;
    public string IsMapSlotNumberLimitDescriptions { get { return isMapSlotNumberLimitDescriptions; } }

    [Header("Days")]
    [SerializeField] private int isDayNumberIndex;
    public int IsDayNumberIndex { get { return isDayNumberIndex; } }

    [SerializeField] private bool isDayNumber;
    public bool IsDayNumber { get { return isDayNumber; } }
    [SerializeField] private int days;
    public int Days { get { return days; } }

    [TextArea] [SerializeField] private string isDayNumberLimitDescriptions;
    public string IsDayNumberLimitDescriptions { get { return isDayNumberLimitDescriptions; } }

    private List<string> allDescriptions;
    public List<string> AllDescriptions { get { return allDescriptions; } }

    public List<bool> everReachThisLimit;

    public BasicLevelLimits(LevelLimits newLevelLimits, GameMaster.LANG lang = GameMaster.LANG.eng)
    {
        int index = 0;
        allDescriptions = new List<string>();
        everReachThisLimit = new List<bool>();

        //Debug.Log("BasicLevelLimits: create new limits, language is " + lang);
        // building

        isBuildingNumber = newLevelLimits.IsBuildingNumber;
        isBuildingNumberIndex = -1;

        if (isBuildingNumber)
        {
            buildings = CustomFunctions.MakeFullCopyOfArrayOfBuildingUnits(newLevelLimits.Buildings);
            isBuildingNumberLimitDescriptions = newLevelLimits.IsBuildingNumberLimitFullDescription.GetDescriptionByLang(lang);
            isBuildingNumberIndex = index;
            index = SetIndex(index, isBuildingNumberLimitDescriptions);

            timerInHours = newLevelLimits.TimerInHoursAfterReaching;
            isBuildingNumberTimerIndex = -1;
            hoursSum = -1;

            if (timerInHours > 0)
            {
                // have timer
                isBuildingNumberTimeLimitDescriptions = newLevelLimits.IsBuildingNumberTimeLimitFullDescription.GetDescriptionByLang(lang);
                isBuildingNumberTimerIndex = index;
                index = SetIndex(index, isBuildingNumberTimeLimitDescriptions);
                
            }
        }

        // resources

        isResourcesNumber = newLevelLimits.IsResourcesNumber;
        isResourcesNumberIndex = -1;

        if (isResourcesNumber)
        {
            resources = CustomFunctions.MakeFullCopyOfArrayOfProductionUnits(newLevelLimits.Resources);
            isResourcesNumberLimitDescriptions = newLevelLimits.IsResourcesNumberLimitFullDescription.GetDescriptionByLang(lang);
            isResourcesNumberIndex = index;
            index = SetIndex(index, isResourcesNumberLimitDescriptions);
        }

        // production

        isProductionNumber = newLevelLimits.IsProductionNumber;
        isProductionNumberIndex = -1;

        if (isProductionNumber)
        {
            production = CustomFunctions.MakeFullCopyOfArrayOfProductionUnits(newLevelLimits.Production);
            isProductionNumberLimitDescriptions = newLevelLimits.IsProductionNumberLimitFullDescription.GetDescriptionByLang(lang);
            isProductionNumberIndex = index;
            index = SetIndex(index, isProductionNumberLimitDescriptions);
        }

        // productionInSum

        isProductionInSumNumber = newLevelLimits.IsProductionInSumNumber;
        isProductionInSumNumberIndex = -1;

        if (isProductionInSumNumber)
        {
            productionInSum = CustomFunctions.MakeFullCopyOfArrayOfProductionUnits(newLevelLimits.ProductionInSum);
            isProductionInSumNumberLimitDescriptions = newLevelLimits.IsProductionInSumNumberLimitFullDescription.GetDescriptionByLang(lang);
            isProductionInSumNumberIndex = index;
            index = SetIndex(index, isProductionInSumNumberLimitDescriptions);
        }

        // mapSlots

        isMapSlotNumber = newLevelLimits.IsMapSlotNumber;
        isMapSlotNumberIndex = -1;

        if (isMapSlotNumber)
        {
            slots = newLevelLimits.Slots;
            isMapSlotNumberLimitDescriptions = newLevelLimits.IsMapSlotNumberLimitFullDescription.GetDescriptionByLang(lang);
            isMapSlotNumberIndex = index;
            index = SetIndex(index, isMapSlotNumberLimitDescriptions);
        }

        // days

        isDayNumber = newLevelLimits.IsDayNumber;
        isDayNumberIndex = -1;

        if (isDayNumber)
        {
            days = newLevelLimits.Days;
            isDayNumberLimitDescriptions = newLevelLimits.IsDayNumberLimitFullDescription.GetDescriptionByLang(lang);
            isDayNumberIndex = index;
            index = SetIndex(index, isDayNumberLimitDescriptions);
        }

    }

    int SetIndex(int currentIndex, string description)
    {
        allDescriptions.Add(description);
        everReachThisLimit.Add(false);
        return currentIndex + 1;
    }
}
