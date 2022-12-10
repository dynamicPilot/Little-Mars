using LittleMars.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Building Settings")]
    public class BuildingSettings : ScriptableObjectInstaller<BuildingSettings>
    {
        public ResourceFolders Folders;

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
