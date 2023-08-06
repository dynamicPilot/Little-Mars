using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Models;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourcesBalanceMenuManager : SideMenuPart//, IInitializable, IDisposable
    {
        readonly SlotUIFactory<ResourceBalanceSlotUI> _factory;
        readonly ProductionManager _productionManager;
        // slots
        Dictionary<Resource, ResourceBalanceSlotUI> _slots = null;

        public ResourcesBalanceMenuManager(SlotUIFactory<ResourceBalanceSlotUI> factory, ProductionManager productionManager) //: base (signalBus)
        {
            _factory = factory;
            _productionManager = productionManager;
        }

        //protected override void SubscribeToUpdate()
        //{
        //    _signalBus.Subscribe<ResourcesProductionChangedSignal>(UpdateProductionForSlots);
        //    _signalBus.Subscribe<ResourcesNeedsChangedSignal>(UpdateNeedsForSlots);
        //}

        //protected override void UnsubscribeToUpdate()
        //{
        //    _signalBus.TryUnsubscribe<ResourcesProductionChangedSignal>(UpdateProductionForSlots);
        //    _signalBus.TryUnsubscribe<ResourcesNeedsChangedSignal>(UpdateNeedsForSlots);
        //}

        public override void CreateSlots(RectTransform transform)
        {
            _slots = new Dictionary<Resource, ResourceBalanceSlotUI>();

            for (int i = 0; i < (int)Resource.all; i++)
                _slots.Add((Resource)i, _factory.CreateSlot(i, transform));
        }

        public override void UpdateSlots()
        {
        }

        void UpdateProductionForSlots(ResourcesProductionChangedSignal args)
        {
            if (_slots == null) return;

            foreach (Resource resource in args.Production.Keys)
            {
                if (!_slots.ContainsKey(resource)) continue;
                _slots[resource].UpdatePlusValue(args.Production[resource][args.Period]);
            }
        }

        void UpdateNeedsForSlots(ResourcesNeedsChangedSignal args)
        {
            if (_slots == null) return;

            foreach (Resource resource in args.Needs.Keys)
            {
                if (!_slots.ContainsKey(resource)) continue;
                _slots[resource].UpdateMinusValue(args.Needs[resource]);
            }
        }
    }
}
