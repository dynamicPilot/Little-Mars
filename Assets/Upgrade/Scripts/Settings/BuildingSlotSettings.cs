using LittleMars.Installers;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/BuildingSlot Settings")]
    public class BuildingSlotSettings : ScriptableObjectInstaller<BuildingSlotSettings>
    {
        public ResourceFolders Folders;

        [Serializable]
        public class ResourceFolders
        {
            public BuildingSlotInstaller.Settings Installer;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Folders.Installer);
        }

    }
}
