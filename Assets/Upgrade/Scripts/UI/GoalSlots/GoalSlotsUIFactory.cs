using LittleMars.Common;
using LittleMars.Model;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI.GoalSlots
{
    public class GoalSlotsUIFactory
    {
        readonly BuildingGoalSlotsUIFactory.Factory _buildingFactory;
        readonly ResourceGoalSlotsUIFactory.Factory _resourceFactory;
        readonly GoalsManager.Settings _settings;

        public GoalSlotsUIFactory(BuildingGoalSlotsUIFactory.Factory buildingFactory, 
            ResourceGoalSlotsUIFactory.Factory resourceFactory, GoalsManager.Settings settings)
        {
            _buildingFactory = buildingFactory;
            _resourceFactory = resourceFactory;
            _settings = settings;
        }

        public List<IGoalSlot> CreateSlots(RectTransform container)
        {
            // no time goal
            var slots = new List<IGoalSlot>();
            int siblingIndex = 1;
            if (_settings.HasBuildingGoals || _settings.HasBuildingGoalsWithTimer)
            {
                using var buildingFactory = _buildingFactory.Create();

                if (_settings.HasBuildingGoals)
                {
                    slots.AddRange(buildingFactory.CreateSlots(_settings.BuildingGoals, container, ref siblingIndex));
                }

            }

            if (_settings.HasProductionGoals || _settings.HasResourcesGoals)
            {
                using var resourceFactory = _resourceFactory.Create();

                if (_settings.HasProductionGoals)
                    slots.AddRange(resourceFactory.CreateSlots(_settings.ProductionGoals, GoalType.production,
                        container, ref siblingIndex));

                if (_settings.HasResourcesGoals)
                    slots.AddRange(resourceFactory.CreateSlots(_settings.ResourceGoals, GoalType.resources,
                        container, ref siblingIndex));
            }

            return slots;
        }

    }

}
