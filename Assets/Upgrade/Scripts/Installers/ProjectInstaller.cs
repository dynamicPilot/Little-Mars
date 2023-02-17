using LittleMars.Commands;
using LittleMars.Common.Catalogues;
using LittleMars.Loaders;
using LittleMars.PlayerStates;
using UnityEngine;
using Zenject;
using LittleMars.SceneControls;

namespace LittleMars.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log("Install buidings");
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<ProjectSceneControl>().AsSingle();
            Container.Bind<LevelsCatalogue>().AsSingle();

            // bind test player
            Container.Bind<IPlayerState>().To<MockPlayerState>().AsSingle();

            InstallCommands();
        }

        private void InstallCommands()
        {          
            Container.Bind<ProjectCommandManager>().AsSingle();
            //Container.Bind<ProjectReceiver>().AsSingle();

            Container.Bind<NullCommand>().AsSingle();
            Container.BindFactory<NextCommand, NextCommand.Factory>();
            Container.BindFactory<MainMenuCommand, MainMenuCommand.Factory>();
        }
    }
}
