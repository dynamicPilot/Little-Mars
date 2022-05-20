using System.Collections.Generic;
using UnityEngine;

public class BuildingsMenuUI : MenuUI
{
    [Header("UI Elements")]
    //[SerializeField] private Transform slotParent;
    //[SerializeField] private GameObject slotPrefab;
    [SerializeField] private List<MenuSlot> slots;

    [Header("Scripts")]
    [SerializeField] private BuildingsControl buildingsControl;
    [SerializeField] private IconStorage iconStorage;
    [SerializeField] private GameMaster gameMaster;

    public void CreateBuldingMenuForLevel()
    {
        slots = new List<MenuSlot>();
        slots.Clear();

        // add existing
        foreach (MenuSlot slot in SlotParent.GetComponentsInChildren<MenuSlot>())
        {
            slots.Add(slot);
        }

        UpdateUI();
        buildingsControl.OnBuildingsChange += UpdateUI;
    }


    public override void UpdateUI()
    {
        //Debug.Log("BuildingMenuUI: update buildings");
        int index = 0;
        foreach (BuildingItem item in buildingsControl.BuildingSamples)
        {
            if (!item.IsAvailable)
            {
                continue;
            }

            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].AddItem(item, iconStorage);
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<MenuSlot>());
                slots[index].SetObjectLinks(gameMaster);
                slots[index].AddItem(item, iconStorage);
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
