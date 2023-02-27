using LittleMars.Animations;
using LittleMars.AudioSystems;
using LittleMars.Buildings;
using LittleMars.CameraControls;
using LittleMars.Commands;
using LittleMars.Commands.Level;
using LittleMars.Common;
using LittleMars.Common.Catalogues;
using LittleMars.Common.Interfaces;
using LittleMars.Common.LevelGoal;
using LittleMars.Common.Signals;
using LittleMars.Connections;
using LittleMars.Connections.View;
using LittleMars.Controllers;
using LittleMars.Effects;
using LittleMars.LevelMenus;
using LittleMars.Localization;
using LittleMars.MainMenus;
using LittleMars.Map;
using LittleMars.Map.Routers;
using LittleMars.Map.States;
using LittleMars.Model;
using LittleMars.Model.Facades;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.Interfaces;
using LittleMars.Model.TimeUpdate;
using LittleMars.Model.Trackers;
using LittleMars.Models;
using LittleMars.PlayerStates;
using LittleMars.Rockets;
using LittleMars.SceneControls;
using LittleMars.Settings;
using LittleMars.Slots;
using LittleMars.UI;
using LittleMars.UI.Achievements;
using LittleMars.UI.BuildingSlots;
using LittleMars.UI.BuildingsSlots;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.GoalSlots;
using LittleMars.UI.GoalTextMenu;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;
        [Inject]
        IPlayerState _playerState;
        [Inject]
        LevelsCatalogue _levelCatalogue;
        public override void InstallBindings()
        {
            InstallLevelSettings();
            InstallLevel();
            InstallInputControls();

            InstallMap();
            InstallModel();            
            InstallTrackers();

            InstallLocalizationSystem();
            InstallEffects();
            InstallSounds();

            InstallBuildings();           
            InstallConnections();
            InstallRockets();

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

            Container.BindExecutionOrder<MapManager>(-10);
            Container.BindExecutionOrder<LevelManager>(10);
        }

        void InstallLevel()
        {
            
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelSceneControl>().AsSingle();
        }

        void InstallLevelSettings()
        {
            Debug.Log("Installer : no settings from catalogue");
            LevelSettings.InstallFromResource(String.Concat(_settings.LevelSettingsFolderPath,
                _playerState.GetLevelNumber(), "_", "LevelSettings"), Container);

            //var settings = _levelCatalogue.GetLevel(_playerState.GetLevelNumber());            
        }

        void InstallInputControls()
        {
            Container.BindInterfacesAndSelfTo<LittleMars.Controllers.InputControl>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputMoveDetection>().AsSingle();
            Container.BindInterfacesAndSelfTo<ZoomControl>().AsSingle();

            Container.Bind<CameraViewPositioner>().AsSingle();
            Container.Bind<CameraParamsUpdater>().AsSingle();
        }

        void InstallMap()
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

            Container.Bind<TradeManager>().AsSingle();
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

        void InstallLocalizationSystem()
        {         
            Container.BindInterfacesAndSelfTo<LevelLangsManager>().AsSingle();
        }


        void InstallEffects()
        {
            Container.BindInterfacesAndSelfTo<PeriodChangeEffectControl>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndGameTweenKiller>().AsSingle();
        }

        void InstallSounds()
        {
            Container.Bind<SoundsCatalogue>().AsSingle();
            Container.Bind<UISoundSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<NotSoundSystem>().AsSingle();
            Container.Bind<SoundsForGameMenuUI>().AsSingle();
        }

        private void InstallTrackers()
        {
            Container.BindInterfacesAndSelfTo<GoalsManager>().AsSingle();

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
        }

        private void InstallConnections()
        {
            Container.BindInterfacesAndSelfTo<ConnectionsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConnectionViewManager>().AsSingle();

            Container.Bind<SlotConnectonsFactory>().AsSingle();

            Container.BindFactory<SlotConnections, SlotConnections.Factory>().AsSingle();
        }

        private void InstallRockets()
        {
            Container.BindInterfacesAndSelfTo<RocketsManager>().AsSingle();
        }

        private void InstallLevelMenus()
        {
            Container.BindInterfacesAndSelfTo<LevelMenusWorkflow>().AsSingle();

            Container.Bind<AsyncSignalGunTimer>().AsSingle();
            Container.Bind<LevelMenusWorkflowTimer>().AsSingle();

            Container.Bind<LevelCommandManager>().AsSingle();
            Container.Bind<LevelReceiver>().AsSingle();           

            Container.Bind<LevelMenu>().AsSingle();

            Container.Bind<GameOverLevelMenu>().AsSingle();
            Container.Bind<GoalTextLevelMenu>().AsSingle();
            Container.Bind<AchievementDisplayLevelMenu>().AsSingle();

            Container.Bind<NullCommand>().AsSingle();
            Container.BindFactory<NextCommand, NextCommand.Factory>();
            Container.BindFactory<StartCommand, StartCommand.Factory>();
            Container.BindFactory<MainMenuCommand, MainMenuCommand.Factory>();
            Container.BindFactory<MainMenuByStartCommand, MainMenuByStartCommand.Factory>();

            Container.BindFactory<EndLevelSignalGun, EndLevelSignalGun.Factory>()
                .WhenInjectedInto<LevelMenusWorkflowTimer>();
        }


        private void InstallGoalInfos()
        {
            Container.BindInterfacesAndSelfTo<GoalDisplayStrategiesManager>().AsSingle();
            Container.Bind<StaffGoalDisplayStrategiesManager>().AsSingle();

            Container.Bind<GoalForStaffGoalCreator>().AsSingle();
            Container.Bind<GoalDisplayStrategiesFactory>().AsSingle();
            Container.Bind<StaffGoalDisplayStrategiesFactory>().AsSingle();

            Container.Bind<DisplayStrategiesFactory>().AsSingle();
            Container.Bind<DisplayStrategyFactory>().AsSingle();
            

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
            //SignalBusInstaller.Install(Container);
            Container.DeclareSignal<MapSlotsAreReadySignal>();
            Container.DeclareSignal<GoalStrategiesIsReadySignal>().OptionalSubscriber();

            Container.DeclareSignal<AddBuildingSignal>();
            Container.DeclareSignal<RemoveBuildingSignal>();

            Container.DeclareSignal<BuildingStateChangedSignal>();
            Container.DeclareSignal<TryChangeBuildingStateSignal>();

            Container.DeclareSignal<BeginBuildingDragSignal>();
            Container.DeclareSignal<StartBuildingPlacementSignal>();
            Container.DeclareSignal<BuildingControllerOpenSignal>();

            Container.DeclareSignal<PeriodChangeSignal>();
            Container.DeclareSignal<HourlySignal>();
            Container.DeclareSignal<TimeSpeedChangedSignal>().OptionalSubscriber();

            Container.DeclareSignal<GoalIsDoneSignal>();
            Container.DeclareSignal<GoalUpdatedSignal>().OptionalSubscriber();

            Container.DeclareSignal<StartLevelSignal>();
            Container.DeclareSignal<StartGameSignal>();

            Container.DeclareSignal<AchievementReachedSignal>();
            Container.DeclareSignal<CallAchivementMenuSignal>();

            Container.DeclareSignal<EndGameReachedSignal>();
            Container.DeclareSignal<EndGameSignal>();
            Container.DeclareSignal<GameOverSignal>();
            Container.DeclareSignal<EndLevelSignal>();
            Container.DeclareSignal<EndSceneSignal>();

            Container.DeclareSignal<ResourcesBalanceUpdatedSignal>();
            Container.DeclareSignal<ResourcesProductionChangedSignal>().OptionalSubscriber();
            Container.DeclareSignal<ResourcesNeedsChangedSignal>().OptionalSubscriber();
            Container.DeclareSignal<TotalProductionChangedSignal>().OptionalSubscriber();

            Container.DeclareSignal<NeedMenuInitSignal>().OptionalSubscriber();

            Container.DeclareSignal<SlotConnectionsUpdatedSignal>();

            Container.DeclareSignal<BuildingTimerIsOverSignal>();

            Container.DeclareSignal<StartTouchSignal>();
            Container.DeclareSignal<EndTouchSignal>();
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
            public string LevelSettingsFolderPath;
        }
    }
}

