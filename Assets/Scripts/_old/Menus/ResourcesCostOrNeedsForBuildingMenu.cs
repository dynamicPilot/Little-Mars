using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesCostOrNeedsForBuildingMenu : MenuUI
{
    [SerializeField] private List<ResourceSlot> slots;
    [SerializeField] private State.MODE mode;
    [SerializeField] private string prefix;

    private IconStorage iconStorage;

    //private BuildingItem item;
    private List<ProductionUnit> units;

    public void SetUI(BuildingItem newItem, IconStorage newIconStorage)
    {
        units = new List<ProductionUnit>();
        slots = new List<ResourceSlot>();

        foreach (ResourceSlot slot in SlotParent.GetComponentsInChildren<ResourceSlot>())
        {
            slots.Add(slot);
        }

        iconStorage = newIconStorage;

        if (mode == State.MODE.resources)
        {
            units = newItem.BuildingNeeds;
        }
        else if (mode == State.MODE.production)
        {
            units = newItem.Production;
        }
        else if (mode == State.MODE.hourly_needs)
        {
            units = newItem.HourlyNeeds;
        }
        UpdateUI();
    }


    public override void UpdateUI()
    {
        int index = 0;
        foreach (ProductionUnit unit in units)
        {
            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].SetScriptLinks(iconStorage);
                slots[index].SetSlot(unit.ResourseType, unit.DayAmount, prefix);
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<ResourceSlot>());
                slots[index].SetScriptLinks(iconStorage);
                slots[index].SetSlot(unit.ResourseType, unit.DayAmount, prefix);
            }
            index++;
        }

        if (index < slots.Count)
        {
            for (int i = index; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}
