using LittleMars.AudioSystems;
using LittleMars.Common.Catalogues;
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
            InstallSounds();
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

        void InstallSounds()
        {
            Container.Bind<SoundsCatalogue>().AsSingle();
            Container.Bind<UISoundSystem>().AsSingle();
        }

        void InstallSignals()
        {
            Container.DeclareSignal<EndSceneSignal>();
        }
    }
}
