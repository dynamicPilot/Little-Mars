using LittleMars.Common.Levels;
using LittleMars.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI.MainMenu
{
    public class LevelsPageUI : MonoBehaviour
    {
        [SerializeField] LevelInfoSlotUI[] _slots;

        ISlotOnClick _onSlotClick;
        public int GetSlotPerPage() => _slots.Length;
        public void SetSlotOnClick(ISlotOnClick onSlotClick) => _onSlotClick = onSlotClick;
        public void SetSlots(List<LevelSettings> levels, List<bool> isDoneLevels)
        {
            bool hasIsDoneList = (isDoneLevels != null && isDoneLevels.Count > 0);
            int index = 0;
            for (int i = 0; i < levels.Count; i++)
            {
                if (i >= _slots.Length) return;

                if (hasIsDoneList) 
                    _slots[i].SetLevelInfo(levels[i].Info.LevelInfo, _onSlotClick, isDoneLevels[i]);
                else 
                    _slots[i].SetLevelInfo(levels[i].Info.LevelInfo, _onSlotClick);

                index++;
            }

            if (index < _slots.Length)
            {
                for (int i = index; i < _slots.Length; i++)
                    _slots[i].SetEmpty();
            }
        }
    }
}
