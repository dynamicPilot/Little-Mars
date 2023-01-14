using UnityEngine;
using Zenject;
using System;
using LittleMars.Slots;
using LittleMars.Map;
using LittleMars.Map.States;
using LittleMars.Common.Signals;
using LittleMars.Models.Facades;
using LittleMars.Common;
using LittleMars.Map.Routers;
using LittleMars.Buildings;
using LittleMars.Models;
using LittleMars.Common.Interfaces;
using LittleMars.Model.Interfaces;
using LittleMars.UI.BuildingSlots;
using LittleMars.Model;
using LittleMars.UI;
using UnityEngine.PlayerLoop;
using LittleMars.Model.TimeUpdate;
using LittleMars.Common.LevelGoal;
using LittleMars.Model.Trackers;
using LittleMars.UI.ResourceSlots;
using LittleMars.Slots.UI;

namespace LittleMars.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private TimeUpdater _timeUpdater;

        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            InstallMap();
            InstallModel();
            InstallTrackers();
            InstallBuildings();
            InstallViewSlots();
            InstallBuildingSlots();
            InstallUIAndManagers();
            InstallSignals();
            InstallExecutionOrder();
        }

        void InstallExecutionOrder()
        {
            Container.BindExecutionOrder<ViewSlotManager>(-20);
            Container.BindExecutionOrder<ResourceSlotMenuManager>(-20);

            Container.BindExecutionOrder<MapManager>(-10);
        }

        private void InstallMap()
        {            
            Container.BindInterfacesAndSelfTo<MapManager>().AsSingle();
            Container.BindFactory<MapSlotExtended, MapSlotExtended.Factory>();

            Container.Bind<MapSlotFactory>().AsSingle();

            // slot states
            Container.Bind<MapSlotCreatorStateFactory>().AsSingle();

            Container.BindFactory<AutoMapSlotCreatorState, AutoMapSlotCreatorState.Factory>().WhenInjectedInto<MapSlotCreatorStateFactory>();
            Container.BindFactory<CustomMapSlotCreatorState, CustomMapSlotCreatorState.Factory>().WhenInjectedInto<MapSlotCreatorStateFactory>();

            // fields
            Container.Bind<FieldManager>().AsSingle();
            
            Container.BindFactory<ResourceField<MapSlotExtended>, ResourceField<MapSlotExtended>.Factory>();

        }

        public void InstallModel()
        {
            Container.BindInstance(_timeUpdater);

            Container.BindInterfacesAndSelfTo<OperationManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesBalancer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProductionManager>().AsSingle();

            Container.Bind<TimeManager>().AsSingle();
            Container.Bind<IconsCatalogue>().AsSingle();            
            Container.Bind<ProductionHelper>().AsSingle();
            Container.Bind<ConstructionHelper>().AsSingle();
            Container.Bind<OperationHelper>().AsSingle();
            Container.Bind<MapRouter>().AsSingle();
            Container.Bind<PlacementManager>().AsSingle();
            Container.Bind<BuildingController>().AsSingle();

            Container.Bind<IModelFacade>().To<ModelFacade>().AsSingle();

            Container.Bind<IPlacement>().To<PlacementManager>().FromResolve();

            Container.BindFactory<BuildingType, Size, Path, Vector2, PlacingBuilding, PlacingBuilding.Factory>()
                .WhenInjectedInto<PlacementManager>();
            Container.BindFactory<MapRouterCheck, MapRouterCheck.Factory>().WhenInjectedInto<FieldManager>();
            Container.BindFactory<BuildingObject, MapRouterCheckForBuilding, MapRouterCheckForBuilding.Factory>()
                .WhenInjectedInto<PlacementManager>();
        }

        public void InstallTrackers()
        {
            Container.BindInterfacesAndSelfTo<GoalsManager>().AsSingle();

            // Container.BindFactory<IPathFindingStrategy, PathFindingStrategyFactory>().To<AStarPathFindingStrategy>();
            //Container.BindFactory<BuildingUnit<int>, IGoalTracker, TrackerFactory<BuildingUnit<int>>().To<BuildingGoalTracker>();
            Container.BindFactory<GoalsTrackerProvider<BuildingUnit<int>>, GoalsTrackerProvider<BuildingUnit<int>>.Factory>().WhenInjectedInto<GoalsManager>();
            Container.BindFactory<GoalsTrackerProvider<ResourceUnit<float>>, GoalsTrackerProvider<ResourceUnit<float>>.Factory>().WhenInjectedInto<GoalsManager>();

            Container.BindFactory<Goal<BuildingUnit<int>>, IGoalTracker, TrackerFactory<BuildingUnit<int>>>()
                .To<BuildingGoalTracker>()
                .WhenInjectedInto<GoalsTrackerProvider<BuildingUnit<int>>>();

            Container.BindFactory<GoalWithTime<BuildingUnit<int>>, IGoalTracker, TrackerFactoryWithTimer<BuildingUnit<int>>>()
                .To<BuildingGoalTrackerWithTimer>()
                .WhenInjectedInto<GoalsTrackerProvider<BuildingUnit<int>>>();
        }

        public void InstallBuildings()
        {
            Container.BindInterfacesAndSelfTo<BuildingManager>().AsSingle();
            Container.Bind<BuildingStorage>().AsSingle();
            Container.Bind<BuildingProvider>().AsSingle();

            Container.BindFactory<BuildingType, Size, BuildingFacade, BuildingFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<BuildingInstaller>(_settings.BuildingPrefab)
                .WithGameObjectName("Building")
                .UnderTransformGroup("Buildings");
        }

        private void InstallViewSlots()
        {            
            Container.BindInterfacesAndSelfTo<ViewSlotManager>().AsSingle();
            Container.Bind<ViewSlotFactory>().AsSingle();

            Container.BindFactory<Vector2, ViewSlotFacade, ViewSlotFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<ViewSlotInstaller>(_settings.SlotPrefab)
                .WithGameObjectName("Slot")
                .UnderTransformGroup("Slots");
        }

        private void InstallBuildingSlots()
        {
            Container.BindInterfacesAndSelfTo<BuildingMenuManager>().AsSingle();
            Container.Bind<BuildingSlotFactory>().AsSingle();

            Container.BindFactory<BuildingType, Size, BuildingSlotFacade, BuildingSlotFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<BuildingSlotInstaller>(_settings.BuildingSlotPrefab)
                .WithGameObjectName("BuildingSlot");
                //.UnderTransformGroup("Buildings");
        }

        private void InstallUIAndManagers()
        {
            // PlacementMenuUI, GameUI -> bind via ZenjectBuinding Component -> GameUI object

            Container.BindInterfacesAndSelfTo<ResourceSlotMenuManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesBalanceMenuManager>().AsSingle();
            //Container.Bind<ResourceSlotUIFactory>().AsSingle();

            Container.Bind<SlotUIFactory<ResourceSlotUI>>().AsSingle().NonLazy();
            Container.Bind<SlotUIFactory<ResourceBalanceSlotUI>>().AsSingle().NonLazy();
            //Container.Bind(typeof(SlotUIFactory<>)).AsSingle().NonLazy();

            Container.BindFactory<ResourceSlotUI, PlaceholderFactory<ResourceSlotUI>>()
                .FromComponentInNewPrefab(_settings.ResourceSlotPrefab)
                .WithGameObjectName("ResourceSlot");

            Container.BindFactory<ResourceBalanceSlotUI, PlaceholderFactory<ResourceBalanceSlotUI>>()
                .FromComponentInNewPrefab(_settings.ResourceBalanceSlotPrefab)
                .WithGameObjectName("ResourceSlot");
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<MapSlotsAreReadySignal>();

            Container.DeclareSignal<AddBuildingSignal>();
            Container.DeclareSignal<RemoveBuildingSignal>();

            Container.DeclareSignal<BuildingStateChangedSignal>();
            Container.DeclareSignal<TryChangeBuildingStateSignal>();

            Container.DeclareSignal<StartBuildingPlacementSignal>();
            Container.DeclareSignal<BuildingControllerSignal>();

            Container.DeclareSignal<PeriodChangeSignal>();
            Container.DeclareSignal<HourlySignal>();

            Container.DeclareSignal<GoalToWinIsDoneSignal>();

            Container.DeclareSignal<ResourcesBalanceUpdatedSignal>();
            Container.DeclareSignal<ResourcesProductionChangedSignal>();
            Container.DeclareSignal<ResourcesNeedsChangedSignal>();
        }

        [Serializable]
        public class Settings
        {
            public GameObject SlotPrefab;
            public GameObject BuildingPrefab;
            public GameObject BuildingSlotPrefab;
            public GameObject ResourceSlotPrefab;
            public GameObject ResourceBalanceSlotPrefab;
        }
    }
}

