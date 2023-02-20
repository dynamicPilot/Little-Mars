﻿using LittleMars.Commands;
using LittleMars.Common.Catalogues;
using LittleMars.Loaders;
using LittleMars.PlayerStates;
using UnityEngine;
using Zenject;
using LittleMars.SceneControls;
using LittleMars.SaveSystem;
using LittleMars.Localization;

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
            InstallSaveSystem();
        }

        private void InstallCommands()
        {          
            Container.Bind<ProjectCommandManager>().AsSingle();
            //Container.Bind<ProjectReceiver>().AsSingle();

            Container.Bind<NullCommand>().AsSingle();
            Container.BindFactory<NextCommand, NextCommand.Factory>();
            Container.BindFactory<MainMenuCommand, MainMenuCommand.Factory>();
        }

        void InstallSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<SavesSystemManager>().AsSingle();
            Container.Bind<PlayerDataProvider>().AsSingle();
            Container.Bind<PathChecker>().AsSingle();
            Container.Bind<JsonConverter>().AsSingle();
            Container.Bind<SavesSystemPathConstructor>().AsSingle();
            Container.Bind<DataLoaderFactory>().AsSingle();

            Container.BindFactory<ISaver, ILoader, SavesSystem, SavesSystem.Factory>().AsSingle();
            Container.BindFactory<JsonDataSaver, JsonDataSaver.Factory>().AsSingle();
            Container.BindFactory<JsonDataLoader, JsonDataLoader.Factory>().AsSingle();
            Container.BindFactory<BinaryDataLoader, BinaryDataLoader.Factory>().AsSingle();
            Container.BindFactory<EmptyDataLoader, EmptyDataLoader.Factory>().AsSingle();
        }
    }
}
