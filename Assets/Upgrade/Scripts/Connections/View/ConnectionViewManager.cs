using LittleMars.Common.Signals;
using LittleMars.Slots;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Connections.View
{
    public class ConnectionViewManager : IInitializable, IDisposable
    {
        readonly ViewSlotManager _viewManager;
        readonly SignalBus _signalBus;

        public ConnectionViewManager(ViewSlotManager viewManager, SignalBus signalBus)
        {
            _viewManager = viewManager;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<SlotConnectionsUpdatedSignal>(OnSlotConnectionsUpdated);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<SlotConnectionsUpdatedSignal>(OnSlotConnectionsUpdated);
        }

        private void OnSlotConnectionsUpdated(SlotConnectionsUpdatedSignal args)
        {
            //Debug.Log("ConnectionViewManager : OnSlotConnectionsUpdated");
            var queue = new Queue<SlotConnections>();
            var checkedSlots = new List<SlotConnections>();

            for (int i = 0; i < args.Slots.Length; i++)
                queue.Enqueue(args.Slots[i]);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (checkedSlots.Contains(current)) continue;
                
                checkedSlots.Add(current);
                var neighbors = current.GetNeighbors();

                for (int i = 0; i < neighbors.Length; i++)
                {
                    if (!checkedSlots.Contains(neighbors[i]))
                        queue.Enqueue(neighbors[i]);
                }                   

                _viewManager.UpdateSlotConnections(current.Indexes, current.Connections);
            }
        }



    }
}
