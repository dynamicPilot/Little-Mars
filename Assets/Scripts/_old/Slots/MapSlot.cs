using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSlot : BasicSlot
{
    [SerializeField] private BasicBuilding building;
    [SerializeField] private int buildingImageIndex;

    [Header("Additional UI")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image gridImage;
    [SerializeField] private Animator animator;
    [SerializeField] private Image effectsImage;
    [SerializeField] private Image additionDetailsImage;

    [Header("Map Item")]

    [SerializeField] private bool blocked = false;
    public bool Blocked { get { return blocked; }}

    [SerializeField] private List<Inventory.B_TYPE> availableTypes;
    public List<Inventory.B_TYPE> AvailableTypes { get { return availableTypes; } set { availableTypes = value; } }

    [SerializeField] private List<Inventory.R_TYPE> availableResources;
    public List<Inventory.R_TYPE> AvailableResources { get { return availableResources; } set { availableResources = value; } }

    [SerializeField] private bool isEmpty = true;
    public bool IsEmpty { get { return isEmpty; } }

    public int listIndex;


    [Header("Neighbors")]
    [SerializeField] MapSlot[] neighbors; // = new MapSlot[4] { slot.n_0, slot.n_1, slot.n_2, slot.n_3 };
    public MapSlot[] Neighbors { get { return neighbors; } }

    //[Header("Production")]
    ////[SerializeField] private List<ProductionUnit> currentPoduction;
    //[SerializeField] private List<ProductionUnit> slotPossibleProduction;

    [Header("Sounds")]
    [SerializeField] private int rocketSoundIndex = 1;

    [Header("Scripts")]
    [SerializeField] private SlotEffects slotEffects;
    public SlotEffects SlotEffects { get { return slotEffects; } }

    [SerializeField] private SlotSigns slotSigns;
    [SerializeField] private GameTime gameTime;

    private AudioControl audioControl;

    private void Awake()
    {
        building = null;
        isEmpty = !blocked;
        animator.runtimeAnimatorController = null;
        audioControl = AudioControl.Instance;
        if (blocked)
        {
            slotSigns.AddBlockSign();
        }
    }

    public void SetScriptLinks(IconStorage newIconStorage, GameTime newGameTime)
    {
        //iconStorage = newIconStorage;
        slotSigns.SetScriptLinks(newIconStorage);
        gameTime = newGameTime;
    }

    public void SetAnimatorMode()
    {
        if (building == null || building.Animator == null)
        {
            return;
        }
        else if ((building.Animator.Length < 1 || building.Animator[buildingImageIndex] == null))
        {
            return;
        }

        if (!building.IsOn)
        {
            //Debug.Log("MapSlot: change mode to turn off");
            animator.SetFloat("mode", 0);
        }
        else
        {
            if (building.Production != null)
            {
                foreach(ProductionUnit unit in building.Production)
                {
                    if (gameTime.Period == GameTime.PERIOD.day && unit.DayAmount == 0)
                    {
                        //Debug.Log("MapSlot: change mode due to production");
                        animator.SetFloat("mode", 0.5f);
                        return;
                    }
                    else if (gameTime.Period == GameTime.PERIOD.night && unit.NightAmount == 0)
                    {
                        //Debug.Log("MapSlot: change mode due to production");
                        animator.SetFloat("mode", 0.5f);
                        return;
                    }
                }
            }

            //Debug.Log("MapSlot: change mode to turn on");

            // stack

            animator.SetFloat("mode", 1f);
        }
    }

    public void CosmodromeControl(float rocketMode = -1f)
    {
        if (!building.IsOn)
        {
            //Debug.Log("MapSlot: cosmodrome --> building is not on");
            return;
        }

        // only for port 
        if (animator.runtimeAnimatorController.name.Contains("ort"))
        {
            if (rocketMode == 0 && building.rocketIsOnMars)
            {
                //Debug.Log("MapSlot: cosmodrome --> cosmodrome has rocket, can not land");
                return;
            }
            else if (rocketMode == 1 && !building.rocketIsOnMars)
            {
                //Debug.Log("MapSlot: cosmodrome --> cosmodrome has no rocket, can not start");
                return;
            }

            building.rocketIsOnMars = (rocketMode == 2);
        }

        SetRocketAnimatorModeForCosmodrome(rocketMode);
    }

    void SetRocketAnimatorModeForCosmodrome(float rocketMode = -1f)
    {
        if (building == null || building.Animator == null)
        {
            return;
        }
        else if ((building.Animator.Length < 1 || building.Animator[buildingImageIndex] == null))
        {
            return;
        }
        else if (building.Type != Inventory.B_TYPE.cosmodrome)
        {
            return;
        }
        else if (!building.IsOn)
        {
            return;
        }

        if (animator.runtimeAnimatorController.name.Contains("ort"))
            animator.SetLayerWeight(2, 1f);

        //Debug.Log("MapSlot: cosmodrome --> set new rocketMode " + rocketMode);
        animator.SetFloat("rocketMode", rocketMode);
        
        if (rocketMode == 0)
        {
            StartCoroutine(RocketLanding());
        }
        else if (rocketMode == 1)
        {
            StartCoroutine(RocketStart());
        }
    }

    IEnumerator RocketStart()
    {
        //Debug.Log("MapSlot: START ROCKET!");
        audioControl.PlayActionSound(rocketSoundIndex);
        yield return new WaitForSeconds(2.2f);
        CosmodromeControl(-1f);
    }

    IEnumerator RocketLanding()
    {
        //Debug.Log("MapSlot: START LANDING!");
        audioControl.PlayActionSound(rocketSoundIndex);
        yield return new WaitForSeconds(2.0f);
        CosmodromeControl(2f);
    }

    public void AddBuilding(BasicBuilding newBuilding, int newBuildingImageIndex)
    {
        if (blocked)
        {
            //Debug.Log("MapSlot: slot blocked!");
            isEmpty = false;
            return;
        }
         
        building = newBuilding;
        buildingImageIndex = newBuildingImageIndex;
        isEmpty = false;

        try
        {
            SetImage(building.IconParts[buildingImageIndex], building.rotationIndex);
        }
        catch
        {
            SetImage(building.Icon, building.rotationIndex);
        }

        if (building.Animator == null)
        {
            //Debug.Log("MapSlot: no animator!");
            animator.enabled = false;
            effectsImage.gameObject.SetActive(false);
            additionDetailsImage.gameObject.SetActive(false);
        }
        else if (building.Animator.Length < 1 || building.Animator[buildingImageIndex] == null)
        {
            //Debug.Log("MapSlot: no animator by index!");
            animator.enabled = false;
            effectsImage.gameObject.SetActive(false);
            additionDetailsImage.gameObject.SetActive(false);
        }
        else
        {
            //Debug.Log("MapSlot: set animator!");
            animator.enabled = true;
            effectsImage.gameObject.SetActive(true);

            //cosmodrome options
            additionDetailsImage.gameObject.SetActive(building.Type == Inventory.B_TYPE.cosmodrome);
            additionDetailsImage.enabled = false;

            animator.runtimeAnimatorController = building.Animator[buildingImageIndex];

            animator.SetLayerWeight(1, 1f);
            //animator.SetLayerWeight(2, 0f);

            SetAnimatorMode();
            SetRocketAnimatorModeForCosmodrome();
        }
    }
    public void MakeEmpty()
    {
        if (blocked)
        {
            //Debug.Log("MapSlot: slot blocked!");
            isEmpty = false;
            return;
        }

        RemoveImage();
        animator.enabled = false;
        effectsImage.gameObject.SetActive(false);
        additionDetailsImage.gameObject.SetActive(false);
        additionDetailsImage.enabled = false;
        isEmpty = true;
        building = null;
        buildingImageIndex = 0;
    }

    public BasicBuilding GetBuilding()
    {
        return building;
    }

    public int GetBuildingImageIndex()
    {
        return buildingImageIndex;
    }

    //public void AddToSlotPossibleProduction(ProductionUnit newUnit)
    //{
    //    slotPossibleProduction.Add(newUnit);
    //}

    public void MakeBlocked(bool isBlocked)
    {
        blocked = isBlocked;

        if (blocked)
        {
            slotSigns.AddBlockSign();
        }
    }

    public void AddTypeToAvailableTypes(Inventory.B_TYPE newType, bool showSign = true)
    {
        if (!availableTypes.Contains(newType))
        {
            if (showSign)
            {
                slotSigns.AddBuildingSign(newType);
            }
            availableTypes.Add(newType);
        }

    }

    public void AddTypeToAvailableResources(Inventory.R_TYPE newResource, bool showSign = true)
    {
        if (!availableResources.Contains(newResource))
        {
            availableResources.Add(newResource);
            if (showSign)
            {
                slotSigns.AddResourcesSign(newResource);
            }
        }
    }

    public override void SetImage(Sprite newImage, int rotationIndex = 0)
    {
        // set background
        backgroundImage.gameObject.SetActive(newImage != null);
        
        base.SetImage(newImage, rotationIndex);

        // turn off grid
        gridImage.enabled = newImage == null;
        slotSigns.gameObject.SetActive(newImage == null);
    }

    public override void RemoveImage()
    {
        base.RemoveImage();
        backgroundImage.gameObject.SetActive(false);

        // turn on grid
        gridImage.enabled = true;
        slotSigns.gameObject.SetActive(true);
    }
}
