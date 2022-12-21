using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using static UnityEngine.UI.CanvasScaler;

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

        readonly LevelConditions.Settings _settings;
        readonly BuildingSlotFactory _factory;
        readonly SignalBus _signalBus;

        public BuildingMenuManager(LevelConditions.Settings settings, BuildingSlotFactory factory,
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
            OnAmountChanged(info.Type, info.Size, ProductionState.off);
        }

        public void OnRemoveBuildingFromMap(RemoveBuildingSignal args)
        {
            // update amount -> more
            Debug.Log("Remove building");
            var info = args.BuildingFacade.Info();
            OnAmountChanged(info.Type, info.Size, ProductionState.on);
        }

        private void OnAmountChanged(BuildingType type, Size size, ProductionState state)
        {
            if (_amounts == null) FillAmounts();
            bool needSlotChange = UpdateAmount(type, size, state);
            Debug.Log("OnAmountChange: need update? " + needSlotChange);
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
        }

        private void ChangeSlotState(BuildingType type, Size size, ProductionState state)
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
            }
        }

        private bool UpdateAmount(BuildingType type, Size size, ProductionState state)
        {
            if (!_amounts.ContainsKey(type)) return false;
            if (!_amounts[type].ContainsKey(size)) return false;
            if (_amounts[type][size] == 0  && state == ProductionState.off) return false;

            var multiplier = (state == ProductionState.on) ? 1 : -1;
            var amount = _amounts[type][size];
            var newAmount = amount + multiplier;

            _amounts[type][size] += multiplier;

            return ((amount == 0 && newAmount > 0) || (amount > 0 && newAmount == 0));
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AddBuildingSignal>(OnAddBuildingToMap);
        }
    }
}
