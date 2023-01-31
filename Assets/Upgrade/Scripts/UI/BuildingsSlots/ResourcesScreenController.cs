using LittleMars.Common;
using LittleMars.UI.Effects;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.BuildingsSlots
{




    public class ResourcesScreenController : IInitializable
    {
        readonly ResourceListFactory _listFactory;
        readonly BuildingSlotGameUI _gameUI;
        readonly BuildingObject _building;

        List<ListScreenEffectUI> _screens;
        List<bool> _slotIsReady;
        ScreenType _screenType;

        public ResourcesScreenController(ResourceListFactory listFactory, 
            BuildingSlotGameUI gameUI, BuildingObject building)
        {
            _listFactory = listFactory;
            _gameUI = gameUI;
            _building = building;

            _screens = new List<ListScreenEffectUI>();
            _slotIsReady = new List<bool>();

            _screenType = ScreenType.needs;
        }

        public void Initialize()
        {
            PrepareLists();
            TranslateToScreen(ScreenType.buildingCost);
        }

        public void ToNextScreen()
        {
            var index = ((int)_screenType + 1) % 3;
            TranslateToScreen((ScreenType) index);
        }

        private void TranslateToScreen(ScreenType type)
        {
            if (_screenType == type) return;

            if (!_slotIsReady[(int)type])
            {
                CreateSlotsForScreen(type);
            }

            if (!_slotIsReady[(int)type])
            {
                _screenType = type;
                ToNextScreen();
            }
            else ChangeScreen(type);
        }

        private void ChangeScreen(ScreenType type)
        {
            CloseScreen();
            _screenType = type;
            OpenScreen();
        }

        private void OpenScreen()
        {
            _screens[(int)_screenType].gameObject.SetActive(true);
        }

        private void CloseScreen()
        {
            _screens[(int)_screenType].gameObject.SetActive(false);
        }

        private void CreateSlotsForScreen(ScreenType type)
        {
            if (type == ScreenType.buildingCost)
            {
                _slotIsReady[(int)type] =
                    CreateSlots(_screens[(int)type], _building.Construction.ResourcesForBuilding, "");
            }                
            else if (type == ScreenType.production)
            {
                _slotIsReady[(int)type] =
                    CreateSlots(_screens[(int)type], _building.Operation.DayProduction, "+");
            }
            else if (type == ScreenType.needs)
            {
                _slotIsReady[(int)type] =
                    CreateSlots(_screens[(int)type], _building.Operation.Needs, "-");
            }
            else
            {
                return;
            }
        }

        private bool CreateSlots(ListScreenEffectUI screenUI, ResourceUnit<float>[] resources, string prefix)
        {
            var rect = screenUI.GetComponent<RectTransform>();
            if (rect == null) return false;

            var isEmpty = (resources == null || resources.Length == 0);
            screenUI.IsEmptyState(isEmpty);

            if (isEmpty) return true;

            _listFactory.CreateSlots(rect, resources, prefix);
            return true;
        }

        private void PrepareLists()
        {
            _screens.Add(_gameUI.BuildingCostSlotParent);
            _screens.Add(_gameUI.ProductionCostSlotParent);
            _screens.Add(_gameUI.NeedsCostSlotParent);

            for (int i = 0; i < _screens.Count; i++)
                _slotIsReady.Add(false);
        }
    }
}
