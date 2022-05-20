using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    public GameObject MenuPanel { get { return menuPanel; } }

    [Header("UI Elements")]
    [SerializeField] private Transform slotParent;
    public Transform SlotParent { get { return slotParent; } }
    [SerializeField] private GameObject slotPrefab;
    public GameObject SlotPrefab { get { return slotPrefab; } }

    [SerializeField] private bool needUpdateOnOpen = true;

    public virtual void UpdateUI()
    {

    }

    public virtual void ChangeMenuState()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);

        if (menuPanel.activeSelf)
        {
            UpdateUI();
        }
    }

    public virtual void OpenMenuPanel()
    {
        menuPanel.SetActive(true);

        if (needUpdateOnOpen)
            UpdateUI();
    }

    public virtual void CloseMenuPanel()
    {
        menuPanel.SetActive(false);
    }

}
