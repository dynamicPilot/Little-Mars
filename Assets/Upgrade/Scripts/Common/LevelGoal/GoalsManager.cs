using LittleMars.Model.Trackers;
using System;
using Zenject;

namespace LittleMars.Common.LevelGoal
{
    public class GoalsManager : IInitializable
    {
        readonly Settings _settings;
        readonly GoalsTrackerProvider<BuildingUnit<int>>.Factory _buildingProviderFactory;
        readonly GoalsTrackerProvider<ResourceUnit<float>>.Factory _resourcesProviderFactory;

        public GoalsManager(Settings settings,
            GoalsTrackerProvider<BuildingUnit<int>>.Factory buildingProviderFactory,
            GoalsTrackerProvider<ResourceUnit<float>>.Factory resourcesProviderFactory)
        {
            _settings = settings;
            _buildingProviderFactory = buildingProviderFactory;
            _resourcesProviderFactory = resourcesProviderFactory;
        }

        public void Initialize()
        {
            CreateTrackers();
        }

        private void CreateTrackers()
        {
            if (_settings.HasBuildingGoals)
            {
                using var buildingProvider = _buildingProviderFactory.Create();
                buildingProvider.Create(_settings.BuildingGoals);
            }

            if (_settings.HasProductionGoals)
            {
                using var productionProvider = _resourcesProviderFactory.Create();
                productionProvider.Create(_settings.ProductionGoals);
            }

            if (_settings.HasResourcesGoals)
            {
                using var productionProvider = _resourcesProviderFactory.Create();
                productionProvider.Create(_settings.ResourceGoals);
            }
        }


        [Serializable]
        public class Settings
        {
            // building goals
            public bool HasBuildingGoals = false;
            public Goals<BuildingUnit<int>> BuildingGoals;

            // resources goals
            public bool HasResourcesGoals = false;
            public Goals<ResourceUnit<float>> ResourceGoals;

            // production goals
            public bool HasProductionGoals = false;
            public Goals<ResourceUnit<float>> ProductionGoals;

        }
    }
}
