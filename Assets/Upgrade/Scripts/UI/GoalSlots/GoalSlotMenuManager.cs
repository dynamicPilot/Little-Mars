using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class GoalSlotMenuManager : SideMenu, IInitializable, IDisposable
    {
        readonly GoalSlotsUIFactory _factory;
        readonly SignalBus _signalBus;
        readonly GameUI _gameUI;
        // _slots
        List<IGoalSlot> _slots = null;

        public GoalSlotMenuManager(GoalSlotsUIFactory factory, SignalBus signalBus, GameUI gameUI)
            : base(Common.MenuInitType.goals)
        {
            _factory = factory;
            _signalBus = signalBus;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<NeedMenuInitSignal>(OnNeedInit);            
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<GoalUpdatedSignal>(OnGoalUpdated);
        }

        public override void OnNeedInit(NeedMenuInitSignal args)
        {
            base.OnNeedInit(args);
            CreateSlots();

            _signalBus.Unsubscribe<NeedMenuInitSignal>(OnNeedInit);
            _signalBus.Subscribe<GoalUpdatedSignal>(OnGoalUpdated);
        }

        private void CreateSlots()
        {
            _slots = _factory.CreateSlots(_gameUI.GoalsSlotParent);
        }

        private void OnGoalUpdated(GoalUpdatedSignal args)
        {
            int index = args.Index;

            if (_slots.Count <= index) return;

            Debug.Log("Call slot for update!");
            _slots[index].UpdateSlot(args);
        }
    }

}
