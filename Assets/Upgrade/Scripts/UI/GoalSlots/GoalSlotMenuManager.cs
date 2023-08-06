using LittleMars.Common.Signals;
using LittleMars.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class GoalSlotMenuManager : SideMenuPart
    {
        readonly GoalSlotsUIFactory _factory;
        readonly GoalsManager _goalsManager;
        // _slots
        List<IGoalSlot> _slots = null;

        public GoalSlotMenuManager(GoalSlotsUIFactory factory, GoalsManager goalsManager)
            //: base(signalBus)
        {
            _factory = factory;
            _goalsManager = goalsManager;
        }

        //protected override void SubscribeToUpdate()
        //{
        //    _signalBus.TryUnsubscribe<GoalUpdatedSignal>(OnGoalUpdated);
        //}

        //protected override void UnsubscribeToUpdate()
        //{
        //    _signalBus.TryUnsubscribe<GoalUpdatedSignal>(OnGoalUpdated);
        //}

        public override void CreateSlots(RectTransform transform)
        {
            _slots = _factory.CreateSlots(transform);
        }

        public override void UpdateSlots()
        {
            var trackers = _goalsManager.GetTrackers();

            for (int i = 0; i < trackers.Count; i++)
            {
                OnGoalUpdated(trackers[i].GetInfo());
            }
        }

        void OnGoalUpdated(GoalUpdatedSignal args)
        {
            int index = args.Index;

            if (_slots.Count <= index) return;
            _slots[index].UpdateSlot(args);
        }
    }

}
