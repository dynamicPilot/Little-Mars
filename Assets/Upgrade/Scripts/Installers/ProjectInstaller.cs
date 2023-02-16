using LittleMars.Common.Catalogues;
using LittleMars.Loaders;
using LittleMars.PlayerStates;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log("Install buidings");
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<LevelsCatalogue>().AsSingle();

            // bind test player
            Container.Bind<IPlayerState>().To<MockPlayerState>().AsSingle();
        }
    }
}
