using LittleMars.UI.GoalDisplays;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI.LevelMenus.Addons
{
    public class MenuStrategies : MonoBehaviour
    {
        [Header("Goals Display Slots")]
        [SerializeField] GoalDisplayUI[] _displayUIs;


        void SetGoalDisplays(IGoalDisplayStrategy[] strategies)
        {
            int index = 0;
            for (int i = 0; i < strategies.Length; i++)
            {
                if (i >= _displayUIs.Length) return;

                _displayUIs[i].gameObject.SetActive(true);
                _displayUIs[i].SetSlot(strategies[i], i);
                //_displayUIs[i].GetComponent<GoalTooltipControllerTextUI>().SetIndex(i);
                index++;
            }

            if (index < _displayUIs.Length)
            {
                for (int i = index; i < _displayUIs.Length; i++)
                    _displayUIs[i].gameObject.SetActive(false);
            }
        }
    }
}
