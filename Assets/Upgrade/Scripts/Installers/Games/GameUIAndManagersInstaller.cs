using LittleMars.UI.GoalSlots;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using Zenject;

namespace LittleMars.Installers.Games
{
    public class GameUIAndManagersInstaller : Installer<GameUIAndManagersInstaller>
    {
        [Inject] GameSceneInstaller.Settings _settings;
        public override void InstallBindings()
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
    }
}
