using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using LittleMars.Model.Interfaces;
using LittleMars.Model.Trackers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model
{
    public class GoalsManager : IInitializable, IDisposable
    {
        readonly Settings _settings;
        readonly SignalBus _signalBus;
        readonly GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>.Factory _buildingProviderFactory;
        readonly GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>.Factory _productionProviderFactory;
        readonly GoalsTrackerProvider<ResourceUnit<float>, ResourceBalanceGoalTracker>.Factory _resourceProviderFactory;
        readonly StaffTrackersProvider.Factory _staffProviderFactory;

        List<IGoalTracker> _trackers;
        List<IGoalTracker> _staffTrackers;

        AchievementReachedSignal _achivementSignal;

        public GoalsManager(Settings settings,
            GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>.Factory buildingProviderFactory,
            GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>.Factory productionProviderFactory,
            GoalsTrackerProvider<ResourceUnit<float>, ResourceBalanceGoalTracker>.Factory resourceProviderFactory,
            StaffTrackersProvider.Factory staffProviderFactory,
            SignalBus signalBus)
        {
            _settings = settings;
            _buildingProviderFactory = buildingProviderFactory;
            _productionProviderFactory = productionProviderFactory;
            _resourceProviderFactory = resourceProviderFactory;
            _staffProviderFactory = staffProviderFactory;
            _signalBus = signalBus;

            _trackers = new();
            _staffTrackers = new();
            
        }

        public void Initialize()
        {
            CreateTrackers();
            CreateStaffTrackers();
            _signalBus.Subscribe<GoalIsDoneSignal>(OnGoalIsDone);

            _achivementSignal = new AchievementReachedSignal { GoalIndex = 0 };
        }

        public void Dispose()
        {
            _trackers.Clear();
            _staffTrackers.Clear();
            _signalBus.TryUnsubscribe<GoalIsDoneSignal>(OnGoalIsDone);
        }

        public List<IGoalTracker> GetTrackers()
        {
            return _trackers;
        }

        public IGoalTracker GetStaffTracker(int index)
        {
            Debug.Log("Need staff tracker with index " + index);
            if (index < 0 || index >= _staffTrackers.Count) return null;
            else return _staffTrackers[index];
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

        private void CreateStaffTrackers()
        {
            using var provider = _staffProviderFactory.Create();
            _staffTrackers.AddRange(provider.CreateTrackers());
        }

        private void OnGoalIsDone(GoalIsDoneSignal args)
        {
            //Debug.Log("OnGoalIsDone for goal index " + args.Index);
            if (args.ResultType == ResultType.win) OnGoalToWinIsDone(args);
            else OnGoalToLoseIsDone(args);
        }

        private void OnGoalToWinIsDone(GoalIsDoneSignal args)
        {
            // check all goals
            var result = CheckTrackers();
            // rise achivement display is needed
            if (args.IsFirstDone) //!result && 
            {
                //Debug.Log("need Achivement Display for goal index " + args.Index);
                OnAchivement(args.Index);
            }

            if (result)
                _signalBus.Fire<EndGameReachedSignal>();
        }

        private void OnGoalToLoseIsDone(GoalIsDoneSignal args)
        {
            // call game over
            Debug.Log("GAME OVER!");
            _signalBus.Fire(OnGameOver(args));

        }

        private void OnAchivement(int goalIndex)
        {
            _achivementSignal.GoalIndex = goalIndex;
            //Debug.Log("OnAchivement for goal index " + _achivementSignal.GoalIndex + " and index " + goalIndex);
            _signalBus.Fire(_achivementSignal);
        }

        private GameOverSignal OnGameOver(GoalIsDoneSignal args)
        {
            return new GameOverSignal
            {
                GoalIndex = args.Index,
                IsStaff = (args.ResultType == ResultType.loseStaff)
            };
        }

        private bool CheckTrackers()
        {
            for(int i = 0; i < _trackers.Count; i++)
            {
                if (!_trackers[i].Check()) return false;
            }

            return true;
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
