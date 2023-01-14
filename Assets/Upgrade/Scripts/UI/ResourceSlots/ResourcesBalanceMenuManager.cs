using LittleMars.Common;
using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourcesBalanceMenuManager : IInitializable, IDisposable
    {
        readonly SlotUIFactory<ResourceBalanceSlotUI> _factory;
        readonly SignalBus _signalBus;
        readonly GameUI _gameUI;
        Dictionary<Resource, ResourceBalanceSlotUI> _slots = null;

        public ResourcesBalanceMenuManager(SlotUIFactory<ResourceBalanceSlotUI> factory, 
            SignalBus signalBus, GameUI gameUI)
        {
            _factory = factory;
            _signalBus = signalBus;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            CreateSlots();
            _signalBus.Subscribe<ResourcesProductionChangedSignal>(UpdateProductionForSlots);
            _signalBus.Subscribe<ResourcesNeedsChangedSignal>(UpdateNeedsForSlots);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ResourcesProductionChangedSignal>(UpdateProductionForSlots);
            _signalBus.TryUnsubscribe<ResourcesNeedsChangedSignal>(UpdateNeedsForSlots);
        }

        private void CreateSlots()
        {
            _slots = new Dictionary<Resource, ResourceBalanceSlotUI>();

            for (int i = 0; i < (int)Resource.all; i++)
                _slots.Add((Resource)i, _factory.CreateSlot((Resource)i, _gameUI.ResourceBalanceSlotParent));
        }


        private void UpdateProductionForSlots(ResourcesProductionChangedSignal args)
        {
            if (_slots == null) return;

            //foreach (Resource resource in args.ResourcesBalance.Keys)
            //{
            //    if (!_slots.ContainsKey(resource)) continue;
            //    _slots[resource].UpdateSlot(args.ResourcesBalance[resource]);
            //}
        }

        private void UpdateNeedsForSlots(ResourcesNeedsChangedSignal args)
        {
            if (_slots == null) return;

            //foreach (Resource resource in args.ResourcesBalance.Keys)
            //{
            //    if (!_slots.ContainsKey(resource)) continue;
            //    _slots[resource].UpdateSlot(args.ResourcesBalance[resource]);
            //}
        }
    }
}
