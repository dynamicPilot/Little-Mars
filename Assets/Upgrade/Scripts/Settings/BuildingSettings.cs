using LittleMars.Installers;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/Building Settings")]
    public class BuildingSettings : ScriptableObjectInstaller<BuildingSettings>
    {
        public ResourceFolders Folders;
        //[SerializeField] private TextAsset _testText;

        [Serializable]
        public class ResourceFolders
        {
            public BuildingInstaller.Settings Installer;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Folders.Installer);
        }

    }
}
