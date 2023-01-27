using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using LittleMars.UI.GoalDisplays;
using System.Collections.Generic;

namespace LittleMars.Model.GoalDisplays
{
    public class GoalDisplayStrategiesFactory
    {
        readonly GoalsManager.Settings _settings;
        readonly GoalDisplayStrategyFactory _strategyFactory;

        readonly GoalInfoFactory<BuildingUnit<int>> _buildingFactory;
        readonly WithTimerGoalInfoFactory<BuildingUnit<int>> _buildingWithTimerFactory;
        readonly GoalInfoFactory<ResourceUnit<float>> _resourceFactory;

        public GoalDisplayStrategiesFactory(GoalsManager.Settings settings,
            GoalDisplayStrategyFactory stateFactory,
            GoalInfoFactory<BuildingUnit<int>> buildingUnitFactory,
            WithTimerGoalInfoFactory<BuildingUnit<int>> buildingWithTimerFactory,
            GoalInfoFactory<ResourceUnit<float>> resourceUnitFactory)
        {
            _settings = settings;
            _strategyFactory = stateFactory;
            _buildingFactory = buildingUnitFactory;
            _buildingWithTimerFactory = buildingWithTimerFactory;
            _resourceFactory = resourceUnitFactory;
        }

        public List<IGoalDisplayStrategy> CreateStrategies()
        {
            var slots = new List<IGoalDisplayStrategy>();
             
            if (_settings.HasBuildingGoals)
            {
                slots.AddRange(CreateSlots<BuildingUnit<int>>(GoalType.building,_settings.BuildingGoals, 
                    _buildingFactory));
            }

            if (_settings.HasBuildingGoalsWithTimer)
            {
                slots.AddRange(CreateSlotsWithTimer<BuildingUnit<int>>(GoalType.building, 
                    _settings.BuildingGoalsWithTimers, _buildingWithTimerFactory));
            }
 
            if (_settings.HasProductionGoals)
            {
                slots.AddRange(CreateSlots<ResourceUnit<float>>(GoalType.production, 
                    _settings.ProductionGoals, _resourceFactory));
            }

            if (_settings.HasResourcesGoals)
            {
                slots.AddRange(CreateSlots<ResourceUnit<float>>(GoalType.resources, 
                    _settings.ResourceGoals, _resourceFactory));
            }

            return slots;
        }

        private List<IGoalDisplayStrategy> CreateSlots<T>(GoalType type, Goals<T> goals, GoalInfoFactory<T> factory)
        {
            var slots = new List<IGoalDisplayStrategy>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                var info = factory.Create(type, goals.GoalsArray[i].Unit);
                slots.Add(_strategyFactory.CreateStrategy(info));
            }

            return slots;
        }


        private List<IGoalDisplayStrategy> CreateSlotsWithTimer<T>(GoalType type, GoalsWithTimer<T> goals, WithTimerGoalInfoFactory<T> factory)
        {
            var slots = new List<IGoalDisplayStrategy>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                var info = factory.Create(type, goals.GoalsArray[i].Time, goals.GoalsArray[i].Unit);
                slots.Add(_strategyFactory.CreateStrategy(info));
            }

            return slots;
        }

    }
}
