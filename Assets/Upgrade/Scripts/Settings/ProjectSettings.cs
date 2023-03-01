using LittleMars.Common.Catalogues;
using LittleMars.Configs;
using LittleMars.Installers;
using LittleMars.Loaders;
using LittleMars.Localization;
using LittleMars.SaveSystem;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/Project Settings")]
    public class ProjectSettings : ScriptableObjectInstaller<ProjectSettings>
    {
        public ScenesSettings Scenes;
        public CatalogueSettings Catalogues;
        public SaveSystemSettings SaveSystem;
        public ConfigSystemSettings ConfigSystem;
        public TextBlocksSettings TextBlocks;
        public ProjectSystemsSettings ProjectSystems;


        [Serializable]
        public class ScenesSettings
        {
            public SceneLoader.Settings SceneNames;
        }

        [Serializable]
        public class CatalogueSettings
        {
            public LevelsCatalogue.Settings Levels;
        }

        [Serializable]
        public class SaveSystemSettings
        {
            public SavesSystemPathConstructor.Settings SaveFiles;
        }

        [Serializable]
        public class ConfigSystemSettings
        {
            public PlayerConfigSystem.Settings PlayerConfigs;
            public LangSettings.Settings PlayerSettings;
        }

        [Serializable]
        public class TextBlocksSettings
        {
            public LangManager.ProjectSettings Blocks;
        }

        [Serializable]
        public class ProjectSystemsSettings
        {
            public ProjectInstaller.Settings Installer;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Scenes.SceneNames);
            Container.BindInstance(Catalogues.Levels);
            Container.BindInstance(SaveSystem.SaveFiles);
            Container.BindInstance(ConfigSystem.PlayerConfigs);
            Container.BindInstance(ConfigSystem.PlayerSettings);
            Container.BindInstance(ProjectSystems.Installer);

            Container.BindInstance(TextBlocks.Blocks);
        }
    }
}
