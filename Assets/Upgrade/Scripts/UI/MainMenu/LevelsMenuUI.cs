using LittleMars.MainMenus;
using LittleMars.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class LevelsMenuUI : MenuUI
    {
        [SerializeField] LevelsPageUI _pageUI;

        [Header("Buttons")]
        [SerializeField] Button _openButton;
        [SerializeField] Button _toNextButton;
        [SerializeField] Button _toPrevButton;
        [SerializeField] Button _backButton;

        LevelsMenu _menu;
        ISlotOnClick _onClick;
        LevelSettings[] _levels = null;
        List<bool> _isDoneLevels = null;

        int _pageIndex = 0;
        int _pagesCount;
        int _slotsPerPage = 0;

        [Inject]
        public void Constructor(LevelsMenu menu, ISlotOnClick onClick)
        {
            _menu = menu;
            _onClick = onClick;
            Init();
        }

        void Init()
        {
            SetListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
            _levels = null;
            _isDoneLevels = null;
        }

        void SetListeners()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);
            _toNextButton.onClick.AddListener(ToNextPage);
            _toPrevButton.onClick.AddListener(ToPrevPage);
            _backButton.onClick.AddListener(Close);
        }

        void RemoveListeners()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
            _toNextButton.onClick.RemoveAllListeners();
            _toPrevButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveListener(Close);
        }

        void OnOpenButtonClick()
        {
            if (_levels == null) SetLevels();
            UpdateIsDoneLevels();

            _pageIndex = 0;            
            Open();
        }

        protected override void Open()
        {
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
            _pageIndex--;
            SetNextPage();
        }

        void ToNextPage()
        {
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
