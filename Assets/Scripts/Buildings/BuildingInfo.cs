using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour
{
    [SerializeField] private ResourcesCostOrNeedsForBuildingMenu productionPart;
    [SerializeField] private ResourcesCostOrNeedsForBuildingMenu needsPart;
    [SerializeField] private Text nameText;
    
    private IconStorage iconStorage;

    public void SetInfo(BuildingItem item, IconStorage newIconStorage, GameMaster.LANG lang = GameMaster.LANG.eng)
    {
        iconStorage = newIconStorage;

        // Set name
        try
        {
            string text = item.ItemNameByLangs.GetDescriptionByLang(lang);

            if (text == "" || text == null)
                text = item.ItemName;

            //Debug.Log("BuildingInfo: have name for lang, it is " + text);
            nameText.text = text;
        }
        catch
        {
            //nameText.gameObject.SetActive(true);
            //Debug.Log("BuildingInfo: no name for lang");
            nameText.text = item.ItemName;
        }

        // Set production
        SetProductionUnits(item, item.Production, productionPart);

        // Set needs
        SetProductionUnits(item, item.HourlyNeeds, needsPart);
    }

    void SetProductionUnits(BuildingItem item, List<ProductionUnit> units, ResourcesCostOrNeedsForBuildingMenu menu)
    {
        if (units == null)
        {
            menu.gameObject.SetActive(false);
        }
        else if (units.Count == 0)
        {
            menu.gameObject.SetActive(false);
        }
        else
        {
            menu.gameObject.SetActive(true);
            menu.SetUI(item, iconStorage);
        }
    }
}
