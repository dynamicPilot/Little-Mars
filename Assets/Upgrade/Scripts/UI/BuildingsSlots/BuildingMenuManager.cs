﻿using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using LittleMars.Common.Levels;

namespace LittleMars.UI.BuildingSlots
{
    /// <summary>
    /// This is a script to create and control Builing slots in menu.
    /// </summary>
    public class BuildingMenuManager : IInitializable, IDisposable
    {
        // slots list
        Dictionary<BuildingType, Dictionary<Size, int>> _amounts = null;
        Dictionary<BuildingType, Dictionary<Size, IBuildingSlotFacade>> _slots = null;

        readonly LevelConditions _settings;
        readonly BuildingSlotFactory _factory;
        readonly SignalBus _signalBus;

        public BuildingMenuManager(LevelConditions settings, BuildingSlotFactory factory,
            SignalBus signalBus)
        {
            _settings = settings;
            _factory = factory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            CreateSlots();
            _signalBus.Subscribe<AddBuildingSignal>(OnAddBuildingToMap);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuildingFromMap);
        }

        public void OnAddBuildingToMap(AddBuildingSignal args)
        {
            // update amount -> less
            var info = args.BuildingFacade.Info();
            OnAmountChanged(info.Type, info.Size, States.off);
        }

        public void OnRemoveBuildingFromMap(RemoveBuildingSignal args)
        {
            // update amount -> more
            //Debug.Log("Remove building");
            var info = args.BuildingFacade.Info();
            OnAmountChanged(info.Type, info.Size, States.on);
        }

        private void OnAmountChanged(BuildingType type, Size size, States state)
        {
            if (_amounts == null) FillAmounts();
            bool needSlotChange = UpdateAmount(type, size, state);
            //Debug.Log("OnAmountChange: need update? " + needSlotChange);
            if (needSlotChange) ChangeSlotState(type, size, state);                
        }


        private void CreateSlots()
        {
            _slots = new Dictionary<BuildingType, Dictionary<Size, IBuildingSlotFacade>>();

            foreach (BuildingUnit<int> unit in _settings.BuildingTypes)
            {
                _slots[unit.Type] = new Dictionary<Size, IBuildingSlotFacade>();
                _slots[unit.Type].Add(unit.Size, _factory.CreateSlot(unit.Type, unit.Size));
            }

            if (_amounts == null) FillAmounts();
        }

        private void ChangeSlotState(BuildingType type, Size size, States state)
        {
            if (!_slots.ContainsKey(type)) return;
            if (!_slots[type].ContainsKey(size)) return;

            _slots[type][size].SetActiveState(state);

        }

        private void FillAmounts()
        {
            _amounts = new Dictionary<BuildingType, Dictionary<Size, int>>();

            foreach(BuildingUnit<int> unit in _settings.BuildingTypes)
            {
                if (unit.Amount == 0) continue;
                if (!_amounts.ContainsKey(unit.Type)) _amounts[unit.Type] = new Dictionary<Size, int>();
                _amounts[unit.Type].Add(unit.Size, unit.Amount);

                OnAmountUpdated(unit.Type, unit.Size);
            }
        }

        private bool UpdateAmount(BuildingType type, Size size, States state)
        {
            if (!_amounts.ContainsKey(type)) return false;
            if (!_amounts[type].ContainsKey(size)) return false;
            if (_amounts[type][size] == 0  && state == States.off) return false;

            var multiplier = (state == States.on) ? 1 : -1;
            var amount = _amounts[type][size];
            var newAmount = amount + multiplier;

            _amounts[type][size] += multiplier;
            OnAmountUpdated(type, size);


            return ((amount == 0 && newAmount > 0) || (amount > 0 && newAmount == 0));
        }


        private void OnAmountUpdated(BuildingType type, Size size)
        {
            _slots[type][size].UpdateSlotAmount(_amounts[type][size]);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AddBuildingSignal>(OnAddBuildingToMap);
        }
    }
}
