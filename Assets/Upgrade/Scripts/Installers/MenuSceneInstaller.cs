using LittleMars.Commands;
using LittleMars.Commands.MainMenu;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.MainMenus;
using LittleMars.SceneControls;
using LittleMars.UI.MainMenu;
using Zenject;

namespace LittleMars.Installers
{
    public class MenuSceneInstaller : MonoInstaller<MenuSceneInstaller>
    {
        public override void InstallBindings()
        {
            InstallMenu();
            InstallCommands();
            InstallSignals();
        }

        void InstallMenu()
        {
            Container.BindInterfacesAndSelfTo<MenuSceneControl>().AsSingle();
            Container.Bind<LevelsMenu>().AsSingle();
            Container.Bind<ISlotOnClick>().To<LevelsInfoSlotsByClick>().AsSingle();
        }

        void InstallCommands()
        {
            Container.Bind<MenuReceiver>().AsSingle();
            Container.Bind<SceneCommandManager>().To<MainMenuCommandManager>().AsSingle();
            Container.Bind<GameMenu>().AsSingle();
            Container.BindInterfacesAndSelfTo<ToLevelCommand>().AsSingle();
        }

        void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ToLevelSignal>();
            Container.DeclareSignal<EndSceneSignal>();
        }
    }
}
