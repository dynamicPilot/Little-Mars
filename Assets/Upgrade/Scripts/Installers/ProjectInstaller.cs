using LittleMars.Commands;
using LittleMars.Common.Catalogues;
using LittleMars.Loaders;
using LittleMars.PlayerStates;
using UnityEngine;
using Zenject;
using LittleMars.SceneControls;
using LittleMars.SaveSystem;
using LittleMars.Localization;
using LittleMars.Configs;
using LittleMars.Common.Signals;

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
            Container.Bind<JsonConverter>().AsSingle();


            InstallPlayer();
            InstallCommands();
            InstallSaveSystem();
            InstallConfigSystem();
            InstallSignals();
        }

        void InstallPlayer()
        {
            // bind test player
            Container.Bind<IPlayerState>().To<MockPlayerState>().AsSingle();
            //Container.BindInterfacesTo<PlayerState>();
            Container.Bind<PlayerSettings>().AsSingle();
        }

        private void InstallCommands()
        {
            Container.Bind<ProjectCommandManager>().AsSingle();
            Container.Bind<NullCommand>().AsSingle();

            Container.BindFactory<NextCommand, NextCommand.Factory>();
            Container.BindFactory<MainMenuCommand, MainMenuCommand.Factory>();
        }

        void InstallSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<SavesSystemManager>().AsSingle();
            Container.Bind<PlayerDataProvider>().AsSingle();
            Container.Bind<PathChecker>().AsSingle();
            Container.Bind<SavesSystemPathConstructor>().AsSingle();

            Container.BindFactory<ISaver, ILoader, SavesSystem, SavesSystem.Factory>().AsSingle();
            Container.BindFactory<JsonDataSaver, JsonDataSaver.Factory>().AsSingle();
            Container.BindFactory<JsonDataLoader, JsonDataLoader.Factory>().AsSingle();
            Container.BindFactory<BinaryDataLoader, BinaryDataLoader.Factory>().AsSingle();
            Container.BindFactory<EmptyDataLoader, EmptyDataLoader.Factory>().AsSingle();

            Container.BindFactory<DataLoaderFactory, DataLoaderFactory.Factory>().AsSingle();
        }

        void InstallConfigSystem()
        {
            Container.BindInterfacesAndSelfTo<PlayerConfigSystem>().AsSingle();
            Container.Bind<PlayerConfigProvider>().AsSingle();

        }

        void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<StartLoadingSignal>();
            Container.DeclareSignal<DataIsLoadedSignal>();
            Container.DeclareSignal<ConfigIsLoadedSignal>().OptionalSubscriber();
            Container.DeclareSignal<NoConfigIsLoadedSignal>().OptionalSubscriber();

            Container.DeclareSignal<NeedSaveConfigSignal>();
            Container.DeclareSignal<NeedSaveDataSignal>();
        }
    }
}
