using LittleMars.Commands;
using LittleMars.MainMenus;
using LittleMars.SceneControls;
using Zenject;

namespace LittleMars.Installers
{
    public class MenuSceneInstaller : MonoInstaller<MenuSceneInstaller>
    {
        public override void InstallBindings()
        {
            InstallMenu();
            InstallCommands();            
        }

        void InstallMenu()
        {
            Container.BindInterfacesAndSelfTo<MenuSceneControl>().AsSingle();

            Container.Bind<LevelsMenu>().AsSingle();
        }

        void InstallCommands()
        {
            Container.Bind<MenuReceiver>().AsSingle();
        }
    }
}
