using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class GoalSlotMenuManager : IInitializable, IDisposable
    {
        readonly GoalSlotsUIFactory _factory;
        readonly SignalBus _signalBus;
        readonly GameUI _gameUI;
        // _slots
        List<IGoalSlot> _slots = null;

        public GoalSlotMenuManager(GoalSlotsUIFactory factory, SignalBus signalBus, GameUI gameUI)
        {
            _factory = factory;
            _signalBus = signalBus;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _slots = _factory.CreateSlots(_gameUI.GoalsSlotParent);
            _signalBus.Subscribe<GoalUpdatedSignal>(OnGoalUpdated);
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<GoalUpdatedSignal>(OnGoalUpdated);
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
