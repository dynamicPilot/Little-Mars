using LittleMars.Common.Signals;
using LittleMars.Model;
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
        readonly GoalsManager _goalsManager;
        // _slots
        List<IGoalSlot> _slots = null;

        public GoalSlotMenuManager(GoalSlotsUIFactory factory, SignalBus signalBus, GameUI gameUI, GoalsManager goalsManager)
            : base(Common.MenuInitType.goals)
        {
            _factory = factory;
            _signalBus = signalBus;
            _gameUI = gameUI;
            _goalsManager = goalsManager;
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
            InitialSlotsUpdate();

            _signalBus.Unsubscribe<NeedMenuInitSignal>(OnNeedInit);
            _signalBus.Subscribe<GoalUpdatedSignal>(OnGoalUpdated);
        }

        private void CreateSlots()
        {
            _slots = _factory.CreateSlots(_gameUI.GoalsSlotParent);
        }

        private void InitialSlotsUpdate()
        {
            var trackers = _goalsManager.GetTrackers();

            for(int i = 0; i < trackers.Count; i++)
            {
                OnGoalUpdated(trackers[i].GetInfo());
            }
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
