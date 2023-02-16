using LittleMars.Common.Catalogues;
using LittleMars.Loaders;
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

        public override void InstallBindings()
        {
            Container.BindInstance(Scenes.SceneNames);
            Container.BindInstance(Catalogues.Levels);
        }
    }
}
