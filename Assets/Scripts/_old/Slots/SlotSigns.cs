using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSigns : MonoBehaviour
{
    [SerializeField] private Image[] signs;

    private IconStorage iconStorage;

    private void Awake()
    {
        foreach (Image sign in signs)
        {
            sign.gameObject.SetActive(false);
        }
    }

    public void SetScriptLinks(IconStorage newIconStorage)
    {
        iconStorage = newIconStorage;
    }

    public void AddResourcesSign(Inventory.R_TYPE type)
    {
        for (int i = 0; i < signs.Length; i++)
        {
            if (!signs[i].gameObject.activeSelf)
            {
                signs[i].gameObject.SetActive(true);
                signs[i].sprite = iconStorage.GetResourceIcon(type);
                return;
            }
        }
    }

    public void AddBuildingSign(Inventory.B_TYPE type)
    {
        for (int i = 0; i < signs.Length; i++)
        {
            if (!signs[i].gameObject.activeSelf)
            {
                signs[i].gameObject.SetActive(true);
                signs[i].sprite = iconStorage.GetBuildingIcon(type);
                return;
            }
        }
    }

    public void AddBlockSign()
    {
        for (int i = 0; i < signs.Length; i++)
        {
            if (!signs[i].gameObject.activeSelf)
            {
                signs[i].gameObject.SetActive(true);
                signs[i].sprite = iconStorage.GetBlockedSlotIcon();
                return;
            }
        }
    }

    public void HideAllSigns()
    {
        foreach (Image sign in signs)
        {
            sign.gameObject.SetActive(false);
        }
    }
}
