using UnityEngine;
using UnityEngine.UI;

public class ResourceSlot : MonoBehaviour
{
    [SerializeField] private Image sign;
    [SerializeField] private Text text;
    [SerializeField] private Image background;

    [Header("Colors")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color signalColor;

    [Header("Values")]
    [SerializeField] private int valueForEnergyMoneyAndSupply = 10;
    [SerializeField] private int valueForFood = 4;
    [SerializeField] private int valueForMetalsAndMachines = 2;

    private Inventory.R_TYPE type;
    private IconStorage iconStorage;

    public void SetScriptLinks(IconStorage newIconStorage)
    {
        iconStorage = newIconStorage;
    }

    public void SetSlot(Inventory.R_TYPE newType, float newAmount, string prefix = "")
    {
        type = newType;
        text.text = prefix + newAmount.ToString("f0");

        if (iconStorage == null)
        {
            sign.gameObject.SetActive(false);
        }
        else
        {
            sign.gameObject.SetActive(true);
            sign.sprite = iconStorage.GetResourceIcon(type);
        }

        if (background != null)
        {
            background.color = defaultColor;
            if (newType == Inventory.R_TYPE.energy || newType == Inventory.R_TYPE.supply_units || newType == Inventory.R_TYPE.money)
            {
                if (newAmount <= valueForEnergyMoneyAndSupply)
                {
                    background.color = signalColor;
                }
            }
            else if (newType == Inventory.R_TYPE.food)
            {
                if (newAmount <= valueForFood)
                {
                    background.color = signalColor;
                }
            }
            else if (newType == Inventory.R_TYPE.metalls || newType == Inventory.R_TYPE.machines)
            {
                if (newAmount <= valueForMetalsAndMachines)
                {
                    background.color = signalColor;
                }
            }
            
        }
        
    }
}
