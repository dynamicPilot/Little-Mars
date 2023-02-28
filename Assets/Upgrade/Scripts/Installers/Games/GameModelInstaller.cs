using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Catalogues;
using LittleMars.Common.Interfaces;
using LittleMars.Map;
using LittleMars.Map.Routers;
using LittleMars.Model;
using LittleMars.Model.Facades;
using LittleMars.Model.TimeUpdate;
using LittleMars.Models;
using LittleMars.UI;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers.Games
{
    public class GameModelInstaller : Installer<GameModelInstaller>
    {
        public override void InstallBindings()
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
    }
}
