using LittleMars.Commands.Level;
using LittleMars.GoalInfoMenu;
using LittleMars.LevelMenus;
using LittleMars.UI.Achievements;
using Zenject;

namespace LittleMars.Installers.Games
{

    public class GameLevelMenusInstaller : Installer<GameLevelMenusInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelMenusWorkflow>().AsSingle();

            Container.Bind<AsyncSignalGunTimer>().AsSingle();
            Container.Bind<LevelMenusWorkflowTimer>().AsSingle();

            Container.Bind<LevelCommandManager>().AsSingle();
            Container.Bind<LevelReceiver>().AsSingle();

            Container.Bind<LevelMenu>().AsSingle();
            Container.Bind<PauseLevelMenu>().AsSingle();

            Container.Bind<GameOverLevelMenu>().AsSingle();
            Container.Bind<GoalTextLevelMenu>().AsSingle();
            Container.Bind<AchievementDisplayLevelMenu>().AsSingle();
            Container.Bind<GameStateLevelMenu>().AsSingle();

            Container.BindFactory<StartCommand, StartCommand.Factory>();
            Container.BindFactory<MainMenuByStartCommand, MainMenuByStartCommand.Factory>();
            Container.BindFactory<GoalInfoCommand, GoalInfoCommand.Factory>(); // -> may be will be removed
            Container.BindFactory<RestartLevelCommand, RestartLevelCommand.Factory>();

            Container.BindFactory<EndLevelSignalGun, EndLevelSignalGun.Factory>()
                .WhenInjectedInto<LevelMenusWorkflowTimer>();
        }
    }
}
