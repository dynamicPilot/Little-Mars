using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleResourcesMenuUI : MenuUI
{
    [SerializeField] private List<DoubleResourceSlot> slots;

    [Header("Options")]
    [SerializeField] private bool hideOnAwake;

    [Header("Scripts")]
    [SerializeField] private State state;
    [SerializeField] private IconStorage iconStorage;
    [SerializeField] private GameTime gameTime;

    private void Awake()
    {
        foreach (DoubleResourceSlot slot in SlotParent.GetComponentsInChildren<DoubleResourceSlot>())
        {
            slot.SetScriptLinks(iconStorage);
            slots.Add(slot);
        }

        state.OnProductionChange += UpdateUI;
        state.OnHourlyNeedsChange += UpdateUI;

        gameTime.OnPeriodToNewDay += UpdateUI;
        gameTime.OnPeriodToNewNight += UpdateUI;

        if (hideOnAwake)
        {
            CloseMenuPanel();
        }
    }


    public override void UpdateUI()
    {
        //Debug.Log("ResourcesMenuUI: update UI");
        //Dictionary<Inventory.R_TYPE, ProductionUnit> unitsByType = new Dictionary<Inventory.R_TYPE, ProductionUnit>();
        List<Inventory.R_TYPE> types = new List<Inventory.R_TYPE>();

        for (int i = 0; i < (int) Inventory.R_TYPE.all; i++)
        {
            types.Add((Inventory.R_TYPE) i);
        }

        float productionAmount = 0;
        float needsAmount = 0;

        int index = 0;
        foreach (Inventory.R_TYPE type in types)
        {
            productionAmount = 0;
            needsAmount = 0;

            if (state.Production.ContainsKey(type))
            {
                if (gameTime.Period == GameTime.PERIOD.day)
                {
                    productionAmount = state.Production[type].DayAmount;
                }
                else if (gameTime.Period == GameTime.PERIOD.night)
                {
                    productionAmount = state.Production[type].NightAmount;
                }
                
            }

            if (state.HourlyNeeds.ContainsKey(type))
            {
                if (gameTime.Period == GameTime.PERIOD.day)
                {
                    needsAmount = state.HourlyNeeds[type].DayAmount;
                }
                else if (gameTime.Period == GameTime.PERIOD.night)
                {
                    needsAmount = state.HourlyNeeds[type].NightAmount;
                }

            }


            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].SetDoubleSlot(type, productionAmount, needsAmount);
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<DoubleResourceSlot>());
                slots[index].SetScriptLinks(iconStorage);
                slots[index].SetDoubleSlot(type, productionAmount, needsAmount);
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
