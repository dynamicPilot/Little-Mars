using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Slots.UI;
using LittleMars.Slots.Views;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Slots.States
{
    public class SlotEmptyState : ISlotState
    {
        readonly ViewSlotView _view;
        readonly ViewSlotUI _viewUI;
        readonly ViewSlotFacade _facade;
        public SlotEmptyState(ViewSlotView view, ViewSlotUI viewUI, ViewSlotFacade facade)
        {
            _view = view;
            _viewUI = viewUI;
            _facade = facade;
        }

        public void SetView()
        {
            //Debug.Log("Set resources images");
            _view.EmptyState();
            _viewUI.ShowSigns();
        }

        public void Dispose()
        {
            //Debug.Log("Resolve resources");
        }

        public void OnDrop(BuildingObject buildingObject)
        {
            Debug.Log("Drop " + buildingObject.name);
            _facade.TryAddBuilding(buildingObject);
        }

        public class Factory : PlaceholderFactory<SlotEmptyState>
        {

        }
    }
}
