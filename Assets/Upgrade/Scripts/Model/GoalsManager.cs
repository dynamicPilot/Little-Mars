using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Model.Interfaces;
using LittleMars.Model.Trackers;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Model
{
    public class GoalsManager : IInitializable
    {
        readonly Settings _settings;
        readonly GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>.Factory _buildingProviderFactory;
        readonly GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>.Factory _productionProviderFactory;
        readonly GoalsTrackerProvider<ResourceUnit<float>, ResourceBalanceGoalTracker>.Factory _resourceProviderFactory;

        List<IGoalTracker> _trackers;

        public GoalsManager(Settings settings,
            GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>.Factory buildingProviderFactory,
            GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>.Factory productionProviderFactory,
            GoalsTrackerProvider<ResourceUnit<float>, ResourceBalanceGoalTracker>.Factory resourceProviderFactory)
        {
            _settings = settings;
            _buildingProviderFactory = buildingProviderFactory;
            _productionProviderFactory = productionProviderFactory;
            _resourceProviderFactory = resourceProviderFactory;

            _trackers = new();
            
        }

        public void Initialize()
        {
            CreateTrackers();
        }

        private void CreateTrackers()
        {
            int currentIndex = 0;
            if (_settings.HasBuildingGoals || _settings.HasBuildingGoalsWithTimer)
            {
                using var buildingProvider = _buildingProviderFactory.Create();

                if (_settings.HasBuildingGoals)
                    _trackers.AddRange(buildingProvider.CreateTrackers(_settings.BuildingGoals, ref currentIndex));

                if (_settings.HasBuildingGoalsWithTimer)
                    _trackers.AddRange(buildingProvider.CreateTrackersWithTimer(_settings.BuildingGoalsWithTimers, ref currentIndex));
            }

            if (_settings.HasProductionGoals)
            {
                using var productionProvider = _productionProviderFactory.Create();
                _trackers.AddRange(productionProvider.CreateTrackers(_settings.ProductionGoals, ref currentIndex));
            }

            if (_settings.HasResourcesGoals)
            {
                using var resourcesProvider = _resourceProviderFactory.Create();
                _trackers.AddRange(resourcesProvider.CreateTrackers(_settings.ResourceGoals, ref currentIndex));

            }
        }


        [Serializable]
        public class Settings
        {
            // building goals
            public bool HasBuildingGoals = false;
            public Goals<BuildingUnit<int>> BuildingGoals;

            public bool HasBuildingGoalsWithTimer = false;
            public GoalsWithTimer<BuildingUnit<int>> BuildingGoalsWithTimers;

            // resources goals
            public bool HasResourcesGoals = false;
            public Goals<ResourceUnit<float>> ResourceGoals;

            // production goals
            public bool HasProductionGoals = false;
            public Goals<ResourceUnit<float>> ProductionGoals;

        }
    }
}
