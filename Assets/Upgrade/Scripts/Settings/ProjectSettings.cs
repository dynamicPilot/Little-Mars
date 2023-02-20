using LittleMars.Common.Catalogues;
using LittleMars.Loaders;
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
            public SavesSystemManager.Settings SaveFiles;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Scenes.SceneNames);
            Container.BindInstance(Catalogues.Levels);
            Container.BindInstance(SaveSystem.SaveFiles);
        }
    }
}
