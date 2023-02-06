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
using LittleMars.Model.TimeUpdate;
using LittleMars.Common.LevelGoal;
using LittleMars.Model.Trackers;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using LittleMars.UI.GoalSlots;
using LittleMars.Connections;
using LittleMars.Connections.View;
using LittleMars.Model.GoalDisplays;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.Achivements;
using LittleMars.Commands;
using LittleMars.UI.LevelMenus;
using LittleMars.Localization;
using LittleMars.Effects;
using LittleMars.UI.GoalTextMenu;

namespace LittleMars.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Langs _lang;

        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            InstallMap();
            InstallModel();            
            InstallTrackers();

            InstallLocalizationSystem();
            InstallEffects();

            InstallBuildings();           
            InstallConnections();

            InstallGoalInfos();
            InstallViewSlots();
            InstallBuildingSlots();
            InstallLevelMenus();
            InstallUIAndManagers();

            InstallSignals();
            InstallExecutionOrder();
        }

        void InstallExecutionOrder()
        {
            Container.BindExecutionOrder<ViewSlotManager>(-20);
            Container.BindExecutionOrder<ConnectionsManager>(-20);
            Container.BindExecutionOrder<ResourceSlotMenuManager>(-20);
            //Container.BindExecutionOrder<StartLevelMenuUI>(-20);

            Container.BindExecutionOrder<MapManager>(-10);
            //Container.BindExecutionOrder<>(-10);
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

        private void InstallModel()
        {
            Container.BindInterfacesAndSelfTo<OperationManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesBalancer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProductionManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeSpeedManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeUpdaterTickable>().AsSingle();

            Container.Bind<TimeManager>().AsSingle();            
            Container.Bind<IconsCatalogue>().AsSingle();
            Container.Bind<ColorsCatalogue>().AsSingle();
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

        private void InstallLocalizationSystem()
        {
            Container.BindInstance(_lang);
            Container.Bind<JsonConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LangsManager>().AsSingle();
        }


        private void InstallEffects()
        {
            Container.BindInterfacesAndSelfTo<PeriodChangeEffectControl>().AsSingle();
        }

        private void InstallTrackers()
        {
            Container.BindInterfacesAndSelfTo<GoalsManager>().AsSingle();

            // Container.BindFactory<IPathFindingStrategy, PathFindingStrategyFactory>().To<AStarPathFindingStrategy>();
            //Container.BindFactory<BuildingUnit<int>, IGoalTracker, TrackerFactory<BuildingUnit<int>>().To<BuildingGoalTracker>();
            Container.BindFactory<GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>, 
                GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>.Factory>().WhenInjectedInto<GoalsManager>();
            
            Container.BindFactory<GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>, 
                GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>.Factory>().WhenInjectedInto<GoalsManager>();
            
            Container.BindFactory<GoalsTrackerProvider<ResourceUnit<float>, ResourceBalanceGoalTracker>,
                GoalsTrackerProvider<ResourceUnit<float>, ResourceBalanceGoalTracker>.Factory>().WhenInjectedInto<GoalsManager>();

            Container.BindFactory<StaffTrackersProvider, StaffTrackersProvider.Factory>().WhenInjectedInto<GoalsManager>();

            Container.BindFactory<Goal<BuildingUnit<int>>, int, IGoalTracker, TrackerFactory<BuildingUnit<int>>>()
                .To<BuildingGoalTracker>()
                .WhenInjectedInto<GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>>();

            Container.BindFactory<GoalWithTime<BuildingUnit<int>>, int, IGoalTracker, TrackerFactoryWithTimer<BuildingUnit<int>>>()
                .To<BuildingGoalTrackerWithTimer>()
                .WhenInjectedInto<GoalsTrackerProvider<BuildingUnit<int>, BuildingGoalTracker>>();

            Container.BindFactory<Goal<ResourceUnit<float>>, int, IGoalTracker, TrackerFactory<ResourceUnit<float>>>()
                .To<ResourceProductionGoalTracker>()
                .WhenInjectedInto<GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>>();

            Container.BindFactory<Goal<ResourceUnit<float>>, int, IGoalTracker, TrackerFactory<ResourceUnit<float>>>()
                .To<ResourceProductionGoalTracker>()
                .WhenInjectedInto<GoalsTrackerProvider<ResourceUnit<float>, ResourceProductionGoalTracker>>();

            Container.BindFactory<int, IGoalTracker, FakeTrackerFactory>()
                .To<BuildingTimerStaffGoalTracker>()
                .WhenInjectedInto<StaffTrackersProvider>();
        }

        private void InstallBuildings()
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

        private void InstallConnections()
        {
            Container.BindInterfacesAndSelfTo<ConnectionsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConnectionViewManager>().AsSingle();

            Container.Bind<SlotConnectonsFactory>().AsSingle();

            Container.BindFactory<SlotConnections, SlotConnections.Factory>().AsSingle();
        }

        private void InstallLevelMenus()
        {
            Container.Bind<CommandManager>().AsSingle();
            Container.Bind<LevelReceiver>().AsSingle();

            Container.Bind<LevelMenu>().AsSingle();

            Container.Bind<GameOverLevelMenu>().AsSingle();
            Container.Bind<GoalTextLevelMenu>().AsSingle();

            Container.Bind<NullCommand>().AsSingle();
            Container.BindFactory<NextCommand, NextCommand.Factory>();
            Container.BindFactory<StartCommand, StartCommand.Factory>();
        }


        private void InstallGoalInfos()
        {
            Container.BindInterfacesAndSelfTo<GoalDisplayStrategiesManager>().AsSingle();

            Container.Bind<StaffGoalDisplayStrategiesManager>().AsSingle();

            Container.Bind<GoalDisplayStrategiesFactory>().AsSingle();
            Container.Bind<StaffGoalDisplayStrategiesFactory>().AsSingle();

            Container.Bind<DisplayStrategiesFactory>().AsSingle();
            Container.Bind<DisplayStrategyFactory>().AsSingle();
            Container.Bind<AchivementDisplayController>().AsSingle();

            Container.BindFactory<GoalType, BuildingUnit<int>, IGoalInfo, GoalInfoFactory<BuildingUnit<int>>>()
                .To<BuildingGoalInfo>()
                .WhenInjectedInto<GoalDisplayStrategiesFactory>();

            Container.BindFactory<GoalType, float, BuildingUnit<int>, IGoalInfo, WithTimerGoalInfoFactory<BuildingUnit<int>>>()
                .To<BuildingWithTimerGoalInfo>().AsTransient();

            Container.BindFactory<GoalType, ResourceUnit<float>, IGoalInfo, GoalInfoFactory<ResourceUnit<float>>>()
                .To<ResourceGoalInfo>()
                .WhenInjectedInto<GoalDisplayStrategiesFactory>();

            Container.BindFactory<IGoalInfo, ResourceGoalDisplayStrategy, ResourceGoalDisplayStrategy.Factory>()
                .WhenInjectedInto<DisplayStrategyFactory>();

            Container.BindFactory<IGoalInfo, BuildingGoalDisplayStrategy, BuildingGoalDisplayStrategy.Factory>()
                .WhenInjectedInto<DisplayStrategyFactory>();

            Container.BindFactory<IGoalInfo, BuildingGoalWithTimerDisplayStrategy, BuildingGoalWithTimerDisplayStrategy.Factory>()
                .WhenInjectedInto<DisplayStrategyFactory>();

            Container.BindFactory<IGoalInfo, BuildingTimerStaffGoalDisplayStrategy, BuildingTimerStaffGoalDisplayStrategy.Factory>()
                .WhenInjectedInto<DisplayStrategyFactory>();
        }

        private void InstallUIAndManagers()
        {
            // PlacementMenuUI, GameUI -> bind via ZenjectBuinding Component -> GameUI object

            Container.Bind<ResourceSlotUISetter>().AsCached();
            Container.Bind<BuildingSlotUISetter>().AsCached();
            Container.Bind<TimerSlotUISetter>().AsCached();
            Container.Bind<GoalTypeUISetter>().AsCached();

            Container.Bind<ISetSlot>().To<ResourceSlotUISetter>().WhenInjectedInto<SlotUIFactory<ResourceSlotUI>>();            
            Container.Bind<ISetSlot>().To<ResourceSlotUISetter>().WhenInjectedInto<SlotUIFactory<ResourceBalanceSlotUI>>();
            Container.Bind<ISetSlot>().To<ResourceSlotUISetter>().WhenInjectedInto<SlotUIFactory<ResourceGoalSlotUI>>();

            Container.Bind<ISetSlot>().To<BuildingSlotUISetter>().WhenInjectedInto<SlotUIFactory<BuildingGoalSlotUI>>();
            Container.Bind<ISetSlot>().To<BuildingSlotUISetter>().WhenInjectedInto<SlotUIFactory<BuildingGoalWithTimerSlotUI>>();
            
            Container.Bind<ISetSlot>().To<GoalTypeUISetter>().WhenInjectedInto<ResourceGoalSlotsUIFactory>();

            Container.Bind<ISetSlot>().To<TimerSlotUISetter>().WhenInjectedInto<BuildingGoalSlotsUIFactory>();

            Container.BindInterfacesAndSelfTo<ResourceSlotMenuManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesBalanceMenuManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GoalSlotMenuManager>().AsSingle();

            Container.Bind<GoalSlotsUIFactory>().AsSingle();

            Container.Bind<SlotUIFactory<ResourceSlotUI>>().AsSingle().NonLazy();
            Container.Bind<SlotUIFactory<ResourceBalanceSlotUI>>().AsSingle().NonLazy();

            Container.Bind<SlotUIFactory<BuildingGoalSlotUI>>().AsSingle().NonLazy();
            Container.Bind<SlotUIFactory<BuildingGoalWithTimerSlotUI>>().AsSingle().NonLazy();
            Container.Bind<SlotUIFactory<ResourceGoalSlotUI>>().AsSingle().NonLazy();

            Container.BindFactory<BuildingGoalSlotsUIFactory, BuildingGoalSlotsUIFactory.Factory>().AsSingle();
            Container.BindFactory<ResourceGoalSlotsUIFactory, ResourceGoalSlotsUIFactory.Factory>().AsSingle();

            Container.BindFactory<ResourceSlotUI, PlaceholderFactory<ResourceSlotUI>>()
                .FromComponentInNewPrefab(_settings.ResourceSlotPrefab)
                .WithGameObjectName("ResourceSlot")
                .WhenInjectedInto<SlotUIFactory<ResourceSlotUI>>();

            Container.BindFactory<ResourceBalanceSlotUI, PlaceholderFactory<ResourceBalanceSlotUI>>()
                .FromComponentInNewPrefab(_settings.ResourceBalanceSlotPrefab)
                .WithGameObjectName("ResourceSlot")
                .WhenInjectedInto<SlotUIFactory<ResourceBalanceSlotUI>>();

            Container.BindFactory<BuildingGoalSlotUI, PlaceholderFactory<BuildingGoalSlotUI>>()
                .FromComponentInNewPrefab(_settings.BuildingGoalSlotPrefab)
                .WithGameObjectName("BuildingGoalSlot")
                .WhenInjectedInto<SlotUIFactory<BuildingGoalSlotUI>>();

            Container.BindFactory<BuildingGoalWithTimerSlotUI, PlaceholderFactory<BuildingGoalWithTimerSlotUI>>()
                .FromComponentInNewPrefab(_settings.BuildingWithTimerGoalSlotPrefab)
                .WithGameObjectName("BuildingGoalSlot")
                .WhenInjectedInto<SlotUIFactory<BuildingGoalWithTimerSlotUI>>();

            Container.BindFactory<ResourceGoalSlotUI, PlaceholderFactory<ResourceGoalSlotUI>>()
                .FromComponentInNewPrefab(_settings.ResourceGoalSlotPrefab)
                .WithGameObjectName("ResourceGoalSlot")
                .WhenInjectedInto<SlotUIFactory<ResourceGoalSlotUI>>();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<MapSlotsAreReadySignal>();
            Container.DeclareSignal<GoalStrategiesIsReadySignal>();

            Container.DeclareSignal<AddBuildingSignal>();
            Container.DeclareSignal<RemoveBuildingSignal>();

            Container.DeclareSignal<BuildingStateChangedSignal>();
            Container.DeclareSignal<TryChangeBuildingStateSignal>();

            Container.DeclareSignal<StartBuildingPlacementSignal>();
            Container.DeclareSignal<BuildingControllerSignal>();

            Container.DeclareSignal<PeriodChangeSignal>();
            Container.DeclareSignal<HourlySignal>();
            Container.DeclareSignal<TimeSpeedChangedSignal>().OptionalSubscriber();

            Container.DeclareSignal<GoalIsDoneSignal>();
            Container.DeclareSignal<GoalUpdatedSignal>().OptionalSubscriber();
            Container.DeclareSignal<AchivementReachedSignal>();

            Container.DeclareSignal<GameOverSignal>();

            Container.DeclareSignal<ResourcesBalanceUpdatedSignal>();
            Container.DeclareSignal<ResourcesProductionChangedSignal>().OptionalSubscriber();
            Container.DeclareSignal<ResourcesNeedsChangedSignal>().OptionalSubscriber();

            Container.DeclareSignal<NeedMenuInitSignal>().OptionalSubscriber();

            Container.DeclareSignal<SlotConnectionsUpdatedSignal>();

            Container.DeclareSignal<BuildingTimerIsOverSignal>();
        }

        [Serializable]
        public class Settings
        {
            public GameObject SlotPrefab;
            public GameObject BuildingPrefab;
            public GameObject BuildingSlotPrefab;
            public GameObject ResourceSlotPrefab;
            public GameObject ResourceBalanceSlotPrefab;
            public GameObject BuildingGoalSlotPrefab;
            public GameObject ResourceGoalSlotPrefab;
            public GameObject BuildingWithTimerGoalSlotPrefab;
        }
    }
}

