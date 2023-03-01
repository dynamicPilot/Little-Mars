using LittleMars.Animations;
using LittleMars.AudioSystems;
using LittleMars.Buildings;
using LittleMars.CameraControls;
using LittleMars.Commands.Level;
using LittleMars.Common;
using LittleMars.Common.Catalogues;
using LittleMars.Common.Signals;
using LittleMars.Connections;
using LittleMars.Connections.View;
using LittleMars.Controllers;
using LittleMars.Effects;
using LittleMars.Installers.Games;
using LittleMars.LevelMenus;
using LittleMars.Localization;
using LittleMars.Map;
using LittleMars.Map.States;
using LittleMars.Model.GoalDisplays;
using LittleMars.Notifications;
using LittleMars.PlayerStates;
using LittleMars.Rockets;
using LittleMars.SceneControls;
using LittleMars.Settings;
using LittleMars.Slots;
using LittleMars.UI.Achievements;
using LittleMars.UI.BuildingSlots;
using LittleMars.UI.GoalDisplays;
using LittleMars.UI.GoalTextMenu;
using LittleMars.UI.ResourceSlots;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Inject] Settings _settings = null;
        [Inject] IPlayerState _playerState;
        [Inject] LevelsCatalogue _levelCatalogue;

        public override void InstallBindings()
        {
            InstallLevelSettings();
            InstallLevel();
            InstallInputControls();

            InstallMap();
            InstallModel();            
            InstallTrackers();

            InstallLocalization();
            InstallEffects();
            InstallSounds();
            InstallNotSystem();

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
            var settings = _levelCatalogue.GetLevel(_playerState.GetLevelNumber());
            if (settings == null) InstallLevelSettingsByResource();
            else InstallLevelSettingsByCatalogue(settings);
        }

        void InstallLevelSettingsByCatalogue(LevelSettings settings)
        {
            Container.BindInstance(settings.Info.LevelInfo);
            Container.BindInstance(settings.Conditions.InitialConditions);
            Container.BindInstance(settings.Map.Lines);
            Container.BindInstance(settings.Map.Fields);
            Container.BindInstance(settings.Goals.GoalsSettings);
            Container.BindInstance(settings.TextBlocks.Blocks);
            Container.BindInstance(settings.Rockets.Rockets);
        }

        void InstallLevelSettingsByResource()
        {
            Debug.Log("Installer : no settings from catalogue");
            LevelSettings.InstallFromResource(String.Concat(_settings.LevelSettingsFolderPath,
                _playerState.GetLevelNumber(), "_", "LevelSettings"), Container);
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
            GameModelInstaller.Install(Container);
        }

        void InstallLocalization()
        {         
            Container.BindInterfacesAndSelfTo<LevelLangsManager>().AsSingle();
        }

        void InstallEffects()
        {
            Container.BindInterfacesAndSelfTo<PeriodChangeEffectControl>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndGameTweenKiller>().AsSingle();
        }

        void InstallNotSystem()
        {
            Container.BindInterfacesAndSelfTo<LevelNotificationManager>().AsSingle();
        }

        void InstallSounds()
        {
            Container.Bind<SoundsCatalogue>().AsSingle();
            Container.Bind<UISoundSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<NotSoundSystem>().AsSingle();
            Container.Bind<SoundsForGameMenuUI>().AsSingle();
        }

        void InstallTrackers()
        {
            GameTrackersInstaller.Install(Container);
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

            Container.BindFactory<StartCommand, StartCommand.Factory>();
            Container.BindFactory<MainMenuByStartCommand, MainMenuByStartCommand.Factory>();
            Container.BindFactory<GoalInfoCommand, GoalInfoCommand.Factory>();
            Container.BindFactory<EndLevelSignalGun, EndLevelSignalGun.Factory>()
                .WhenInjectedInto<LevelMenusWorkflowTimer>();
        }

        void InstallGoalInfos()
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

        void InstallUIAndManagers()
        {
            GameUIAndManagersInstaller.Install(Container);
        }

        void InstallSignals()
        {
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
            Container.DeclareSignal<NeedGoalInfoSignal>();

            Container.DeclareSignal<SlotConnectionsUpdatedSignal>();

            Container.DeclareSignal<BuildingTimerIsOverSignal>();

            Container.DeclareSignal<StartTouchSignal>();
            Container.DeclareSignal<EndTouchSignal>();

            Container.DeclareSignal<RocketLaunchSignal>();
            Container.DeclareSignal<TimerOnSignal>();
            Container.DeclareSignal<TimerOffSignal>();

            Container.DeclareSignal<NeedRouteErrorNotSignal>();
            Container.DeclareSignal<NeedResourceNotSignal>();
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

