using LittleMars.UI.Windows;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers.Games
{
    public class WindowManagerInstaller : Installer<WindowManagerInstaller>
    {
        static public GameObject WindowPrefab { set; get; }
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WindowManager>().AsSingle();
            Container.Bind<WindowFactory>().AsSingle();

            Container.BindFactory<WindowID, GameWindow, GameWindow.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<GameWindowInstaller>(WindowPrefab);
        }
    }
}
