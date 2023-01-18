using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalSlots
{
    public class BuildingGoalSlotsUIFactory : IDisposable
    {
        readonly SlotUIFactory<GoalSlotUI> _factory;

        public BuildingGoalSlotsUIFactory(SlotUIFactory<GoalSlotUI> factory)
        {
            _factory = factory;
        }

        public List<GoalSlotUI> CreateSlots(Goals<BuildingUnit<int>> goals, RectTransform container, 
            ref int siblingIndex)
        {
            if (goals.GoalsArray == null || goals.GoalsArray.Length == 0) return null;

            var slots = new List<GoalSlotUI>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                slots.Add(CreateSlot(goals.GoalsArray[i], container, siblingIndex));
                siblingIndex++;
            }

            return slots;
        }

        public GoalSlotUI CreateSlot(Goal<BuildingUnit<int>> goal, RectTransform container, int siblingIndex)
        {
            var slot = _factory.CreateSlot((int)goal.Unit.Type, container, siblingIndex);
            slot.SetTargetValue(goal.Unit.Amount);
            return slot;
        }

        public void Dispose()
        {
        }

        public class Factory : PlaceholderFactory<BuildingGoalSlotsUIFactory>
        { }
    }

    public class ResourceGoalSlotsUIFactory : IDisposable
    {
        readonly SlotUIFactory<ResourceGoalSlotUI> _factory;

        public ResourceGoalSlotsUIFactory(SlotUIFactory<ResourceGoalSlotUI> factory)
        {
            _factory = factory;
        }

        public List<GoalSlotUI> CreateSlots(Goals<ResourceUnit<float>> goals, GoalType type, 
            RectTransform container, ref int siblingIndex)
        {
            if (goals.GoalsArray == null || goals.GoalsArray.Length == 0) return null;

            var slots = new List<GoalSlotUI>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                slots.Add(CreateSlot(goals.GoalsArray[i], type, container, siblingIndex));
                siblingIndex++;
            }

            return slots;
        }

        public ResourceGoalSlotUI CreateSlot(Goal<ResourceUnit<float>> goal, GoalType type, RectTransform container,
            int siblingIndex)
        {
            var slot = _factory.CreateSlot((int)goal.Unit.Type, container, siblingIndex);
            slot.SetTargetValue(goal.Unit.Amount);
            slot.SetGoalType(type);
            return slot;
        }

        public void Dispose()
        {
        }

        public class Factory : PlaceholderFactory<ResourceGoalSlotsUIFactory>
        { }
    }

}
