using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreator : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform slotPrefab;
    [SerializeField] private List<MapSlot> slots;
    public List<MapSlot> Slots { get { return slots; } }

    [Header("Options")]
    [SerializeField] private int maxIterationForFieldCreator = 10;

    [Header("Scripts")]
    [SerializeField] private BuildingPlacementMenu buildingPlacementMenu;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ProductionControl productionControl;
    [SerializeField] private IconStorage iconStorage;
    [SerializeField] private GameTime gameTime;

    private MapChecking mapChecking;
    private GridLayoutGroup gridLayout;
    private LevelInfo levelInfo;

    private void Awake()
    {
        gridLayout = slotParent.GetComponent<GridLayoutGroup>();
        mapChecking = GetComponent<MapChecking>();
    }

    public void CreateMapForLevel(LevelInfo newLevelInfo)
    {
        levelInfo = newLevelInfo;
        int slotNumberMustBe = CheckMapColumnAndRowCountersAndReturnTotalSlotNumber();
        slots = new List<MapSlot>();
        slots.Clear();

        // add existing
        foreach (MapSlot slot in slotParent.GetComponentsInChildren<MapSlot>())
        {
            slot.GetComponent<MapSlotDropEventSystem>().SetScriptLinks(mapChecking, buildingPlacementMenu, inventory, productionControl, iconStorage, gameTime);
            slot.MakeEmpty();
            slot.listIndex = slots.Count;
            slot.AvailableTypes = CustomFunctions.MakeFullCopyOfListOfBuildingsType(levelInfo.DefaultBuildingTypes);
            slots.Add(slot);
            
            slotNumberMustBe -= 1;
        }

        if (!levelInfo.AutoMap)
        {
            if (levelInfo.CustomeAutoMap != null)
            {
                CreateMapByCustomAutoMap(slotNumberMustBe);

                if (levelInfo.AutoFields)
                {
                    CreateMapResourcesAndBuildingsFields();
                }

                return;
            }
            
            if (slotNumberMustBe == 0)
            {
                FillSlotNeighbors();

                if (levelInfo.AutoFields)
                {
                    CreateMapResourcesAndBuildingsFields();
                }

                return;
            }    
            else
            {
                return;
            }
        }

        CreateSlotsAndFillNeighbors(slotNumberMustBe);
        //if (gridLayout == null)
        //    gridLayout = slotParent.GetComponent<GridLayoutGroup>();

        //// fixed columns
        //gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        //gridLayout.constraintCount = levelInfo.ColumnCounter;

        //if (slotNumberMustBe == 0)
        //{
        //    FillSlotNeighbors();
        //    return;
        //}

        //for (int i = 0; i < slotNumberMustBe; i++)
        //{
        //    MapSlot newSlot = Instantiate(slotPrefab, slotParent).GetComponent<MapSlot>();
        //    newSlot.GetComponent<MapSlotDropEventSystem>().SetScriptLinks(mapChecking, buildingPlacementMenu, inventory, productionControl, iconStorage, gameTime);
        //    newSlot.MakeEmpty();
        //    newSlot.listIndex = slots.Count;
        //    newSlot.AvailableTypes = levelInfo.DefaultBuildingTypes;
        //    slots.Add(newSlot);

        //}

        //FillSlotNeighbors();

        if (levelInfo.AutoFields)
            CreateMapResourcesAndBuildingsFields();
    }

    int CheckMapColumnAndRowCountersAndReturnTotalSlotNumber()
    {
        if (levelInfo.CustomeAutoMap != null && !levelInfo.AutoMap)
        {
            if (levelInfo.CustomeAutoMap.RowCounter != levelInfo.RowCounter)
            {
                levelInfo.RowCounter = levelInfo.CustomeAutoMap.RowCounter;
            }

            if (levelInfo.CustomeAutoMap.ColumnCounter != levelInfo.ColumnCounter)
            {
                levelInfo.ColumnCounter = levelInfo.CustomeAutoMap.ColumnCounter;
            }
        }

        return levelInfo.ColumnCounter * levelInfo.RowCounter;
    }

    void CreateMapByCustomAutoMap(int currentSlotNumberMustBe)
    {
        //Debug.Log("MapCreator: start create map for custom map...");
        CreateSlotsAndFillNeighbors(currentSlotNumberMustBe);

        // set every slot parameter
        for (int i = 0; i< levelInfo.CustomeAutoMap.RowCounter; i++)
        {
            for (int j = 0; j < levelInfo.CustomeAutoMap.ColumnCounter; j++)
            {
                int index = i * levelInfo.CustomeAutoMap.ColumnCounter + j;
                // is blocked
                slots[index].MakeBlocked(levelInfo.CustomeAutoMap.MapByLines[i].Line[j].IsBlock);

                if (levelInfo.CustomeAutoMap.MapByLines[i].Line[j].IsResources != null)
                {
                    // resources
                    foreach (Inventory.R_TYPE type in levelInfo.CustomeAutoMap.MapByLines[i].Line[j].IsResources)
                    {
                        slots[index].AddTypeToAvailableResources(type);
                    }
                }

                if (levelInfo.CustomeAutoMap.MapByLines[i].Line[j].IsBuildings != null)
                {
                    // buildings
                    foreach (Inventory.B_TYPE type in levelInfo.CustomeAutoMap.MapByLines[i].Line[j].IsBuildings)
                    {
                        //Debug.Log("MapCreator: add type to " + type);
                        slots[index].AddTypeToAvailableTypes(type);
                    }
                }

            }
        }
    }

    void CreateSlotsAndFillNeighbors(int currentSlotNumberMustBe, bool fillNeighbors = true)
    {
        if (gridLayout == null)
            gridLayout = slotParent.GetComponent<GridLayoutGroup>();

        // fixed columns
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = levelInfo.ColumnCounter;

        if (currentSlotNumberMustBe == 0)
        {
            FillSlotNeighbors();
            return;
        }

        for (int i = 0; i < currentSlotNumberMustBe; i++)
        {
            MapSlot newSlot = Instantiate(slotPrefab, slotParent).GetComponent<MapSlot>();
            newSlot.GetComponent<MapSlotDropEventSystem>().SetScriptLinks(mapChecking, buildingPlacementMenu, inventory, productionControl, iconStorage, gameTime);
            newSlot.MakeEmpty();
            newSlot.listIndex = slots.Count;
            newSlot.AvailableTypes = CustomFunctions.MakeFullCopyOfListOfBuildingsType(levelInfo.DefaultBuildingTypes);
            slots.Add(newSlot);
        }

        if (fillNeighbors) FillSlotNeighbors();
    }


    void CreateMapResourcesAndBuildingsFields()
    {
        foreach(MapField field in levelInfo.Fields)
        {
            int interationCutter = 0;
            KeyValuePair<List<MapSlot>, int> result = new KeyValuePair<List<MapSlot>, int>(null, -1);
            while (interationCutter <= maxIterationForFieldCreator && result.Value == -1)
            {
                interationCutter++;
                // select start slot
                MapSlot startSlot = slots[Random.Range(0, slots.Count)];
                //Debug.Log("MapChecking: start slot is " + startSlot.listIndex);
                // try to place way
                result = mapChecking.CheckForFieldWay(field.Way, startSlot);
            }

            if (result.Value != -1 && result.Key != null)
            {
                // create field
                foreach (MapSlot slot in result.Key)
                {
                    if (field.ResourseType != Inventory.R_TYPE.none && field.ResourseType != Inventory.R_TYPE.all)
                    {
                        slot.AddTypeToAvailableResources(field.ResourseType);
                        //slot.AddToSlotPossibleProduction(field.Production);
                    }

                    if (field.BuildingType != Inventory.B_TYPE.none && field.BuildingType != Inventory.B_TYPE.all && !levelInfo.DefaultBuildingTypes.Contains(field.BuildingType))
                    {
                        slot.AddTypeToAvailableTypes(field.BuildingType);
                        //slot.AddToSlotPossibleProduction(field.Production);
                    }
                }
            }
        }

        
    }

    void FillSlotNeighbors()
    {
        //int indexInSlots = 0;
        for (int col = 0; col < levelInfo.ColumnCounter; col++)
        {
            for (int row = 0; row < levelInfo.RowCounter; row++)
            {
                MapSlot currentSlot = slots[row*levelInfo.ColumnCounter + col];

                if (col > 0)
                {
                    currentSlot.Neighbors[3] = slots[currentSlot.listIndex - 1];
                }

                if (col < levelInfo.ColumnCounter - 1)
                {
                    currentSlot.Neighbors[1] = slots[currentSlot.listIndex + 1];
                }

                if (row > 0)
                {
                    currentSlot.Neighbors[0] = slots[currentSlot.listIndex - levelInfo.ColumnCounter];
                }

                if (row < levelInfo.RowCounter - 1)
                {
                    currentSlot.Neighbors[2] = slots[currentSlot.listIndex + levelInfo.ColumnCounter];
                }

                //indexInSlots++;
            }
        }
    }


}
