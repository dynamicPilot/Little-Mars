using LittleMars.SceneControls;
using Zenject;

namespace LittleMars.Installers
{
    public class StartSceneInstaller : MonoInstaller<StartSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartSceneControl>().AsSingle();
        }
    }
}
