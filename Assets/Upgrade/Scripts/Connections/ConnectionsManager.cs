using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Connections
{
    public class ConnectionsManager : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly SlotConnectonsFactory _factory;

        List<List<SlotConnections>> _connections = null;

        public ConnectionsManager(SignalBus signalBus, SlotConnectonsFactory factory)
        {
            _signalBus = signalBus;
            _factory = factory;
        }

        public void Initialize()
        {
            Debug.Log("Created connections ");
            _signalBus.Subscribe<MapSlotsAreReadySignal>(CreateConnection);

            _signalBus.Subscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.TryUnsubscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        public IEnumerable<BuildingType> GetConnectionsForSlots(IEnumerable<Indexes> indexes)
        {
            List<BuildingType> connections = new();

            foreach (Indexes index in indexes)
            {
                var slot = _connections[index.Row][index.Column];
                connections.AddRange(slot.GetActiveConnections());
            }

            return connections;
        }

        private void CreateConnection()
        {
            _signalBus.Unsubscribe<MapSlotsAreReadySignal>(CreateConnection);
            _connections = _factory.CreateSlotConnections();

            Debug.Log("Created connections " + _connections.Count);
        }

        private void OnAddBuilding(AddBuildingSignal args)
        {
            var indexes = args.BuildingFacade.MapSlotIndexes();
            var slots = new List<SlotConnections>();

            // add connections to this building
            foreach (Indexes index in indexes)
            {
                var slot = _connections[index.Row][index.Column];
                slot.AddBuilding(args.BuildingFacade.Info().Type,
                    args.BuildingFacade.Connections());
                slots.Add(slot);
            }

            OnSlotConnectionUpdate(slots.ToArray());
        }

        private void OnRemoveBuilding(RemoveBuildingSignal args)
        {
            // remove connections to this building
            var indexes = args.BuildingFacade.MapSlotIndexes();
            var slots = new List<SlotConnections>();

            // add connections to this building
            foreach (Indexes index in indexes)
            {
                var slot = _connections[index.Row][index.Column];
                slot.RemoveBilding();
                slots.Add(slot);
            }

            OnSlotConnectionUpdate(slots.ToArray());
        }

        private void OnSlotConnectionUpdate(SlotConnections[] slots)
        {
            _signalBus.Fire(new SlotConnectionsUpdatedSignal
            {
                Slots = slots
            });
        }

    }
}
