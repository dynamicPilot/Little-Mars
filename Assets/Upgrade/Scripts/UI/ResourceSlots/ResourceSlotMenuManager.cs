using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using Zenject;
using Resource = LittleMars.Common.Resource;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceSlotMenuManager : IInitializable, IDisposable
    {
        readonly SlotUIFactory<MenuResourceSlotUI> _factory;
        readonly ResourceLimiter _limiter;
        readonly SignalBus _signalBus;
        readonly GameUI _gameUI;
        Dictionary<Resource, MenuResourceSlotUI> _slots = null;

        public ResourceSlotMenuManager(SlotUIFactory<MenuResourceSlotUI> factory, SignalBus signalBus, 
            GameUI gameUI, ResourceLimiter limiter)
        {
            _factory = factory;
            _signalBus = signalBus;
            _gameUI = gameUI;
            _limiter = limiter;
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

        void CreateSlots()
        {
            _slots = new Dictionary<Resource, MenuResourceSlotUI>();
            for (int i = 0; i < (int)Resource.all; i++)
            {
                _slots.Add((Resource)i, _factory.CreateSlot(i, _gameUI.ResourceSlotParent));
            }

            _gameUI.ResourceGridControl.SetWidth((int)Resource.all);
        }

        void UpdateSlots(ResourcesBalanceUpdatedSignal args)
        {
            if (_slots == null) return;

            foreach(Resource resource in args.ResourcesBalance.Keys)
            {
                if (!_slots.ContainsKey(resource)) continue;
                _slots[resource].UpdateSlotAndIndication(args.ResourcesBalance[resource],
                    _limiter.IsLimit(resource, (args.ResourcesBalance[resource])));
            }
        }


    }
}
