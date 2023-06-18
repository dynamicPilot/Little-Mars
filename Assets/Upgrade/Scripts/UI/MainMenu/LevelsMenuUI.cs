using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.LevelMenus;
using LittleMars.MainMenus;
using LittleMars.Settings;
using LittleMars.UI.LevelMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class LevelsMenuUI : MenuUIWithControls
    {
        [SerializeField] LevelsPageUI _pageUI;

        [Header("Buttons")]
        [SerializeField] Button _toNextButton;
        [SerializeField] Button _toPrevButton;

        LevelsMenu _menu;
        ISlotOnClick _onClick;
        LevelSettings[] _levels = null;
        List<bool> _isDoneLevels = null;

        int _pageIndex = 0;
        int _pagesCount;
        int _slotsPerPage = 0;

        [Inject]
        public void Constructor(LevelsMenu menu, GameMenu gameMenu, ISlotOnClick onClick, 
            SoundsForGameMenuUI sounds)
        {
            _menu = menu;
            _gameMenu = gameMenu;
            _sounds = sounds;
            _onClick = onClick;
            Init();
        }

        void Init()
        {
            SetButtons();
        }

        private void OnDestroy()
        {
            RemoveListeners();
            _levels = null;
            _isDoneLevels = null;
        }

        protected override void SetListeners()
        {
            _toNextButton.onClick.AddListener(ToNextPage);
            _toPrevButton.onClick.AddListener(ToPrevPage);
            base.SetListeners();
        }

        protected override void RemoveListeners()
        {
            _toNextButton.onClick.RemoveAllListeners();
            _toPrevButton.onClick.RemoveAllListeners();
            base.RemoveListeners();
        }

        public override void OnOpenMenu()
        {
            if (_levels == null) SetLevels();
            UpdateIsDoneLevels();

            _pageIndex = 0;
            base.OnOpenMenu();
        }

        protected override void Open()
        {
            //_audioSystem.PlayUISound(UISoundType.clickFirst);
            SetPage();
            base.Open();
        }

        void SetLevels()
        {
            _levels = _menu.GetLevels();
            _slotsPerPage = _pageUI.GetSlotPerPage();
            _pageUI.SetSlotOnClick(_onClick);
            _pagesCount = (int)Math.Ceiling(_levels.Length * 1f / _slotsPerPage);
        }

        void UpdateIsDoneLevels()
        {
            _isDoneLevels = _menu.GetIsDoneLevels();
        }

        void SetPage()
        {
            var startIndex = _pageIndex * _slotsPerPage;
            var lengthRemain = _levels.Length - startIndex;
            var takeCount = Math.Min(_slotsPerPage, lengthRemain);
            var isDoneLevels = (_isDoneLevels == null) ? null :
                _isDoneLevels.Skip(startIndex).Take(takeCount).ToList();

            _pageUI.SetSlots(_levels.Skip(startIndex).Take(takeCount).ToList(), isDoneLevels);
        }

        void SetNextPage()
        {
            CheckPageIndex();
            SetPage();
        }

        void ToPrevPage()
        {
            _sounds.PlaySoundForCommandType(CommandType.turnPage);
            _pageIndex--;
            SetNextPage();
        }

        void ToNextPage()
        {
            _sounds.PlaySoundForCommandType(CommandType.turnPage);
            _pageIndex++;
            SetNextPage();
        }

        void CheckPageIndex()
        {
            if (_pageIndex < 0) _pageIndex = _pagesCount - 1;
            else if (_pageIndex >= _pagesCount) _pageIndex = 0;
        }
    }
}
