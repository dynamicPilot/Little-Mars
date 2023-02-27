using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Connections.View;
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
        readonly ConnectionIndicators _indicators;

        public SlotEmptyState(ViewSlotView view, ViewSlotUI viewUI, ViewSlotFacade facade,
            ConnectionIndicators indicators)
        {
            _view = view;
            _viewUI = viewUI;
            _facade = facade;
            _indicators = indicators;
        }

        public void SetView()
        {
            //Debug.Log("Set resources images");
            _view.EmptyState();
            _viewUI.ShowSigns();

            _indicators.HideIndicators();
            _indicators.ChangeCanShowState(false);
        }

        public void Dispose()
        {
            //Debug.Log("Resolve resources");
        }

        public void OnDrop(BuildingObject buildingObject)
        {
            _facade.TryAddBuilding(buildingObject);
        }

        public class Factory : PlaceholderFactory<SlotEmptyState>
        {

        }
    }
}
