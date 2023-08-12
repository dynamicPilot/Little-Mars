using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceSlotMenuManager : IInitializable, IDisposable
    {
        readonly SlotUIFactory<ResourceSlotUI> _factory;
        readonly SignalBus _signalBus;
        readonly GameUI _gameUI;
        Dictionary<Resource, ResourceSlotUI> _slots = null;

        public ResourceSlotMenuManager(SlotUIFactory<ResourceSlotUI> factory, SignalBus signalBus, GameUI gameUI)
        {
            _factory = factory;
            _signalBus = signalBus;
            _gameUI = gameUI;
        }

        public void Initialize()
        {           
            CreateSlots();
            _signalBus.Subscribe<ResourcesBalanceUpdatedSignal>(UpdateSlots);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ResourcesBalanceUpdatedSignal>(UpdateSlots);
        }

        private void CreateSlots()
        {
            _slots = new Dictionary<Resource, ResourceSlotUI>();
            for (int i = 0; i < (int)Resource.all; i++)
            {
                _slots.Add((Resource)i, _factory.CreateSlot(i, _gameUI.ResourceSlotParent));
            }

            _gameUI.ResourceGridControl.SetWidth((int)Resource.all);
        }

        private void UpdateSlots(ResourcesBalanceUpdatedSignal args)
        {
            if (_slots == null) return;

            foreach(Resource resource in args.ResourcesBalance.Keys)
            {
                if (!_slots.ContainsKey(resource)) continue;
                _slots[resource].UpdateSlot(args.ResourcesBalance[resource]);
            }
        }

    }
}
