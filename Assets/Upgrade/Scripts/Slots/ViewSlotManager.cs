using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Connections;
using LittleMars.Slots.States;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Zenject;

namespace LittleMars.Slots
{
    /// <summary>
    /// Control scripts for all slots in View.
    /// </summary>
    public class ViewSlotManager : IInitializable, IDisposable
    { 
        readonly ViewSlotFactory _factory;
        readonly SignalBus _signalBus;

        List<List<ViewSlotFacade>> _slots = null;
        

        public ViewSlotManager(ViewSlotFactory factory, SignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            // spawn slots
            _signalBus.Subscribe<MapSlotsAreReadySignal>(SpawnSlots);
            _signalBus.Subscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.TryUnsubscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        public void PlacingBuildingInSlots(IEnumerable<Indexes> indexes)
        {
            ChangeSlotStates(indexes, SlotStates.placing);
        }

        public void RemoveBuildingFromSlots(IEnumerable<Indexes> indexes)
        {
            ChangeSlotStates(indexes, SlotStates.empty);
        }

        private void SpawnSlots()
        {
            _slots = _factory.Create();
            _signalBus.Unsubscribe<MapSlotsAreReadySignal>(SpawnSlots);
        }

        private void OnAddBuilding(AddBuildingSignal arg)
        {
            ChangeSlotStates(arg.BuildingFacade.MapSlotIndexes(), SlotStates.building);
        }

        private void OnRemoveBuilding(RemoveBuildingSignal arg)
        {
            RemoveBuildingFromSlots(arg.BuildingFacade.MapSlotIndexes());
        }

        private void ChangeSlotStates(IEnumerable<Indexes> indexes, SlotStates state)
        {
            if (_slots == null) return;
            foreach (Indexes ind in indexes)
                _slots[ind.Row][ind.Column].ChangeSlotStateTo(state);
        }

        public void UpdateSlotConnections(Indexes ind, Dictionary<Direction, Connection> connections)
        {
            if (_slots == null) return;
            Debug.Log("Update Slot Connections for index: row " + ind.Row + " col " + ind.Column);
            _slots[ind.Row][ind.Column].UpdateIndicators(connections);
        }

    }
}
