using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourcesBalanceMenuManager : SideMenu, IInitializable, IDisposable
    {
        readonly SlotUIFactory<ResourceBalanceSlotUI> _factory;
        readonly SignalBus _signalBus;
        readonly GameUI _gameUI;

        Dictionary<Resource, ResourceBalanceSlotUI> _slots = null;

        public ResourcesBalanceMenuManager(SlotUIFactory<ResourceBalanceSlotUI> factory, 
            SignalBus signalBus, GameUI gameUI) : base (MenuInitType.resources)
        {
            _factory = factory;
            _signalBus = signalBus;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            //CreateSlots();
            _signalBus.Subscribe<NeedMenuInitSignal>(OnNeedInit);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ResourcesProductionChangedSignal>(UpdateProductionForSlots);
            _signalBus.TryUnsubscribe<ResourcesNeedsChangedSignal>(UpdateNeedsForSlots);
        }

        public override void OnNeedInit(NeedMenuInitSignal args)
        {
            base.OnNeedInit(args);
            CreateSlots();

            _signalBus.Unsubscribe<NeedMenuInitSignal>(OnNeedInit);
            _signalBus.Subscribe<ResourcesProductionChangedSignal>(UpdateProductionForSlots);
            _signalBus.Subscribe<ResourcesNeedsChangedSignal>(UpdateNeedsForSlots);
        }

        private void CreateSlots()
        {
            _slots = new Dictionary<Resource, ResourceBalanceSlotUI>();

            for (int i = 0; i < (int)Resource.all; i++)
                _slots.Add((Resource)i, _factory.CreateSlot(i, _gameUI.ResourceBalanceSlotParent));
        }


        private void UpdateProductionForSlots(ResourcesProductionChangedSignal args)
        {
            if (_slots == null) return;

            foreach (Resource resource in args.Production.Keys)
            {
                if (!_slots.ContainsKey(resource)) continue;
                _slots[resource].UpdatePlusValue(args.Production[resource][args.Period]);
            }
        }

        private void UpdateNeedsForSlots(ResourcesNeedsChangedSignal args)
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
