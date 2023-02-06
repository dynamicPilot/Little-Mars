using LittleMars.Common;
using LittleMars.UI.GoalDisplays;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Model.GoalDisplays
{
    public class GoalDisplayStrategiesFactory
    {
        readonly GoalsManager.Settings _settings;
        readonly DisplayStrategiesFactory _strategiesFactory;

        readonly GoalInfoFactory<BuildingUnit<int>> _buildingFactory;
        readonly WithTimerGoalInfoFactory<BuildingUnit<int>> _buildingWithTimerFactory;
        readonly GoalInfoFactory<ResourceUnit<float>> _resourceFactory;

        public GoalDisplayStrategiesFactory(GoalsManager.Settings settings,
            DisplayStrategiesFactory strategiesFactory,
            GoalInfoFactory<BuildingUnit<int>> buildingUnitFactory,
            WithTimerGoalInfoFactory<BuildingUnit<int>> buildingWithTimerFactory,
            GoalInfoFactory<ResourceUnit<float>> resourceUnitFactory)
        {
            _settings = settings;
            _strategiesFactory = strategiesFactory;

            _buildingFactory = buildingUnitFactory;
            _buildingWithTimerFactory = buildingWithTimerFactory;
            _resourceFactory = resourceUnitFactory;
        }

        public List<IGoalDisplayStrategy> CreateStrategies()
        {
            var slots = new List<IGoalDisplayStrategy>();
             
            if (_settings.HasBuildingGoals)
            {
                slots.AddRange(_strategiesFactory.CreateSlots<BuildingUnit<int>>(GoalType.building,
                    _settings.BuildingGoals, 
                    _buildingFactory));
            }

            if (_settings.HasBuildingGoalsWithTimer)
            {
                Debug.Log("Create strategy with timer");
                slots.AddRange(_strategiesFactory.CreateSlotsWithTimer<BuildingUnit<int>>(GoalType.building, 
                    _settings.BuildingGoalsWithTimers, 
                    _buildingWithTimerFactory));
            }
 
            if (_settings.HasProductionGoals)
            {
                slots.AddRange(_strategiesFactory.CreateSlots<ResourceUnit<float>>(GoalType.production, 
                    _settings.ProductionGoals, _resourceFactory));
            }

            if (_settings.HasResourcesGoals)
            {
                slots.AddRange(_strategiesFactory.CreateSlots<ResourceUnit<float>>(GoalType.resources, 
                    _settings.ResourceGoals, _resourceFactory));
            }

            return slots;
        }
    }
}
