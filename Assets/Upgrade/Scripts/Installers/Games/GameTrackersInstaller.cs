using LittleMars.Common;
using LittleMars.Common.LevelGoal;
using LittleMars.Model;
using LittleMars.Model.Interfaces;
using LittleMars.Model.Trackers;
using Zenject;

namespace LittleMars.Installers.Games
{
    public class GameTrackersInstaller : Installer<GameTrackersInstaller>
    {
        public override void InstallBindings()
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
    }
}
