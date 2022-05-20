using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSlotsPage : MenuUI
{
    [SerializeField] private List<LevelSlot> slots;

    private List<LevelInfo> infos;
    private List<LevelInfo> completedLevels;

    public void SetSlotsPage(List<LevelInfo> newInfos, List<LevelInfo> newCompletedLevels)
    {
        infos = newInfos;
        completedLevels = newCompletedLevels;

        if (completedLevels == null)
        {
            completedLevels = new List<LevelInfo>();
        }

        if (slots == null)
        {
            slots = new List<LevelSlot>();
        }

        UpdateUI();
    }
    public override void UpdateUI()
    {
        int index = 0;
        foreach (LevelInfo info in infos)
        {
            bool isCompleted = completedLevels.Contains(info);
            bool isAvailable = CheckIfIsAvailable(info);

            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].SetLevelInfo(info, isCompleted, isAvailable, index);
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<LevelSlot>());
                slots[index].SetLevelInfo(info, isCompleted, isAvailable, index);
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

    bool CheckIfIsAvailable(LevelInfo levelToCheck)
    {
        if (levelToCheck.LevelsToBecomeAvailable != null)
        {
            foreach (LevelInfo levelInfo in levelToCheck.LevelsToBecomeAvailable)
            {
                if (!completedLevels.Contains(levelInfo))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
