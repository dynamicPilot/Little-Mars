using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Info", menuName = "Level Info")]
public class LevelInfo : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string levelName;
    [SerializeField] private int number;
    public int Number { get { return number; } }
    //[SerializeField] private string description;

    [SerializeField] private Sprite startIcon;
    public Sprite StartIcon { get { return startIcon; } }

    [SerializeField] private Sprite winningIcon;
    public Sprite WinningIcon { get { return winningIcon; } }

    [Header("Scene")]
    [SerializeField] private string sceneName;
    public string SceneName { get { return sceneName; } }

    //[SerializeField] private int index = -1;
    //public int Index { get { return index; } set { index = value; } }

    [Header("State")]
    [SerializeField] private LevelInfo[] levelsToBecomeAvailable;
    public LevelInfo[] LevelsToBecomeAvailable { get { return levelsToBecomeAvailable; } }

    [Header("Map")]
    [SerializeField] private bool autoMap = true;
    public bool AutoMap { get { return autoMap; } }

    [SerializeField] private CustomMap customeAutoMap;
    public CustomMap CustomeAutoMap { get { return customeAutoMap; } }

    [SerializeField] private int rowCounter;
    public int RowCounter { get { return rowCounter; } set { rowCounter = value; } }

    [SerializeField] private int columnCounter;
    public int ColumnCounter { get { return columnCounter; } set { columnCounter = value; } }

    [SerializeField] private bool autoFields = true;
    public bool AutoFields { get { return autoFields; } }

    [SerializeField] private MapField[] fields;
    public MapField[] Fields { get { return fields; } }

    [SerializeField] private List<Inventory.B_TYPE> defaultBuildingTypes;
    public List<Inventory.B_TYPE> DefaultBuildingTypes { get { return defaultBuildingTypes; } }

    [Header("Buildings")]
    [SerializeField] private BuildingUnit[] buildings;
    public BuildingUnit[] Buildings { get { return buildings; } }

    [Header("Start State")]
    [SerializeField] private List<ProductionUnit> startResources;
    public List<ProductionUnit> StartResources { get { return startResources; } }

    [Header("Trading Price")]
    [SerializeField] private List<ProductionUnit> tradePrices;
    public List<ProductionUnit> TradePrices { get { return tradePrices; } }

    [Header("Limits")]
    [SerializeField] private bool needWaitingBeforeWinning = false;
    public bool NeedWaitingBeforeWinning { get { return needWaitingBeforeWinning; } }

    [SerializeField] private LevelLimits winningLimit;
    public LevelLimits WinningLimit { get { return winningLimit; } }
    [SerializeField] private LevelLimits endOfLevelLimit;
    public LevelLimits EndOfLevelLimit { get { return endOfLevelLimit; } }

    [Header("Training Story")]
    //[SerializeField] private bool needTraining;
    //public bool NeedTraining { get { return needTraining; } }

    [SerializeField] private FullStory trainingStory;
    public FullStory TrainingStory { get { return trainingStory; } }


}
