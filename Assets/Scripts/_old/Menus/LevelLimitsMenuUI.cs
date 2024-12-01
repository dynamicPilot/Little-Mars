using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLimitsMenuUI : MenuUI
{
    [SerializeField] private List<LevelLimitToggle> slots;
    [SerializeField] private Text levelNumberText;

    [Header("Options")]
    [SerializeField] private UIFullLabel prefixes;
    [SerializeField] private bool hideOnAwake;

    [Header("Scripts")]
    [SerializeField] private UILanguageViaControl uILanguageViaControl;

    private void Awake()
    {
        foreach (LevelLimitToggle slot in SlotParent.GetComponentsInChildren<LevelLimitToggle>())
        {
            slots.Add(slot);
        }

        if (hideOnAwake)
        {
            CloseMenuPanel();
        }
    }

    public void SetLevelLimitAsReached(int index, bool value = true)
    {
        if (index < slots.Count && index > -1)
        {
            slots[index].SetToggleIsOnValue(value);
        }
    }

    public void UpdateProductInSum(int index, float number, float goalNumber, bool needRemoveOld = false)
    {
        if (index < slots.Count && index > -1)
        {
            string scoreString = " (" + number.ToString("f0") + "/" + goalNumber.ToString("f0") + ")";
            slots[index].AddScoreToText(scoreString, needRemoveOld);
        }
    }

    public void SetLevel(int newLevelNumber, List<string> descriptions, GameMaster.LANG language)
    {
        levelNumberText.text = prefixes.GetLabelByLang(language).Label + " " + newLevelNumber.ToString();

        int index = 0;
        foreach(string description in descriptions)
        {
            if (description == null)
            {
                continue;
            }
            else if (description == "")
            {
                continue;
            }

            if (index < slots.Count)
            {
                slots[index].gameObject.SetActive(true);
                slots[index].SetToggle(description);
            }
            else
            {
                slots.Add(Instantiate(SlotPrefab, SlotParent).GetComponent<LevelLimitToggle>());
                slots[index].SetToggle(description);

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

        if (uILanguageViaControl != null)
        {
            uILanguageViaControl.UpdateText();
        }
    }
}
