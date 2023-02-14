using LittleMars.Commands;
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
        }

        void InstallCommands()
        {
            Container.Bind<MenuReceiver>().AsSingle();
        }
    }
}
