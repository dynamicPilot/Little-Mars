using LittleMars.MainMenus;
using LittleMars.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class LevelsMenuUI : MenuUI
    {
        [SerializeField] LevelInfoSlotUI[] _slots;
        [SerializeField] int _slotPerPage = 4;

        LevelsMenu _menu;
        LevelSettings[] _levels = null;
        int _pageIndex = 0;
        int _pagesCount;

        [Inject]
        public void Constructor(LevelsMenu menu)
        {
            _menu = menu;

            Init();
        }

        void Init()
        {
            _levels = _menu.GetLevels();
            _pagesCount = (int) Math.Ceiling( _levels.Length * 1f / _slotPerPage);
            Debug.Log("Current pages count " + _pagesCount);
            SetPage();
        }

        private void OnDestroy()
        {
            _levels = null;
        }

        void SetPage()
        {
            var startIndex = _pageIndex * _slotPerPage;
            var lengthRemain = _levels.Length - startIndex;
            var takeCount = Math.Min(_slotPerPage, lengthRemain);

            SetSlots(_levels.Skip(startIndex).Take(takeCount).ToList());
        }

        void PrevPage()
        {

        }

        void NextPage()
        {

        }

        void SetSlots(List<LevelSettings> levels)
        {
            int index = 0;
            for (int i = 0; i < levels.Count; i++)
            {
                if (i >= _slots.Length) return;

                _slots[i].SetLevelInfo(levels[i].Info.LevelInfo, false);
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
