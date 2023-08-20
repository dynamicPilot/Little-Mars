using LittleMars.GoalInfoMenu;
using LittleMars.UI.SignSlot;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalInfoMenu
{
    public class SignInfoMenuUI : MonoBehaviour
    {
        [SerializeField] List<SignSlotUI> _signSlots;
        [SerializeField] SignSlotUI[] _standardSlots;

        SignInfoMenu _infoMenu;

        [Inject]
        public void Constructor(SignInfoMenu infoMenu)
        {
            _infoMenu = infoMenu;
        }

        public void SetSlots()
        {
            // standard slots
            var standartElements = _infoMenu.GetStandardElements(out var standardStates);
            SetStandardSlot(standardStates, standartElements);

            // element slots
            var elements = _infoMenu.GetSignInfoElements(out var hasBuildings);
            if (hasBuildings) SetElementSlots(elements);
            else HideSlots(0);
        }

        void SetElementSlots(SignInfoElement[] elements)
        {
            int i = 0;
            for (; i < elements.Length; i++)
            {
                if (i >= _signSlots.Count) return; // temp -> need create new slots
                _signSlots[i].gameObject.SetActive(true);

                _signSlots[i].SetSlot(elements[i].Icon);
                _signSlots[i].SetSize((int)elements[i].Size);
                _signSlots[i].SetText(elements[i].Text);
            }

            HideSlots(i);
        }

        void HideSlots(int startIndex)
        {
            for (int i = startIndex; i< _signSlots.Count; i++) 
                _signSlots[i].gameObject.SetActive(false);
        }

        void SetStandardSlot(bool[] states, SignInfoElement[] elements)
        {
            Debug.Log("SignInfoMenuUI: set standard slots. Slots count "+ elements.Length);
            for (int i = 0; i < elements.Length; i++)
            {
                _standardSlots[i].gameObject.SetActive(states[i]);

                if (states[i])
                {
                    _standardSlots[i].SetSlot(elements[i].Icon);
                    _standardSlots[i].HideSize();
                    _standardSlots[i].SetText(elements[i].Text);
                }
            }
        }
    }
}
