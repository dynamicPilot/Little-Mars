using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingConnectionMenu : MenuUI
{
    [SerializeField] private List<BasicSlot> slots;
    [SerializeField] private IconStorage iconStorage;

    private Inventory.B_TYPE[] connections;
    private Inventory.R_TYPE resourceField = Inventory.R_TYPE.none;

    public void SetUI(BuildingItem newItem, IconStorage newIconStorage)
    {
        slots = new List<BasicSlot>();
        connections = newItem.ConnectionTypes;
        resourceField = newItem.ResourceField;

        foreach (BasicSlot slot in SlotParent.GetComponentsInChildren<BasicSlot>())
        {
            slots.Add(slot);
        }

        iconStorage = newIconStorage;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        if (iconStorage == null)
        {
            return;
        }

        int index = 0;

        if (connections != null)
        {
            if (connections.Length > 0)
            {
                foreach (Inventory.B_TYPE unit in connections)
                {
                    if (iconStorage.GetBuildingIcon(unit) == null)
                    {
                        continue;
                    }

                    if (index < slots.Count)
                    {
                        slots[index].gameObject.SetActive(true);
                        slots[index].SetImage(iconStorage.GetBuildingIcon(unit));
                    }
                    else
                    {
                        slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<BasicSlot>());
                        slots[index].SetImage(iconStorage.GetBuildingIcon(unit));
                    }
                    index++;
                }
            } 
        }
        

        if (resourceField != Inventory.R_TYPE.none && resourceField != Inventory.R_TYPE.all)
        {
            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].SetImage(iconStorage.GetResourceIcon(resourceField));
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<BasicSlot>());
                slots[index].SetImage(iconStorage.GetResourceIcon(resourceField));
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
