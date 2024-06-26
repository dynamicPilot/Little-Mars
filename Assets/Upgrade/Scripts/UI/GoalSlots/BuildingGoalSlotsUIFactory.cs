﻿using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class BuildingGoalSlotsUIFactory : IDisposable
    {
        readonly SlotUIFactory<BuildingGoalSlotUI> _factory;
        readonly SlotUIFactory<BuildingGoalWithTimerSlotUI> _withTimeFactory;
        readonly ISetSlot _timerSetter;

        public BuildingGoalSlotsUIFactory(SlotUIFactory<BuildingGoalSlotUI> factory,
            SlotUIFactory<BuildingGoalWithTimerSlotUI> withTimeFactory, ISetSlot timerSetter)
        {
            _factory = factory;
            _withTimeFactory = withTimeFactory;
            _timerSetter = timerSetter; 
        }

        public List<BuildingGoalSlotUI> CreateSlots(Goals<BuildingUnit<int>> goals, RectTransform container, 
            ref int siblingIndex)
        {
            if (goals.GoalsArray == null || goals.GoalsArray.Length == 0) return null;

            var slots = new List<BuildingGoalSlotUI>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                slots.Add(CreateSlot(goals.GoalsArray[i], container, siblingIndex));
                siblingIndex++;
            }

            return slots;
        }

        public BuildingGoalSlotUI CreateSlot(Goal<BuildingUnit<int>> goal,
            RectTransform container, int siblingIndex)
        {
            var slot = _factory.CreateSlot((int)goal.Unit.Type, container, siblingIndex);
            slot.SetTargetValue(goal.Unit.Amount);
            SetBuildingSize(slot, (int)goal.Unit.Size);
            return slot;
        }

        public List<BuildingGoalWithTimerSlotUI> CreateWithTimeSlots(GoalsWithTimer<BuildingUnit<int>> goals, 
            RectTransform container, ref int siblingIndex)
        {
            if (goals.GoalsArray == null || goals.GoalsArray.Length == 0) return null;

            var slots = new List<BuildingGoalWithTimerSlotUI>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                slots.Add(CreateWithTimeSlot(goals.GoalsArray[i], container, siblingIndex));
                siblingIndex++;
            }

            return slots;
        }

        public BuildingGoalWithTimerSlotUI CreateWithTimeSlot(GoalWithTime<BuildingUnit<int>> goal, 
            RectTransform container, int siblingIndex)
        {
            var slot = _withTimeFactory.CreateSlot((int)goal.Unit.Type, container, siblingIndex);
            slot.SetTargetValue(goal.Unit.Amount, goal.Time);
            SetBuildingSize(slot, (int)goal.Unit.Size);
            SetTimerIcon(slot);
            return slot;
        }

        private void SetBuildingSize(ISetSize slot, int index)
        {
            slot.SetSize(index);
        }

        private void SetTimerIcon(GoalWithTimerSlotUI slot)
        {
            slot.SetTimerIcon(_timerSetter);
        }

        public void Dispose()
        {
        }

        public class Factory : PlaceholderFactory<BuildingGoalSlotsUIFactory>
        { }
    }

}
