using LittleMars.Models.Grid;
using LittleMars.Models.Creators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using LittleMars.Slots;
using LittleMars.Settings;
using LittleMars.Slots.States;
using LittleMars.Map;
using LittleMars.Map.States;
using LittleMars.Common.Signals;
using LittleMars.Models.Facades;
using LittleMars.Common;
using System.Runtime.CompilerServices;
using Zenject.Asteroids;
using LittleMars.Slots.Views;
using LittleMars.Map.Routers;
using LittleMars.Buildings;
using LittleMars.Buildings.Parts;
using LittleMars.Models;
using LittleMars.Common.Interfaces;
using LittleMars.Model.Interfaces;
using LittleMars.UI.BuildingsSlots;
using LittleMars.UI.BuildingSlots;
using LittleMars.Model;
using LittleMars.UI;

namespace LittleMars.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            InstallMap();
            InstallModel();
            InstallBuildings();
            InstallViewSlots();
            InstallBuildingSlots();
            InstallUI();
            InstallSignals();
            InstallExecutionOrder();
        }

        void InstallExecutionOrder()
        {
            Container.BindExecutionOrder<ViewSlotManager>(-20);
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
            Container.Bind<BuildingCatalogue>().AsSingle();
            Container.Bind<ResourcesBalanceHelper>().AsSingle();
            Container.Bind<ProductionHelper>().AsSingle();
            Container.Bind<MapRouter>().AsSingle();
            Container.Bind<OperationManager>().AsSingle();
            Container.Bind<PlacementManager>().AsSingle();

            Container.Bind<IModelFacade>().To<ModelFacade>().AsSingle();
            Container.Bind<IPlacement>().To<PlacementManager>().FromResolve();
            Container.Bind<IProduction>().To<ProductionManager>().AsSingle();

            Container.BindFactory<BuildingType, Size, Path, Vector2, PlacingBuilding, PlacingBuilding.Factory>()
                .WhenInjectedInto<PlacementManager>();
            Container.BindFactory<MapRouterCheck, MapRouterCheck.Factory>().WhenInjectedInto<FieldManager>();
            Container.BindFactory<BuildingObject, MapRouterCheckForBuilding, MapRouterCheckForBuilding.Factory>()
                .WhenInjectedInto<PlacementManager>();
        }

        public void InstallBuildings()
        {
            Container.BindInterfacesAndSelfTo<BuildingManager>().AsSingle();
            Container.Bind<BuildingFactory>().AsSingle();

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

        private void InstallUI()
        {
            // PlacementMenuUI, GameUI -> bind via ZenjectBuinding Component -> GameUI object
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<MapSlotsAreReadySignal>();
            Container.DeclareSignal<AddBuildingSignal>();
            Container.DeclareSignal<RemoveBuildingSignal>();
            Container.DeclareSignal<BuildingStateChangedSignal>();
            Container.DeclareSignal<StartBuildingPlacementSignal>();
        }

        [Serializable]
        public class Settings
        {
            public GameObject SlotPrefab;
            public GameObject BuildingPrefab;
            public GameObject BuildingSlotPrefab;
        }
    }
}

