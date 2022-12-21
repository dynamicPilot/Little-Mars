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
    public class ViewSlotManager : IInitializable, IDisposable
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
            _signalBus.Subscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.TryUnsubscribe<RemoveBuildingSignal>(OnRemoveBuilding);
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

        public void PlacingBuildingInSlots(IEnumerable<Indexes> indexes)
        {
            ChangeSlotStates(indexes, SlotStates.placing);
        }

        public void RemoveBuildingFromSlots(IEnumerable<Indexes> indexes)
        {
            ChangeSlotStates(indexes, SlotStates.empty);
        }

        private void ChangeSlotStates(IEnumerable<Indexes> indexes, SlotStates state)
        {
            foreach (Indexes ind in indexes)
                _slots[ind.Row][ind.Column].ChangeSlotStateTo(state);
        }

        
    }
}
