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

        [Serializable]
        public class ScenesSettings
        {
            public SceneLoader.Settings SceneNames;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Scenes.SceneNames);
        }
    }
}
