using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDescriptionMenuUI : MenuUI
{
    [SerializeField] private List<LimitDescriptionSlot> slots;
    private BasicLevelLimits limits;

    public void SetUI(BasicLevelLimits newLimits)
    {
        limits = newLimits;

        foreach (LimitDescriptionSlot slot in SlotParent.GetComponentsInChildren<LimitDescriptionSlot>())
        {
            slots.Add(slot);
        }

        UpdateUI();
    }

    public override void UpdateUI()
    {
        int index = 0;
        foreach (string description in limits.AllDescriptions)
        {
            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].SetText(description);
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<LimitDescriptionSlot>());
                slots[index].SetText(description);
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
