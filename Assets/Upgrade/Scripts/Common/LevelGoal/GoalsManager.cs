using LittleMars.Common.Interfaces;
using LittleMars.Model.Interfaces;
using LittleMars.Model.Trackers;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Common.LevelGoal
{
    public class GoalsManager : IInitializable
    {
        readonly Settings _settings;
        readonly GoalsTrackerProvider<BuildingUnit<int>>.Factory _buildingProviderFactory;
        readonly GoalsTrackerProvider<ResourceUnit<float>>.Factory _resourcesProviderFactory;

        List<IGoalTracker> _trackers;
        public GoalsManager(Settings settings,
            GoalsTrackerProvider<BuildingUnit<int>>.Factory buildingProviderFactory,
            GoalsTrackerProvider<ResourceUnit<float>>.Factory resourcesProviderFactory)
        {
            _settings = settings;
            _buildingProviderFactory = buildingProviderFactory;
            _resourcesProviderFactory = resourcesProviderFactory;

            _trackers = new();
        }

        public void Initialize()
        {
            CreateTrackers();
        }

        private void CreateTrackers()
        {
            if (_settings.HasBuildingGoals || _settings.HasBuildingGoalsWithTimer)
            {
                using var buildingProvider = _buildingProviderFactory.Create();

                if (_settings.HasBuildingGoals)
                    _trackers.AddRange(buildingProvider.CreateTrackers(_settings.BuildingGoals));

                if (_settings.HasBuildingGoalsWithTimer)
                    _trackers.AddRange(buildingProvider.CreateTrackersWithTimer(_settings.BuildingGoalsWithTimers));
            }

            if (_settings.HasProductionGoals || _settings.HasResourcesGoals)
            {
                using var productionProvider = _resourcesProviderFactory.Create();

                if (_settings.HasProductionGoals)
                    _trackers.AddRange(productionProvider.CreateTrackers(_settings.ProductionGoals));

                if (_settings.HasResourcesGoals)
                    _trackers.AddRange(productionProvider.CreateTrackers(_settings.ResourceGoals));
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
