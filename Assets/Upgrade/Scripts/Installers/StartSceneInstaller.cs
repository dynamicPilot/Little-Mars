using LittleMars.Common.Signals;
using LittleMars.SceneControls;
using LittleMars.StartMenus;
using Zenject;

namespace LittleMars.Installers
{
    public class StartSceneInstaller : MonoInstaller<StartSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<StartSceneControl>().AsSingle();

            InstallLangMenu();
            InstallExecutionOrder();
            InstallSignals();
        }

        void InstallLangMenu()
        {
            Container.Bind<LangMenu>().AsSingle();
        }

        void InstallExecutionOrder()
        {
            Container.BindExecutionOrder<StartSceneControl>(-20);
            Container.BindExecutionOrder<StartManager>(-10);
        }

        void InstallSignals()
        {
            Container.DeclareSignal<EndSceneSignal>();
        }
    }
}
