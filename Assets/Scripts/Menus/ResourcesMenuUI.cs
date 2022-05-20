using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMenuUI : MenuUI
{
    [SerializeField] private List<ResourceSlot> slots;
    [SerializeField] private State.MODE mode;

    [Header("Scripts")]
    [SerializeField] private State state;
    [SerializeField] private IconStorage iconStorage;

    private void Awake()
    {
        foreach(ResourceSlot slot in SlotParent.GetComponentsInChildren<ResourceSlot>())
        {
            slot.SetScriptLinks(iconStorage);
            slots.Add(slot);
        }

        if (mode == State.MODE.resources)
        {
            state.OnResourcesChange += UpdateUI;
        }
        else if (mode == State.MODE.production)
        {
            state.OnProductionChange += UpdateUI;
        }
        else if (mode == State.MODE.hourly_needs)
        {
            state.OnHourlyNeedsChange += UpdateUI;
        }
    }

    public override void UpdateUI()
    {
        //Debug.Log("ResourcesMenuUI: update UI");
        Dictionary<Inventory.R_TYPE, ProductionUnit> unitsByType = new Dictionary<Inventory.R_TYPE, ProductionUnit>();
        
        if (mode == State.MODE.resources)
        {
            unitsByType = state.Resources;
        }
        else if (mode == State.MODE.production)
        {
            unitsByType = state.Production;
        }
        else if (mode == State.MODE.hourly_needs)
        {
            unitsByType = state.HourlyNeeds;
        }

        int index = 0;
        foreach (Inventory.R_TYPE type in unitsByType.Keys)
        {
            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].SetSlot(unitsByType[type].ResourseType, unitsByType[type].DayAmount);
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<ResourceSlot>());
                slots[index].SetScriptLinks(iconStorage);
                slots[index].SetSlot(unitsByType[type].ResourseType, unitsByType[type].DayAmount);
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
