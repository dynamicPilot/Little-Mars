using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using System.Collections.Generic;

namespace LittleMars.Model.GoalInfos
{
    public class GoalInfosFactory
    {
        readonly GoalsManager.Settings _settings;
        readonly GoalInfo<BuildingUnit<int>>.Factory _buildingUnitFactory;
        readonly GoalInfo<ResourceUnit<float>>.Factory _resourceUnitFactory;

        public GoalInfosFactory(GoalsManager.Settings settings, 
            GoalInfo<BuildingUnit<int>>.Factory buildingUnitFactory, 
            GoalInfo<ResourceUnit<float>>.Factory resourceUnitFactory)
        {
            _settings = settings;
            _buildingUnitFactory = buildingUnitFactory;
            _resourceUnitFactory = resourceUnitFactory;
        }

        public List<IGoalInfo> CreateInfos()
        {
            var slots = new List<IGoalInfo>();
             
            if (_settings.HasBuildingGoals)
            {
                CreateSlots<BuildingUnit<int>>(GoalType.building,_settings.BuildingGoals, _buildingUnitFactory);
            }

            if (_settings.HasBuildingGoalsWithTimer)
            {
                CreateSlotsWithTimer<BuildingUnit<int>>(GoalType.building, _settings.BuildingGoalsWithTimers, _buildingUnitFactory);
            }
 
            if (_settings.HasProductionGoals)
            {
                CreateSlots<ResourceUnit<float>>(GoalType.production, _settings.ProductionGoals, _resourceUnitFactory);
            }

            if (_settings.HasResourcesGoals)
            {
                CreateSlots<ResourceUnit<float>>(GoalType.resources, _settings.ResourceGoals, _resourceUnitFactory);
            }

            return slots;
        }

        private List<IGoalInfo> CreateSlots<T>(GoalType type, Goals<T> goals, GoalInfo<T>.Factory factory)
        {
            var slots = new List<IGoalInfo>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                slots.Add(factory.Create(type, false, goals.GoalsArray[i].Unit));
            }

            return slots;
        }


        private List<IGoalInfo> CreateSlotsWithTimer<T>(GoalType type, GoalsWithTimer<T> goals, GoalInfo<T>.Factory factory)
        {
            var slots = new List<IGoalInfo>();

            for (int i = 0; i < goals.GoalsArray.Length; i++)
            {
                slots.Add(factory.Create(type, true, goals.GoalsArray[i].Unit));
            }

            return slots;
        }

    }
}
