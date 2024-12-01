using UnityEngine;
using UnityEngine.UI;

public class MenuSlot : BasicSlot
{
    [SerializeField] private BuildingItem buildingItem;
    [SerializeField] private ResourcesCostOrNeedsForBuildingMenu buildingCost;
    [SerializeField] private BuildingInfo info;
    [SerializeField] private BuildingConnectionMenu buildingConnectionMenu;
    [SerializeField] private Text buildingNumberText;

    [Header("Options")]
    [SerializeField] private int soundIndex = 0;

    [Header("Options")]
    [SerializeField] private bool needSetOnAwake;
    [SerializeField] private bool needClickSound = true;

    [Header("Scripts")]
    [SerializeField] private GameMaster gameMaster;

    private AudioControl audioControl;
    private IconStorage iconStorage;

    private void Awake()
    {
        if (buildingItem != null && needSetOnAwake)
        {
            AddItem(buildingItem);
        }

        info.gameObject.SetActive(false);
        audioControl = AudioControl.Instance;
    }

    public void SetObjectLinks(GameMaster newGameMaster)
    {
        gameMaster = newGameMaster;
    }

    public void AddItem(BuildingItem newBuildingItem, IconStorage newIconStorage = null)
    {
        buildingItem = newBuildingItem;
        iconStorage = newIconStorage;
        SetImage(buildingItem.Icon);

        buildingCost.SetUI(buildingItem, iconStorage);
        SetConnectionsMenu();
        SetBuildingNumber();
    }

    public void RemoveItem()
    {
        buildingItem = null;
        RemoveImage();
    }

    public void ShowInfo()
    {
        info.gameObject.SetActive(true);
        if (needClickSound) audioControl.PlayClickSound(soundIndex);
        info.SetInfo(buildingItem, iconStorage, gameMaster.Lang);
    }

    public void HideInfo()
    {
        info.gameObject.SetActive(false);
        
    }

    public BuildingItem GetItem()
    {
        return buildingItem;
    }

    void SetConnectionsMenu()
    {
        if (buildingItem.ConnectionTypes == null && (buildingItem.ResourceField == Inventory.R_TYPE.none || buildingItem.ResourceField == Inventory.R_TYPE.all))
        {
            buildingConnectionMenu.gameObject.SetActive(false);
        }
        else if (buildingItem.ConnectionTypes.Length == 0 && (buildingItem.ResourceField == Inventory.R_TYPE.none || buildingItem.ResourceField == Inventory.R_TYPE.all))
        {
            buildingConnectionMenu.gameObject.SetActive(false);
        }
        else
        {
            buildingConnectionMenu.gameObject.SetActive(true);
            buildingConnectionMenu.SetUI(buildingItem, iconStorage);
        }
    }

    public void SetBuildingNumber()
    {
        buildingNumberText.text = buildingItem.AvailableAmount.ToString();
    }
}
