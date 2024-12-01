using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevel : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private LevelInfo[] levels;
    public LevelInfo[] Levels { get { return levels; } }

    [Header("Scripts")]
    [SerializeField] private ChooseLevelUI chooseLevelUI;

    public void OpenUIPanel()
    {
        chooseLevelUI.MenuPanel.SetActive(true);
    }

    public void CloseUIPanel()
    {
        chooseLevelUI.MenuPanel.SetActive(false);
    }


}
