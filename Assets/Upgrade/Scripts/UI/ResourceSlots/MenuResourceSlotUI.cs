using System;
using UnityEngine;

namespace LittleMars.UI.ResourceSlots
{
    public class MenuResourceSlotUI : ResourceSlotUI
    {
        [SerializeField] GameObject _indication;

        public void UpdateSlotAndIndication(float value, bool needIndication)
        {
            _indication.SetActive(needIndication);
            UpdateSlot(value);
        }
    }
}
