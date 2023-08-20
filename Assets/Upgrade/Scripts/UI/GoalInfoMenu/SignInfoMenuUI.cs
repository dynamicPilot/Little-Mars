using LittleMars.UI.SignSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LittleMars.UI.GoalInfoMenu
{
    public class SignInfoMenuUI : MonoBehaviour
    {
        [SerializeField] List<SignSlotUI> _signSlots;
        [SerializeField] SignSlotUI _timerSlot;
        [SerializeField] SignSlotUI _storageSlot;
        [SerializeField] SignSlotUI _productonSlot;

        public void SetSlots()
        {

        }
    }
}
