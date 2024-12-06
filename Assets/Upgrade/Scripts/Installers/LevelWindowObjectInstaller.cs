using LittleMars.Common.Signals;
using LittleMars.UI.Elements.MenuScreens;
using Zenject;

namespace LittleMars.Installers
{
    public class LevelWindowObjectInstaller : MonoInstaller<LevelWindowObjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuScreenController>().AsSingle();
            Container.DeclareSignal<MenuScreenSetText>();
        }
    }
}
