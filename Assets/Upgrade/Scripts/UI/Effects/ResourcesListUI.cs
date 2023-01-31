using LittleMars.Common;
using LittleMars.UI.BuildingsSlots;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Effects
{

    public class ResourcesListUI : IInitializable
    {
        readonly SlotUIFactory<ResourceSlotUI> _factory;
        readonly BuildingSlotGameUI _gameUI;
        readonly BuildingObject _building;

        public ResourcesListUI(SlotUIFactory<ResourceSlotUI> factory, 
            BuildingSlotGameUI gameUI, BuildingObject building)
        {
            _factory = factory;
            _gameUI = gameUI;
            _building = building;
        }

        public void Initialize()
        {
            CreateSlots();
        }

        private void CreateSlots()
        {
            _gameUI.BuildingCostSlotParent
                .gameObject.SetActive(_building.Construction.ResourcesForBuilding.Length > 0);


            for(int i = 0; i < _building.Construction.ResourcesForBuilding.Length; i++)
            {
                var unit = _building.Construction.ResourcesForBuilding[i];
                var slot =_factory.CreateSlot((int)unit.Type, _gameUI.BuildingCostSlotParent.GetComponent<RectTransform>(), i+1);
                slot.UpdateSlot(unit.Amount);
            }
        }
    }
}
