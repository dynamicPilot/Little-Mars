using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Slots.States;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Slots
{
    /// <summary>
    /// Control scripts for all slots in View.
    /// </summary>
    public class ViewSlotManager : IInitializable
    {
        List<List<ViewSlotFacade>> _slots = new List<List<ViewSlotFacade>>();
        readonly ViewSlotFactory _factory;
        readonly SignalBus _signalBus;

        public ViewSlotManager(ViewSlotFactory factory, SignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            // spawn slots
            _signalBus.Subscribe<MapSlotsAreReadySignal>(SpawnSlots);
        }

        private void SpawnSlots()
        {
            _slots = _factory.Create();
            _signalBus.Unsubscribe<MapSlotsAreReadySignal>(SpawnSlots);
        }

        public void PlacingBuildingInSlots(List<Indexes> indexes)
        {
            ChangeSlotStates(indexes, SlotStates.placing);
        }

        public void RemoveBuildingFromSlots(List<Indexes> indexes)
        {
            ChangeSlotStates(indexes, SlotStates.empty);
        }

        public void AddBuildingToSlots(List<Indexes> indexes, BuildingType type)
        {
            ChangeSlotStates(indexes, SlotStates.building);
        }

        private void ChangeSlotStates(List<Indexes> indexes, SlotStates state)
        {
            foreach (Indexes ind in indexes)
                _slots[ind.Row][ind.Column].ChangeSlotStateTo(state);
        }

        
    }
}
